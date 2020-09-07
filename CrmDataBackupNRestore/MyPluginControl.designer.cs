namespace CrmDataBackupNRestore
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSample = new System.Windows.Forms.ToolStripButton();
            this.tabS = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_general = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lv_entities = new System.Windows.Forms.ListView();
            this.ch_entity_logicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_entity_displayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_attributes = new System.Windows.Forms.ListView();
            this.ch_attr_logicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_attr_displayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_attr_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.tp_privileges = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.lv_securityRoles = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_C_SaveType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_C_CryptoUsage = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_C_CryptoKey = new System.Windows.Forms.TextBox();
            this.ch_entity_guid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tp_general.SuspendLayout();
            this.tp_privileges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(920, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(88, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSample
            // 
            this.tsbSample.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSample.Name = "tsbSample";
            this.tsbSample.Size = new System.Drawing.Size(48, 22);
            this.tsbSample.Text = "Try me";
            this.tsbSample.Click += new System.EventHandler(this.tsbSample_Click);
            // 
            // tabS
            // 
            this.tabS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tabS.Name = "tabS";
            this.tabS.Size = new System.Drawing.Size(100, 22);
            this.tabS.Text = "toolStripButton1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_general);
            this.tabControl1.Controls.Add(this.tp_privileges);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 125);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(920, 395);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tp_general
            // 
            this.tp_general.Controls.Add(this.label4);
            this.tp_general.Controls.Add(this.lv_entities);
            this.tp_general.Controls.Add(this.lv_attributes);
            this.tp_general.Controls.Add(this.label3);
            this.tp_general.Location = new System.Drawing.Point(4, 22);
            this.tp_general.Name = "tp_general";
            this.tp_general.Padding = new System.Windows.Forms.Padding(3);
            this.tp_general.Size = new System.Drawing.Size(912, 369);
            this.tp_general.TabIndex = 0;
            this.tp_general.Text = "General";
            this.tp_general.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(217, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Attributes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lv_entities
            // 
            this.lv_entities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_entity_guid,
            this.ch_entity_logicalName,
            this.ch_entity_displayName});
            this.lv_entities.FullRowSelect = true;
            this.lv_entities.GridLines = true;
            this.lv_entities.HideSelection = false;
            this.lv_entities.Location = new System.Drawing.Point(8, 39);
            this.lv_entities.Name = "lv_entities";
            this.lv_entities.Size = new System.Drawing.Size(205, 307);
            this.lv_entities.TabIndex = 4;
            this.lv_entities.UseCompatibleStateImageBehavior = false;
            this.lv_entities.View = System.Windows.Forms.View.Details;
            this.lv_entities.SelectedIndexChanged += new System.EventHandler(this.lv_entities_SelectedIndexChanged);
            // 
            // ch_entity_logicalName
            // 
            this.ch_entity_logicalName.Text = "Logical Name";
            this.ch_entity_logicalName.Width = 100;
            // 
            // ch_entity_displayName
            // 
            this.ch_entity_displayName.Text = "Display Name";
            this.ch_entity_displayName.Width = 100;
            // 
            // lv_attributes
            // 
            this.lv_attributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_attr_logicalName,
            this.ch_attr_displayName,
            this.ch_attr_type});
            this.lv_attributes.FullRowSelect = true;
            this.lv_attributes.GridLines = true;
            this.lv_attributes.HideSelection = false;
            this.lv_attributes.Location = new System.Drawing.Point(219, 39);
            this.lv_attributes.Name = "lv_attributes";
            this.lv_attributes.Size = new System.Drawing.Size(687, 307);
            this.lv_attributes.TabIndex = 3;
            this.lv_attributes.UseCompatibleStateImageBehavior = false;
            this.lv_attributes.View = System.Windows.Forms.View.Details;
            // 
            // ch_attr_logicalName
            // 
            this.ch_attr_logicalName.Text = "Logical Name";
            this.ch_attr_logicalName.Width = 100;
            // 
            // ch_attr_displayName
            // 
            this.ch_attr_displayName.Text = "Display Name";
            this.ch_attr_displayName.Width = 100;
            // 
            // ch_attr_type
            // 
            this.ch_attr_type.Text = "Type";
            this.ch_attr_type.Width = 100;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Entity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tp_privileges
            // 
            this.tp_privileges.Controls.Add(this.dataGridView1);
            this.tp_privileges.Controls.Add(this.label5);
            this.tp_privileges.Controls.Add(this.lv_securityRoles);
            this.tp_privileges.Location = new System.Drawing.Point(4, 22);
            this.tp_privileges.Name = "tp_privileges";
            this.tp_privileges.Padding = new System.Windows.Forms.Padding(3);
            this.tp_privileges.Size = new System.Drawing.Size(912, 369);
            this.tp_privileges.TabIndex = 1;
            this.tp_privileges.Text = "Privileges";
            this.tp_privileges.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(249, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(657, 340);
            this.dataGridView1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "보안역할";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lv_securityRoles
            // 
            this.lv_securityRoles.FullRowSelect = true;
            this.lv_securityRoles.GridLines = true;
            this.lv_securityRoles.HideSelection = false;
            this.lv_securityRoles.Location = new System.Drawing.Point(6, 33);
            this.lv_securityRoles.Name = "lv_securityRoles";
            this.lv_securityRoles.Size = new System.Drawing.Size(217, 199);
            this.lv_securityRoles.TabIndex = 6;
            this.lv_securityRoles.UseCompatibleStateImageBehavior = false;
            this.lv_securityRoles.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_C_SaveType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chk_C_CryptoUsage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_C_CryptoKey);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // cb_C_SaveType
            // 
            this.cb_C_SaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_C_SaveType.FormattingEnabled = true;
            this.cb_C_SaveType.Items.AddRange(new object[] {
            "Excel",
            "Binary"});
            this.cb_C_SaveType.Location = new System.Drawing.Point(316, 30);
            this.cb_C_SaveType.Name = "cb_C_SaveType";
            this.cb_C_SaveType.Size = new System.Drawing.Size(121, 20);
            this.cb_C_SaveType.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "저장 방법";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(493, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "암호화사용";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chk_C_CryptoUsage
            // 
            this.chk_C_CryptoUsage.AutoSize = true;
            this.chk_C_CryptoUsage.Location = new System.Drawing.Point(599, 32);
            this.chk_C_CryptoUsage.Name = "chk_C_CryptoUsage";
            this.chk_C_CryptoUsage.Size = new System.Drawing.Size(15, 14);
            this.chk_C_CryptoUsage.TabIndex = 2;
            this.chk_C_CryptoUsage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(645, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "암호키";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_C_CryptoKey
            // 
            this.txt_C_CryptoKey.Enabled = false;
            this.txt_C_CryptoKey.Location = new System.Drawing.Point(751, 30);
            this.txt_C_CryptoKey.Name = "txt_C_CryptoKey";
            this.txt_C_CryptoKey.Size = new System.Drawing.Size(100, 21);
            this.txt_C_CryptoKey.TabIndex = 0;
            // 
            // ch_entity_guid
            // 
            this.ch_entity_guid.Width = 0;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(920, 520);
            this.OnCloseTool += new System.EventHandler(this.MyPluginControl_OnCloseTool);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_general.ResumeLayout(false);
            this.tp_privileges.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbSample;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_general;
        private System.Windows.Forms.TabPage tp_privileges;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_C_CryptoUsage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_C_CryptoKey;
        private System.Windows.Forms.ToolStripButton tabS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lv_entities;
        private System.Windows.Forms.ListView lv_attributes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lv_securityRoles;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ColumnHeader ch_entity_logicalName;
        private System.Windows.Forms.ColumnHeader ch_entity_displayName;
        private System.Windows.Forms.ComboBox cb_C_SaveType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader ch_attr_logicalName;
        private System.Windows.Forms.ColumnHeader ch_attr_displayName;
        private System.Windows.Forms.ColumnHeader ch_attr_type;
        private System.Windows.Forms.ColumnHeader ch_entity_guid;
    }
}
