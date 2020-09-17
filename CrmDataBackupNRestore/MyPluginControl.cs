using System;
using System.Activities.Presentation.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Core.Definition;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

namespace CrmDataBackupNRestore
{
    /// <summary>
    /// https://www.xrmtoolbox.com/documentation/for-developers/plugincontrolbase-base-class/
    /// https://www.xrmtoolbox.com/documentation/for-developers/debug/
    /// https://www.xrmtoolbox.com/documentation/for-developers/interfaces/
    /// https://www.xrmtoolbox.com/documentation/for-developers/multipleconnectionsplugincontrolbase-base-class/
    /// https://www.xrmtoolbox.com/documentation/for-developers/
    /// </summary>
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;
        private string selectedEntityLogicalName;
        private bool isIVLoaded;

        private Dictionary<string,string> checkedEntities;
        private Dictionary<string, List<string>> checkedAttributes;

        //로드시 checked 이벤트 회피용
        private bool isAttributeLoad;
        public MyPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            if (mySettings.SelectedTabControl.HasValue)
            {
                tabControl1.SelectedIndex = mySettings.SelectedTabControl.Value;
                tabControl1_SelectedIndexChanged(null, null);
            }
            else
            {
                mySettings.SelectedTabControl = 0;
                tabControl1_SelectedIndexChanged(null, null);
            }

            checkedEntities = new Dictionary< string ,string>();
            checkedAttributes = new Dictionary<string, List<string>>(); 
            txt_C_IV.Text = Binary.ByteToHex(Binary.GenerateIV);
            isIVLoaded = false;
            isAttributeLoad = false;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            // The ExecuteMethod method handles connecting to an
            // organization if XrmToolBox is not yet connected
            //ExecuteMethod(GetAccounts);
        }

        private void GetSecurityRole()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Security Roles",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("role")
                    {

                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (args.Result is EntityCollection result)
                    {
                        foreach (var entity in result.Entities)
                        {
                            lv_securityRoles.Items.Add(new ListViewItem(new String[] { entity.LogicalName }));
                        }
                    }
                }
            });
        }

        private void GetEntities()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Entities",
                Work = (worker, args) =>
                {

                    args.Result = (RetrieveAllEntitiesResponse)Service.Execute(new RetrieveAllEntitiesRequest()
                    {
                        EntityFilters = EntityFilters.Entity
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (args.Result is RetrieveAllEntitiesResponse result)
                    {
                        foreach (var entity in result.EntityMetadata.Where(x=>x.CanCreateAttributes.Value && x.CanCreateCharts.Value && x.CanCreateForms.Value && x.CanCreateViews.Value))
                        {
                            lv_entities.Items.Add(new ListViewItem(new String[] {  entity.LogicalName, entity.DisplayName.UserLocalizedLabel?.Label, entity.MetadataId.ToString(), }));
                        }
                    }
                }
            });
        }

        private void GetAtrributes(ListViewItem.ListViewSubItemCollection listViewSubItem)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Attributes",
                Work = (worker, args) =>
                {
                    args.Result = (RetrieveEntityResponse)Service.Execute(new RetrieveEntityRequest()
                    {
                        EntityFilters = EntityFilters.Attributes,
                        LogicalName = listViewSubItem[lv_entities.Columns["ch_entity_logicalName"].Index].Text,
                        MetadataId = new Guid(listViewSubItem[lv_entities.Columns["ch_entity_guid"].Index].Text)
                    });
                    //args.Argument.ToString();
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (args.Result is RetrieveEntityResponse result)
                    {
                        isAttributeLoad = true;
                        
                        foreach (var attr in result.EntityMetadata.Attributes.Where(x=>x.IsValidForCreate == true))
                        {
                            lv_attributes.Items.Add(new ListViewItem(new String[] { attr.LogicalName, attr.DisplayName.UserLocalizedLabel?.Label, attr.AttributeTypeName.Value }));
                        }

                        var key = listViewSubItem[lv_entities.Columns["ch_entity_guid"].Index].Text;
                        //Get Checked
                        if (checkedAttributes.ContainsKey(key) && checkedAttributes[key] != null)
                        {
                            var logicalNameIndex = lv_attributes.Columns["ch_attr_logicalName"].Index;
                            foreach (var item in lv_attributes.Items.Cast<ListViewItem>().Where(x=> checkedAttributes[key].Contains(x.SubItems[logicalNameIndex].Text)))
                            {
                                item.Checked = true;
                            }
                        }

                        isAttributeLoad = false;
                    }
                }
            });
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ShowInfoNotification("You can visit XrmToolBox portal", new Uri("https://www.xrmtoolbox.com"), 32);
            //ShowWarningNotification("You can visit XrmToolBox portal", new Uri("https://www.xrmtoolbox.com"), 32);
            //ShowErrorNotification("You can visit XrmToolBox portal", new Uri("https://www.xrmtoolbox.com"), 32);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Call this method to hide the notification
            HideNotification();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mySettings.SelectedTabControl = tabControl1.SelectedIndex;
            if (tabControl1.SelectedTab == tp_general)
            {
                #region Init ListView
                // Width of -2 indicates auto-size.

                //lv_entities
                lv_entities.Clear();
                lv_entities.Columns.Add("ch_entity_logicalName", "Logical Name", 110, HorizontalAlignment.Left, null);
                lv_entities.Columns.Add("ch_entity_displayName", "Display Name", 110, HorizontalAlignment.Left, null);
                lv_entities.Columns.Add("ch_entity_guid", "Guid", 0, HorizontalAlignment.Left, null);

                //lv_attributes
                lv_attributes.Clear();
                lv_attributes.Columns.Add("ch_attr_logicalName", "Logical Name", 110, HorizontalAlignment.Left, null);
                lv_attributes.Columns.Add("ch_attr_displayName", "Display Name", 200, HorizontalAlignment.Left, null);
                lv_attributes.Columns.Add("ch_attr_type", "Type", 100, HorizontalAlignment.Left, null);
                #endregion

                #region init

                selectedEntityLogicalName = string.Empty;

                #endregion
                ExecuteMethod(GetEntities);
            }
            else if (tabControl1.SelectedTab == tp_privileges)
            {
                ExecuteMethod(GetSecurityRole);
            }
        }

        private void lv_entities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_entities.SelectedIndices.Count > 0)
            {
                //Retrieve attributes
                selectedEntityLogicalName = lv_entities.SelectedItems[0].SubItems[lv_entities.Columns["ch_entity_logicalName"].Index].Text;
                lv_attributes.Items.Clear();
                ExecuteMethod(GetAtrributes, lv_entities.SelectedItems[0].SubItems);
                //var attr = GetAtrributes(lv_entities.Items[lv_entities.SelectedIndices[0]][0]);
            }

        }

        private void tsb_Export_Click(object sender, EventArgs e)
        {
            //Core.Binary.Save("","a");
            //Encrypter.SaveAsBinary(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IV"), Encrypter.BlowFish.IV);
            //Encrypter.BlowFish.IV =  Core.Binary.LoadAsBinary<byte[]>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IV"));

            var folderPath = GetFolderPath();
            if (string.IsNullOrWhiteSpace(folderPath)) return;
            foreach (var entity in checkedEntities)
            {
                //get attributes
                if (checkedAttributes[entity.Key] != null)
                {
                    //var selectedAttributes = (from ListViewItem item in lv_attributes.Items select item.SubItems[lv_attributes.Columns["ch_attr_logicalName"].Index].Text);
                    var selectedAttributes = checkedAttributes[entity.Key];

                    if(!selectedAttributes.Any()) continue;
                    //var records = GetEntityRecords(selectedEntityLogicalName, selectedAttributes);
                    var records = GetEntityRecords(entity.Value, selectedAttributes);

                    //Core.Binary.SaveAsBinary(Path.Combine(folderPath, $"{selectedEntityLogicalName}_{DateTime.Now:yyyy-MM-dd_HHmmss}"), records, 2);
                    Core.Binary.SaveAsBinary(
                        Path.Combine(folderPath, $"{entity.Value}_{DateTime.Now:yyyy-MM-dd_HHmmss}"), records, 2);
                }
            }

            //IF LOADED DONT SAVE IV -- need create code
            if (!File.Exists(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}")))
            {
                Core.Binary.SaveAsBinary(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}"), Binary.IV);
            }
            else
            {
                var IV =  Binary.LoadAsBinary<byte[]>(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}"));
                if (!Binary.IV.Equals(IV))
                {
                    if (MessageBox.Show("Do you want overwrite new IV ?", "", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Core.Binary.SaveAsBinary(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}"), Binary.IV);
                    }
                }
            }
        }

        private void tsb_Import_Click(object sender, EventArgs e)
        {
            //Preview 필요
            var fileName = GetFilePath();
            if (string.IsNullOrWhiteSpace(fileName)) return;
            var records = Core.Binary.LoadAsBinary<IEnumerable<EntityWrapper>>(fileName, 2);

            foreach (var entity in records)
            {
                var aa = entity.GenerateEntity();
            }
        }

        private IEnumerable<EntityWrapper> GetEntityRecords(string entityLogicalName, IEnumerable<string> selectedAttributes)
        {
            //Need Fix : Attributes (overridencreatedon -> createdon)
            var qe = new QueryExpression(entityLogicalName)
            {
                ColumnSet = new ColumnSet(selectedAttributes.ToArray()),
                PageInfo = new PagingInfo()
                {
                    Count = 5000,
                    PageNumber = 1
                }
            };
            var ec = Service.RetrieveMultiple(qe);

            var entities = new List<EntityWrapper>(ec.Entities.Select(a => new EntityWrapper() { Id = a.Id, Attributes = a.Attributes.ToDictionary(x => x.Key, x => x.Value), LogicalName = a.LogicalName }));

            while (ec.MoreRecords)
            {
                qe.PageInfo.PageNumber += 1;
                qe.PageInfo.PagingCookie = ec.PagingCookie;
                ec = Service.RetrieveMultiple(qe);

                entities.AddRange(ec.Entities.Select(a => new EntityWrapper() { Id = a.Id, Attributes = a.Attributes.ToDictionary(x => x.Key, x => x.Value), LogicalName = a.LogicalName }));
            }
            return entities;
        }

        private string GetFolderPath()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //not empty is ok

                    return fbd.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string GetFilePath()
        {
            using (var ofd = new OpenFileDialog())
            {
                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    return ofd.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void btn_LoadIV_Click(object sender, EventArgs e)
        {
            Binary.IV = Binary.LoadAsBinary<byte[]>(GetFilePath());

            this.txt_C_IV.Text = Binary.ByteToHex(Binary.IV);
        }

        private void lv_entities_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var key = e.Item.SubItems[lv_entities.Columns["ch_entity_guid"].Index].Text;
            var value = e.Item.SubItems[lv_entities.Columns["ch_entity_logicalName"].Index].Text;

            if (e.Item.Checked)
            {
                if (!e.Item.Selected)
                {
                    foreach (ListViewItem item in lv_entities.SelectedItems)
                    {
                        item.Selected = false;
                    }
                    e.Item.Selected = true;
                }

                if (!checkedEntities.ContainsKey(key))
                {
                    checkedEntities.Add(key,value);
                    if (checkedAttributes.ContainsKey(key))
                    {
                        checkedAttributes.Remove(key);
                    }
                    checkedAttributes.Add(key, null);
                }
            }
            else
            {
                if (checkedEntities.ContainsKey(key))
                {
                    checkedEntities.Remove(key);
                    checkedAttributes.Remove(key);
                }
            }
        }

        private void lv_attributes_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (isAttributeLoad == false)
            {
                if (e.Item.Checked && lv_entities.SelectedIndices.Count > 0)
                {
                    var key = lv_entities.SelectedItems[0].SubItems[lv_entities.Columns["ch_entity_guid"].Index].Text;
                    if (!checkedAttributes.ContainsKey(key))
                    {
                        checkedAttributes.Add(key, null);
                    }

                    if (checkedAttributes[key] == null)
                    {
                        checkedAttributes[key] = new List<string>();
                    }

                    checkedAttributes[key]
                        .Add(e.Item.SubItems[lv_attributes.Columns["ch_attr_logicalName"].Index].Text);
                }
                else if (!e.Item.Checked && lv_entities.SelectedIndices.Count > 0)
                {
                    var key = lv_entities.SelectedItems[0].SubItems[lv_entities.Columns["ch_entity_guid"].Index].Text;

                    if (!checkedAttributes.ContainsKey(key))
                    {
                        checkedAttributes.Add(key, null);
                    }

                    if (checkedAttributes[key] == null)
                    {
                        checkedAttributes[key] = new List<string>();
                    }

                    var logicalName = e.Item.SubItems[lv_attributes.Columns["ch_attr_logicalName"].Index].Text;
                    if (checkedAttributes[key].Contains(logicalName))
                        checkedAttributes[key].Remove(logicalName);
                }
            }
        }
    }
}