namespace PpmMain
{
    partial class PluginManagerMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManagerMainForm));
            this.PluginTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PluginDescriptionAvailable = new System.Windows.Forms.TextBox();
            this.AvailablePluginsList = new System.Windows.Forms.DataGridView();
            this.availablePluginsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.UpdatedPluginsList = new System.Windows.Forms.DataGridView();
            this.outdatedPluginsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.PluginDescriptionInstalled = new System.Windows.Forms.TextBox();
            this.InstalledPluginsList = new System.Windows.Forms.DataGridView();
            this.installedPluginsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Uninstall = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.outdatedPluginsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.shortNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.licenseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.installedVersionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shortNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDescriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.licenseDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shortNameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDescriptionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.licenseDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstalledVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pluginManagerMainFormControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PluginTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailablePluginsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.availablePluginsBindingSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdatedPluginsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstalledPluginsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pluginManagerMainFormControllerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PluginTabs
            // 
            this.PluginTabs.Controls.Add(this.tabPage1);
            this.PluginTabs.Controls.Add(this.tabPage2);
            this.PluginTabs.Controls.Add(this.tabPage3);
            this.PluginTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginTabs.Location = new System.Drawing.Point(0, 0);
            this.PluginTabs.Name = "PluginTabs";
            this.PluginTabs.SelectedIndex = 0;
            this.PluginTabs.Size = new System.Drawing.Size(1037, 672);
            this.PluginTabs.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.PluginDescriptionAvailable);
            this.tabPage1.Controls.Add(this.AvailablePluginsList);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.tabPage1.Size = new System.Drawing.Size(1029, 639);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Available";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // PluginDescriptionAvailable
            // 
            this.PluginDescriptionAvailable.Location = new System.Drawing.Point(8, 423);
            this.PluginDescriptionAvailable.Multiline = true;
            this.PluginDescriptionAvailable.Name = "PluginDescriptionAvailable";
            this.PluginDescriptionAvailable.Size = new System.Drawing.Size(1013, 208);
            this.PluginDescriptionAvailable.TabIndex = 12;
            // 
            // AvailablePluginsList
            // 
            this.AvailablePluginsList.AutoGenerateColumns = false;
            this.AvailablePluginsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AvailablePluginsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn});
            this.AvailablePluginsList.DataSource = this.availablePluginsBindingSource;
            this.AvailablePluginsList.Location = new System.Drawing.Point(8, 50);
            this.AvailablePluginsList.Name = "AvailablePluginsList";
            this.AvailablePluginsList.Size = new System.Drawing.Size(1013, 367);
            this.AvailablePluginsList.TabIndex = 11;
            // 
            // availablePluginsBindingSource
            // 
            this.availablePluginsBindingSource.DataMember = "availablePlugins";
            this.availablePluginsBindingSource.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button4.Location = new System.Drawing.Point(926, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 26);
            this.button4.TabIndex = 9;
            this.button4.Text = "Install";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(288, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 26);
            this.button1.TabIndex = 8;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 26);
            this.textBox1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox5);
            this.tabPage2.Controls.Add(this.UpdatedPluginsList);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.tabPage2.Size = new System.Drawing.Size(1029, 639);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Updated";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(8, 423);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(1013, 208);
            this.textBox5.TabIndex = 14;
            // 
            // UpdatedPluginsList
            // 
            this.UpdatedPluginsList.AllowUserToOrderColumns = true;
            this.UpdatedPluginsList.AutoGenerateColumns = false;
            this.UpdatedPluginsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UpdatedPluginsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.InstalledVersion,
            this.versionDataGridViewTextBoxColumn1});
            this.UpdatedPluginsList.DataSource = this.outdatedPluginsBindingSource1;
            this.UpdatedPluginsList.Location = new System.Drawing.Point(8, 50);
            this.UpdatedPluginsList.Margin = new System.Windows.Forms.Padding(0);
            this.UpdatedPluginsList.Name = "UpdatedPluginsList";
            this.UpdatedPluginsList.Size = new System.Drawing.Size(1013, 367);
            this.UpdatedPluginsList.TabIndex = 13;
            // 
            // outdatedPluginsBindingSource1
            // 
            this.outdatedPluginsBindingSource1.DataMember = "outdatedPlugins";
            this.outdatedPluginsBindingSource1.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button7.Location = new System.Drawing.Point(840, 13);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(80, 26);
            this.button7.TabIndex = 12;
            this.button7.Text = "Update";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button5.Location = new System.Drawing.Point(926, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 26);
            this.button5.TabIndex = 11;
            this.button5.Text = "Update All";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.Location = new System.Drawing.Point(288, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 26);
            this.button2.TabIndex = 10;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(269, 26);
            this.textBox2.TabIndex = 9;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.PluginDescriptionInstalled);
            this.tabPage3.Controls.Add(this.InstalledPluginsList);
            this.tabPage3.Controls.Add(this.Uninstall);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.tabPage3.Size = new System.Drawing.Size(1029, 639);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Installed";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // PluginDescriptionInstalled
            // 
            this.PluginDescriptionInstalled.Location = new System.Drawing.Point(8, 423);
            this.PluginDescriptionInstalled.Multiline = true;
            this.PluginDescriptionInstalled.Name = "PluginDescriptionInstalled";
            this.PluginDescriptionInstalled.Size = new System.Drawing.Size(1013, 208);
            this.PluginDescriptionInstalled.TabIndex = 14;
            // 
            // InstalledPluginsList
            // 
            this.InstalledPluginsList.AllowUserToAddRows = false;
            this.InstalledPluginsList.AllowUserToDeleteRows = false;
            this.InstalledPluginsList.AllowUserToResizeColumns = false;
            this.InstalledPluginsList.AllowUserToResizeRows = false;
            this.InstalledPluginsList.AutoGenerateColumns = false;
            this.InstalledPluginsList.CausesValidation = false;
            this.InstalledPluginsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InstalledPluginsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn2,
            this.versionDataGridViewTextBoxColumn2});
            this.InstalledPluginsList.DataSource = this.installedPluginsBindingSource;
            this.InstalledPluginsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.InstalledPluginsList.Location = new System.Drawing.Point(8, 50);
            this.InstalledPluginsList.MultiSelect = false;
            this.InstalledPluginsList.Name = "InstalledPluginsList";
            this.InstalledPluginsList.ReadOnly = true;
            this.InstalledPluginsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InstalledPluginsList.Size = new System.Drawing.Size(1013, 367);
            this.InstalledPluginsList.TabIndex = 13;
            this.InstalledPluginsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InstalledPluginsList_CellContentClick);
            this.InstalledPluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.InstalledPluginList_DataBindingComplete);
            // 
            // installedPluginsBindingSource
            // 
            this.installedPluginsBindingSource.DataMember = "installedPlugins";
            this.installedPluginsBindingSource.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // Uninstall
            // 
            this.Uninstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Uninstall.Location = new System.Drawing.Point(926, 13);
            this.Uninstall.Name = "Uninstall";
            this.Uninstall.Size = new System.Drawing.Size(90, 26);
            this.Uninstall.TabIndex = 11;
            this.Uninstall.Text = "Uninstall";
            this.Uninstall.UseVisualStyleBackColor = true;
            this.Uninstall.Click += new System.EventHandler(this.Uninstall_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.Location = new System.Drawing.Point(288, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "Search";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(13, 13);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(269, 26);
            this.textBox3.TabIndex = 9;
            // 
            // shortNameDataGridViewTextBoxColumn
            // 
            this.shortNameDataGridViewTextBoxColumn.DataPropertyName = "ShortName";
            this.shortNameDataGridViewTextBoxColumn.HeaderText = "ShortName";
            this.shortNameDataGridViewTextBoxColumn.Name = "shortNameDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // versionDescriptionDataGridViewTextBoxColumn
            // 
            this.versionDescriptionDataGridViewTextBoxColumn.DataPropertyName = "VersionDescription";
            this.versionDescriptionDataGridViewTextBoxColumn.HeaderText = "VersionDescription";
            this.versionDescriptionDataGridViewTextBoxColumn.Name = "versionDescriptionDataGridViewTextBoxColumn";
            // 
            // licenseDataGridViewTextBoxColumn
            // 
            this.licenseDataGridViewTextBoxColumn.DataPropertyName = "License";
            this.licenseDataGridViewTextBoxColumn.HeaderText = "License";
            this.licenseDataGridViewTextBoxColumn.Name = "licenseDataGridViewTextBoxColumn";
            // 
            // installedVersionDataGridViewTextBoxColumn
            // 
            this.installedVersionDataGridViewTextBoxColumn.DataPropertyName = "InstalledVersion";
            this.installedVersionDataGridViewTextBoxColumn.HeaderText = "InstalledVersion";
            this.installedVersionDataGridViewTextBoxColumn.Name = "installedVersionDataGridViewTextBoxColumn";
            // 
            // shortNameDataGridViewTextBoxColumn1
            // 
            this.shortNameDataGridViewTextBoxColumn1.DataPropertyName = "ShortName";
            this.shortNameDataGridViewTextBoxColumn1.HeaderText = "ShortName";
            this.shortNameDataGridViewTextBoxColumn1.Name = "shortNameDataGridViewTextBoxColumn1";
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            // 
            // versionDescriptionDataGridViewTextBoxColumn1
            // 
            this.versionDescriptionDataGridViewTextBoxColumn1.DataPropertyName = "VersionDescription";
            this.versionDescriptionDataGridViewTextBoxColumn1.HeaderText = "VersionDescription";
            this.versionDescriptionDataGridViewTextBoxColumn1.Name = "versionDescriptionDataGridViewTextBoxColumn1";
            // 
            // licenseDataGridViewTextBoxColumn1
            // 
            this.licenseDataGridViewTextBoxColumn1.DataPropertyName = "License";
            this.licenseDataGridViewTextBoxColumn1.HeaderText = "License";
            this.licenseDataGridViewTextBoxColumn1.Name = "licenseDataGridViewTextBoxColumn1";
            // 
            // shortNameDataGridViewTextBoxColumn2
            // 
            this.shortNameDataGridViewTextBoxColumn2.DataPropertyName = "ShortName";
            this.shortNameDataGridViewTextBoxColumn2.HeaderText = "ShortName";
            this.shortNameDataGridViewTextBoxColumn2.Name = "shortNameDataGridViewTextBoxColumn2";
            // 
            // descriptionDataGridViewTextBoxColumn2
            // 
            this.descriptionDataGridViewTextBoxColumn2.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn2.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn2.Name = "descriptionDataGridViewTextBoxColumn2";
            // 
            // versionDescriptionDataGridViewTextBoxColumn2
            // 
            this.versionDescriptionDataGridViewTextBoxColumn2.DataPropertyName = "VersionDescription";
            this.versionDescriptionDataGridViewTextBoxColumn2.HeaderText = "VersionDescription";
            this.versionDescriptionDataGridViewTextBoxColumn2.Name = "versionDescriptionDataGridViewTextBoxColumn2";
            // 
            // licenseDataGridViewTextBoxColumn2
            // 
            this.licenseDataGridViewTextBoxColumn2.DataPropertyName = "License";
            this.licenseDataGridViewTextBoxColumn2.HeaderText = "License";
            this.licenseDataGridViewTextBoxColumn2.Name = "licenseDataGridViewTextBoxColumn2";
            // 
            // InstalledVersion
            // 
            this.InstalledVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InstalledVersion.DataPropertyName = "InstalledVersion";
            this.InstalledVersion.HeaderText = "InstalledVersion";
            this.InstalledVersion.Name = "InstalledVersion";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            // 
            // pluginManagerMainFormControllerBindingSource
            // 
            this.pluginManagerMainFormControllerBindingSource.DataSource = typeof(PpmMain.Controllers.PluginManagerMainFormController);
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // versionDataGridViewTextBoxColumn1
            // 
            this.versionDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.versionDataGridViewTextBoxColumn1.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn1.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn1.Name = "versionDataGridViewTextBoxColumn1";
            // 
            // nameDataGridViewTextBoxColumn2
            // 
            this.nameDataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn2.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn2.Name = "nameDataGridViewTextBoxColumn2";
            this.nameDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // versionDataGridViewTextBoxColumn2
            // 
            this.versionDataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.versionDataGridViewTextBoxColumn2.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn2.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn2.Name = "versionDataGridViewTextBoxColumn2";
            this.versionDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // PluginManagerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 672);
            this.Controls.Add(this.PluginTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginManagerMainForm";
            this.Text = "Paratext Plugin Manager";
            this.Load += new System.EventHandler(this.PluginManagerMainForm_Load);
            this.PluginTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailablePluginsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.availablePluginsBindingSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdatedPluginsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstalledPluginsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pluginManagerMainFormControllerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl PluginTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button Uninstall;
        private System.Windows.Forms.DataGridView AvailablePluginsList;
        private System.Windows.Forms.TextBox PluginDescriptionAvailable;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DataGridView UpdatedPluginsList;
        private System.Windows.Forms.TextBox PluginDescriptionInstalled;
        private System.Windows.Forms.DataGridView InstalledPluginsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn shortNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn licenseDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource availablePluginsBindingSource;
        private System.Windows.Forms.BindingSource pluginManagerMainFormControllerBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn installedVersionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shortNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDescriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn licenseDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource outdatedPluginsBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn shortNameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDescriptionDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn licenseDataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource installedPluginsBindingSource;
        private System.Windows.Forms.BindingSource outdatedPluginsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstalledVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn2;
    }
}