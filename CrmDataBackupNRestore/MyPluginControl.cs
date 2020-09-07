﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
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
            }
            else
            {
                mySettings.SelectedTabControl = 0;
            }
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
                    var result = args.Result as EntityCollection;
                    if (result != null)
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
                    var result = args.Result as RetrieveAllEntitiesResponse;
                    if (result != null)
                    {
                        foreach (var entity in result.EntityMetadata)
                        {
                            lv_entities.Items.Add(new ListViewItem(new String[] { entity.LogicalName, entity.DisplayName.UserLocalizedLabel?.Label }));
                        }
                    }
                }
            });
        }

        private void GetAtrributes(ListViewItem.ListViewSubItem listViewSubItem)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Attributes",
                Work = (worker, args) =>
                {
                    args.Result = (RetrieveAllEntitiesResponse)Service.Execute(new RetrieveAllEntitiesRequest()
                    {
                        EntityFilters = EntityFilters.Entity
                    });
                    //args.Argument.ToString();
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as RetrieveAllEntitiesResponse;
                    if (result != null)
                    {
                        foreach (var entity in result.EntityMetadata)
                        {
                            lv_entities.Items.Add(new ListViewItem(new String[] { entity.LogicalName, entity.DisplayName.UserLocalizedLabel?.Label }));
                        }
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
                ExecuteMethod(GetAtrributes, lv_entities.SelectedItems[0].SubItems[0]);
                //var attr = GetAtrributes(lv_entities.Items[lv_entities.SelectedIndices[0]][0]);
            }

        }
    }
}