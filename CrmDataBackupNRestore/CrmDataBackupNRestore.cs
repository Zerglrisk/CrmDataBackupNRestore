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
    public partial class CrmDataBackupNRestore : PluginControlBase
    {
        #region Private Variables

        private Settings mySettings;
        private string selectedEntityLogicalName;
        private bool isIVLoaded;

        private Dictionary<string, string> checkedEntities;
        private Dictionary<string, List<string>> checkedAttributes;

        /// <summary>
        /// 로드시 checked 이벤트 회피용
        /// </summary>
        private bool isAttributeLoad;

        private int lvattributesColumns;
        private int lventitiesColumns;

        #endregion

        public CrmDataBackupNRestore()
        {
            InitializeComponent();
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

        #region ToolStripMenu Button Event

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

        private void tsb_Export_Click(object sender, EventArgs e)
        {
            //Core.Binary.Save("","a");
            //Encrypter.SaveAsBinary(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IV"), Encrypter.BlowFish.IV);
            //Encrypter.BlowFish.IV =  Core.Binary.LoadAsBinary<byte[]>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IV"));

            var folderPath = GetFolderPath();
            if (string.IsNullOrWhiteSpace(folderPath)) return;

            ExecuteMethod(ExportData, folderPath);
        }

        private void itsmi_imports_Click(object sender, EventArgs e)
        {
            //var folderPath = GetFolderPath();
            MessageBox.Show("기능 준비중");
        }

        private void itsmi_import_Click(object sender, EventArgs e)
        {
            //Preview 필요
            //Need Fix : 레코드가 5000개 이상일 경우 어떻게 읽을 것인가를 해결방안 찾기
            //파일 확장자 정하기
            var fileName = GetFilePath("cdbr");
            if (string.IsNullOrWhiteSpace(fileName)) return;

            ExecuteMethod(ImportData, fileName);
        }
        #endregion

        #region Events

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

            //default true
            chk_C_CryptoUsage.Checked = !mySettings.isUseCrypto.HasValue || mySettings.isUseCrypto.Value;

            checkedEntities = new Dictionary<string, string>();
            checkedAttributes = new Dictionary<string, List<string>>();
            txt_C_IV.Text = Binary.ByteToHex(Binary.GenerateIV);
            isIVLoaded = false;
            isAttributeLoad = false;
            lvattributesColumns = -1;
            lventitiesColumns = -1;
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

        #endregion

        #region GroupBox Events

        private void btn_LoadIV_Click(object sender, EventArgs e)
        {
            var filePath = GetFilePath("iv");
            if (string.IsNullOrWhiteSpace(filePath)) return;
            Binary.IV = Binary.LoadAsBinary<byte[]>(filePath);
            this.txt_C_IV.Text = Binary.ByteToHex(Binary.IV);
        }

        #endregion

        #region General Tab Events

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

        private void lv_entities_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var key = e.Item.SubItems[lv_entities.Columns["ch_entity_guid"].Index].Text;
            var value = e.Item.SubItems[lv_entities.Columns["ch_entity_logicalName"].Index].Text;

            if (e.Item.Checked)
            {
                if (!e.Item.Selected)
                {
                    //If Not selected, set not select other item, select current item
                    foreach (ListViewItem item in lv_entities.SelectedItems.Cast<ListViewItem>().Where(x=> x.Selected == true))
                    {
                        item.Selected = false;
                    }
                    e.Item.Selected = true;
                }

                if (!checkedEntities.ContainsKey(key))
                {
                    //if not exist into dictionary, add item.
                    checkedEntities.Add(key, value);
                }
            }
            else
            {
                if (checkedEntities.ContainsKey(key))
                {
                    checkedEntities.Remove(key);
                    if (checkedAttributes.ContainsKey(key))
                    {
                        checkedAttributes.Remove(key);
                    }
                }
            }
        }

        private void lv_attributes_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (isAttributeLoad != false) return;

            if (e.Item.Checked && lv_entities.SelectedIndices.Count > 0)
            {
                //Need Fix : When Check Attribute only, Check Entity Too
                var key = lv_entities.SelectedItems[0].SubItems[lv_entities.Columns["ch_entity_guid"].Index].Text;
                if (!checkedAttributes.ContainsKey(key))
                {
                    //If Not Created  Item in Dict
                    lv_entities.SelectedItems[0].Checked = true;
                    checkedAttributes.Add(key, null);
                }

                if (checkedAttributes[key] == null)
                {
                    checkedAttributes[key] = new List<string>();
                }

                checkedAttributes[key].Add(e.Item.SubItems[lv_attributes.Columns["ch_attr_logicalName"].Index].Text);
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

        /// <summary>
        /// Attribute Column Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_attributes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != lvattributesColumns)
            {
                lvattributesColumns = e.Column;
                this.lv_attributes.Sorting = SortOrder.Ascending;
            }
            else
            {
                this.lv_attributes.Sorting = this.lv_attributes.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }

            this.lv_attributes.Sort();

            this.lv_attributes.ListViewItemSorter = new CustomListViewComparer(e.Column, this.lv_attributes.Sorting);
        }

        /// <summary>
        /// Entity Column Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_entities_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != lventitiesColumns)
            {
                lventitiesColumns = e.Column;
                this.lv_entities.Sorting = SortOrder.Ascending;
            }
            else
            {
                this.lv_entities.Sorting = this.lv_entities.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }

            this.lv_entities.Sort();

            this.lv_entities.ListViewItemSorter = new CustomListViewComparer(e.Column, this.lv_entities.Sorting);
        }

        #endregion

        #region Privileges Tab Events

        #endregion

        #region Private Excute Method

        private void ImportData(string fileName)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Importing",
                Work = (worker, args) =>
                {
                    var cnt = 0;

                    var records = Core.Binary.LoadAsBinaryEx<EntityWrapper>(fileName, 2).ToArray();

                    //worker.ReportProgress(cnt, $"[{entity.Value}] Retrieving Records");
                    //try
                    //{

                    var ec = records.Deserialize();
                    //}
                    //catch (System.Runtime.Serialization.SerializationException)
                    //{
                    //    //Decript Failed
                    //    throw new Exception("Please Load Correct IV First");
                    //}

                    foreach (var entity in ec.Entities)
                    {
                        if (entity.Contains("statuscode")) 
                        {
                            //if have statuscode, statecode just followed
                            var statusCode = ((OptionSetValue)entity["statuscode"]);
                            var stateCode = ((OptionSetValue)entity["statecode"]);
                            entity.Attributes.Remove("statuscode");
                            entity.Attributes.Remove("statecode");
                            try
                            {
                                var id = Service.Create(entity);
                                SetStatusCode(new EntityReference(entity.LogicalName, id), statusCode, statusCode);
                            }
                            catch (System.ServiceModel.FaultException ex)
                            {
                                if (ex.HResult == -2146233087)
                                {
                                    //duplicate
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            Service.Create(entity);
                        }
                    }


                    args.Result = "";
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = args =>
                {
                    // it will display "I have found the user id" in this example
                    SetWorkingMessage($"[{args.ProgressPercentage}/{checkedEntities.Count}] {args.UserState}");
                },
            });
        }
        private void ExportData(string folderPath)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Exporting",
                Work = (worker, args) =>
                {
                    var cnt = 0;
                    foreach (var entity in checkedEntities)
                    {
                        //get attributes
                        if (checkedAttributes.ContainsKey(entity.Key))
                        {
                            ++cnt;
                            //var selectedAttributes = (from ListViewItem item in lv_attributes.Items select item.SubItems[lv_attributes.Columns["ch_attr_logicalName"].Index].Text);
                            var selectedAttributes = checkedAttributes[entity.Key];

                            if (!selectedAttributes.Any()) continue;

                            worker.ReportProgress(cnt, $"[{entity.Value}] Retrieving Records");

                            //var records = GetEntityRecords(entity.Value, selectedAttributes);
                            var recordsCnt = 0;
                            #region GetEntityRecords
                            //Attributes (overriddencreatedon -> createdon)
                            var arr = selectedAttributes.Select(x => !x.Equals("overriddencreatedon") ? x : "createdon").ToList();

                            var test = Core.Definition.Serialize.EntityMetadataWrapper.GetEntityMetadataWrapper(Service, entity.Value);
                            //Attributes (if have statuscode, must follow statecode too)
                            if (arr.Contains("statuscode"))//.Any(x => x.Equals("statuscode")))
                            {
                                arr.Add("statecode");
                            }


                            var qe = new QueryExpression(entity.Value)
                            {
                                ColumnSet = new ColumnSet(arr.ToArray()),
                                PageInfo = new PagingInfo()
                                {
                                    Count = 5000,
                                    PageNumber = 1,
                                },
                            };
                            var ec = Service.RetrieveMultiple(qe);

                            var entities = new List<EntityWrapper>(ec.Entities.Select(a => new EntityWrapper() { Id = a.Id, Attributes = a.Attributes.ToDictionary(x => x.Key, x => x.Value), LogicalName = a.LogicalName }));
                            recordsCnt = ec.Entities.Count;
                            worker.ReportProgress(cnt, $"[{entity.Value}] {recordsCnt} Records Retrieving");
                            while (ec.MoreRecords)
                            {
                                qe.PageInfo.PageNumber += 1;
                                qe.PageInfo.PagingCookie = ec.PagingCookie;
                                ec = Service.RetrieveMultiple(qe);

                                entities.AddRange(ec.Entities.Select(a => new EntityWrapper() { Id = a.Id, Attributes = a.Attributes.ToDictionary(x => x.Key, x => x.Value), LogicalName = a.LogicalName }));
                                recordsCnt += ec.Entities.Count;
                                worker.ReportProgress(cnt, $"[{entity.Value}] {recordsCnt} Records Saving");
                            }
                            #endregion                  

                            //Core.Binary.SaveAsBinary(Path.Combine(folderPath, $"{selectedEntityLogicalName}_{DateTime.Now:yyyy-MM-dd_HHmmss}"), records, 2);
                            Core.Binary.SaveAsBinaryEx(
                                Path.Combine(folderPath, $"{entity.Value}_{DateTime.Now:yyyy-MM-dd_HHmmss}_{recordsCnt}.cdbr"), entities, 2);
                        }
                    }

                    args.Result = "";
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //IF LOADED DONT SAVE IV -- need create code
                    if (!File.Exists(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}")))
                    {
                        Core.Binary.SaveAsBinary(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}.iv"), Binary.IV);
                    }
                    else
                    {
                        var IV = Binary.LoadAsBinary<byte[]>(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}"));
                        if (!Binary.IV.Equals(IV))
                        {
                            if (MessageBox.Show("Do you want overwrite new IV ?", "", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                Core.Binary.SaveAsBinary(Path.Combine(folderPath, $"IV_{HardwareInfoGetter.GetUUID()}.iv"), Binary.IV);
                            }
                        }
                    }
                },
                ProgressChanged = args =>
                {
                    // it will display "I have found the user id" in this example
                    SetWorkingMessage($"[{args.ProgressPercentage}/{checkedEntities.Count}] {args.UserState}");
                },
            });
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
                        return;
                    }

                    if (args.Result is RetrieveAllEntitiesResponse result)
                    {
                        foreach (var entity in result.EntityMetadata.Where(x =>
                            x.CanCreateAttributes.Value && x.CanCreateCharts.Value && x.CanCreateForms.Value &&
                            x.CanCreateViews.Value))
                        {
                            lv_entities.Items.Add(new ListViewItem(new String[]
                            {
                                entity.LogicalName, entity.DisplayName.UserLocalizedLabel?.Label,
                                entity.MetadataId.ToString(),
                            }));
                        }
                    }
                },
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

                        foreach (var attr in result.EntityMetadata.Attributes.Where(
                            x => (x.IsValidForCreate != null && x.IsValidForCreate.Value)
                               && (!string.IsNullOrWhiteSpace(x.DisplayName.UserLocalizedLabel?.Label))
                               && (x.IsValidForRead != null && x.IsValidForRead.Value)))
                        {
                            lv_attributes.Items.Add(new ListViewItem(new String[] { attr.LogicalName, attr.DisplayName.UserLocalizedLabel?.Label, attr.AttributeTypeName.Value }));
                        }

                        var key = listViewSubItem[lv_entities.Columns["ch_entity_guid"].Index].Text;
                        //Get Checked
                        if (checkedAttributes.ContainsKey(key) && checkedAttributes[key] != null)
                        {
                            var logicalNameIndex = lv_attributes.Columns["ch_attr_logicalName"].Index;
                            foreach (var item in lv_attributes.Items.Cast<ListViewItem>().Where(x => checkedAttributes[key].Contains(x.SubItems[logicalNameIndex].Text)))
                            {
                                item.Checked = true;
                            }
                        }

                        isAttributeLoad = false;
                    }
                }
            });
        }

        #endregion

        #region Private Call Method

        private IEnumerable<EntityWrapper> GetEntityRecords(string entityLogicalName, IEnumerable<string> selectedAttributes)
        {
            //Attributes (overriddencreatedon -> createdon)
            var arr = selectedAttributes.Select(x => !x.Equals("overriddencreatedon") ? x : "createdon").ToList();

            //Attributes (if have statuscode, must follow statecode too)
            if (arr.Contains("statuscode"))//.Any(x => x.Equals("statuscode")))
            {
                arr.Add("statecode");
            }


            var qe = new QueryExpression(entityLogicalName)
            {
                ColumnSet = new ColumnSet(arr.ToArray()),
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
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //not empty is ok

                    return fbd.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string GetFilePath(string extension = "")
        {
            using (var ofd = new OpenFileDialog())
            {
                if (!string.IsNullOrWhiteSpace(extension))
                {
                    ofd.DefaultExt = extension;
                    ofd.Filter = $"{extension} file(*.{extension})|*.{extension}";
                }

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

        private void SetStatusCode(EntityReference target, OptionSetValue stateCode, OptionSetValue statusCode)
        {
            var setStateRequest = new SetStateRequest
            {
                EntityMoniker = target,
                State = stateCode,
                Status = statusCode
            };
            var response = (SetStateResponse)Service.Execute(setStateRequest);
        }
        #endregion
    }
}