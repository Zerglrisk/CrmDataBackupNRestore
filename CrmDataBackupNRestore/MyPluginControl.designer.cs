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
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.tsb_Export = new System.Windows.Forms.ToolStripButton();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSample = new System.Windows.Forms.ToolStripButton();
            this.tabS = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_general = new System.Windows.Forms.TabPage();
            this.pn_general_attribute = new System.Windows.Forms.Panel();
            this.lv_attributes = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.pn_general_entity = new System.Windows.Forms.Panel();
            this.lv_entities = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.tp_privileges = new System.Windows.Forms.TabPage();
            this.pn_priv_privileges_2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pn_priv_privileges = new System.Windows.Forms.Panel();
            this.lv_securityRoles = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_LoadIV = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cb_C_SaveType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_C_CryptoUsage = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_C_IV = new System.Windows.Forms.TextBox();
            this.toolStripMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_general.SuspendLayout();
            this.pn_general_attribute.SuspendLayout();
            this.pn_general_entity.SuspendLayout();
            this.tp_privileges.SuspendLayout();
            this.pn_priv_privileges_2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pn_priv_privileges.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Close,
            this.tsb_Export,
            this.tsb_Import});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(920, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsb_Close
            // 
            this.tsb_Close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(40, 22);
            this.tsb_Close.Text = "Close";
            this.tsb_Close.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tsb_Export
            // 
            this.tsb_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Export.Name = "tsb_Export";
            this.tsb_Export.Size = new System.Drawing.Size(45, 22);
            this.tsb_Export.Text = "Export";
            this.tsb_Export.Click += new System.EventHandler(this.tsb_Export_Click);
            // 
            // tsb_Import
            // 
            this.tsb_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_Import.Name = "tsb_Import";
            this.tsb_Import.Size = new System.Drawing.Size(47, 22);
            this.tsb_Import.Text = "Import";
            this.tsb_Import.Click += new System.EventHandler(this.tsb_Import_Click);
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
            this.tp_general.Controls.Add(this.pn_general_attribute);
            this.tp_general.Controls.Add(this.pn_general_entity);
            this.tp_general.Location = new System.Drawing.Point(4, 22);
            this.tp_general.Name = "tp_general";
            this.tp_general.Padding = new System.Windows.Forms.Padding(3);
            this.tp_general.Size = new System.Drawing.Size(912, 369);
            this.tp_general.TabIndex = 0;
            this.tp_general.Text = "General";
            this.tp_general.UseVisualStyleBackColor = true;
            // 
            // pn_general_attribute
            // 
            this.pn_general_attribute.Controls.Add(this.lv_attributes);
            this.pn_general_attribute.Controls.Add(this.label4);
            this.pn_general_attribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_general_attribute.Location = new System.Drawing.Point(253, 3);
            this.pn_general_attribute.Name = "pn_general_attribute";
            this.pn_general_attribute.Padding = new System.Windows.Forms.Padding(10);
            this.pn_general_attribute.Size = new System.Drawing.Size(656, 363);
            this.pn_general_attribute.TabIndex = 6;
            // 
            // lv_attributes
            // 
            this.lv_attributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_attributes.FullRowSelect = true;
            this.lv_attributes.GridLines = true;
            this.lv_attributes.HideSelection = false;
            this.lv_attributes.Location = new System.Drawing.Point(10, 33);
            this.lv_attributes.Name = "lv_attributes";
            this.lv_attributes.Size = new System.Drawing.Size(636, 320);
            this.lv_attributes.TabIndex = 3;
            this.lv_attributes.UseCompatibleStateImageBehavior = false;
            this.lv_attributes.View = System.Windows.Forms.View.Details;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(636, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Attributes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pn_general_entity
            // 
            this.pn_general_entity.Controls.Add(this.lv_entities);
            this.pn_general_entity.Controls.Add(this.label3);
            this.pn_general_entity.Dock = System.Windows.Forms.DockStyle.Left;
            this.pn_general_entity.Location = new System.Drawing.Point(3, 3);
            this.pn_general_entity.Name = "pn_general_entity";
            this.pn_general_entity.Padding = new System.Windows.Forms.Padding(10);
            this.pn_general_entity.Size = new System.Drawing.Size(250, 363);
            this.pn_general_entity.TabIndex = 8;
            // 
            // lv_entities
            // 
            this.lv_entities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_entities.FullRowSelect = true;
            this.lv_entities.GridLines = true;
            this.lv_entities.HideSelection = false;
            this.lv_entities.Location = new System.Drawing.Point(10, 33);
            this.lv_entities.Name = "lv_entities";
            this.lv_entities.Size = new System.Drawing.Size(230, 320);
            this.lv_entities.TabIndex = 4;
            this.lv_entities.UseCompatibleStateImageBehavior = false;
            this.lv_entities.View = System.Windows.Forms.View.Details;
            this.lv_entities.SelectedIndexChanged += new System.EventHandler(this.lv_entities_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Entity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tp_privileges
            // 
            this.tp_privileges.Controls.Add(this.pn_priv_privileges_2);
            this.tp_privileges.Controls.Add(this.pn_priv_privileges);
            this.tp_privileges.Location = new System.Drawing.Point(4, 22);
            this.tp_privileges.Name = "tp_privileges";
            this.tp_privileges.Padding = new System.Windows.Forms.Padding(3);
            this.tp_privileges.Size = new System.Drawing.Size(912, 369);
            this.tp_privileges.TabIndex = 1;
            this.tp_privileges.Text = "Privileges";
            this.tp_privileges.UseVisualStyleBackColor = true;
            // 
            // pn_priv_privileges_2
            // 
            this.pn_priv_privileges_2.Controls.Add(this.dataGridView1);
            this.pn_priv_privileges_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_priv_privileges_2.Location = new System.Drawing.Point(253, 3);
            this.pn_priv_privileges_2.Name = "pn_priv_privileges_2";
            this.pn_priv_privileges_2.Padding = new System.Windows.Forms.Padding(10);
            this.pn_priv_privileges_2.Size = new System.Drawing.Size(656, 363);
            this.pn_priv_privileges_2.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(636, 343);
            this.dataGridView1.TabIndex = 8;
            // 
            // pn_priv_privileges
            // 
            this.pn_priv_privileges.Controls.Add(this.lv_securityRoles);
            this.pn_priv_privileges.Controls.Add(this.label5);
            this.pn_priv_privileges.Dock = System.Windows.Forms.DockStyle.Left;
            this.pn_priv_privileges.Location = new System.Drawing.Point(3, 3);
            this.pn_priv_privileges.Name = "pn_priv_privileges";
            this.pn_priv_privileges.Padding = new System.Windows.Forms.Padding(10);
            this.pn_priv_privileges.Size = new System.Drawing.Size(250, 363);
            this.pn_priv_privileges.TabIndex = 9;
            // 
            // lv_securityRoles
            // 
            this.lv_securityRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_securityRoles.FullRowSelect = true;
            this.lv_securityRoles.GridLines = true;
            this.lv_securityRoles.HideSelection = false;
            this.lv_securityRoles.Location = new System.Drawing.Point(10, 33);
            this.lv_securityRoles.Name = "lv_securityRoles";
            this.lv_securityRoles.Size = new System.Drawing.Size(230, 320);
            this.lv_securityRoles.TabIndex = 6;
            this.lv_securityRoles.UseCompatibleStateImageBehavior = false;
            this.lv_securityRoles.View = System.Windows.Forms.View.Details;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "보안역할";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_LoadIV);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.cb_C_SaveType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chk_C_CryptoUsage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_C_IV);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btn_LoadIV
            // 
            this.btn_LoadIV.Location = new System.Drawing.Point(855, 29);
            this.btn_LoadIV.Name = "btn_LoadIV";
            this.btn_LoadIV.Size = new System.Drawing.Size(50, 23);
            this.btn_LoadIV.TabIndex = 8;
            this.btn_LoadIV.Text = "Load";
            this.btn_LoadIV.UseVisualStyleBackColor = true;
            this.btn_LoadIV.Click += new System.EventHandler(this.btn_LoadIV_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(253, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "Preview 사용";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(359, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.UseVisualStyleBackColor = true;
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
            this.label1.Text = "IV";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_C_IV
            // 
            this.txt_C_IV.Enabled = false;
            this.txt_C_IV.Location = new System.Drawing.Point(751, 30);
            this.txt_C_IV.MaxLength = 16;
            this.txt_C_IV.Name = "txt_C_IV";
            this.txt_C_IV.Size = new System.Drawing.Size(100, 21);
            this.txt_C_IV.TabIndex = 0;
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
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tp_general.ResumeLayout(false);
            this.pn_general_attribute.ResumeLayout(false);
            this.pn_general_entity.ResumeLayout(false);
            this.tp_privileges.ResumeLayout(false);
            this.pn_priv_privileges_2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pn_priv_privileges.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txt_C_IV;
        private System.Windows.Forms.ToolStripButton tabS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lv_entities;
        private System.Windows.Forms.ListView lv_attributes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lv_securityRoles;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cb_C_SaveType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pn_general_attribute;
        private System.Windows.Forms.Panel pn_general_entity;
        private System.Windows.Forms.Panel pn_priv_privileges_2;
        private System.Windows.Forms.Panel pn_priv_privileges;
        private System.Windows.Forms.ToolStripButton tsb_Export;
        private System.Windows.Forms.ToolStripButton tsb_Import;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_LoadIV;
    }
}
