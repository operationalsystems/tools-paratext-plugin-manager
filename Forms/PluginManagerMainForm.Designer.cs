using PpmMain.Util;

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
            this.availablePluginsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pluginManagerMainFormControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.outdatedPluginsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.installedPluginsBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.FormProgress = new System.Windows.Forms.ProgressBar();
            this.Installed = new System.Windows.Forms.TabPage();
            this.PluginDescriptionInstalled = new System.Windows.Forms.TextBox();
            this.InstalledPluginsList = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uninstall = new System.Windows.Forms.Button();
            this.Updates = new System.Windows.Forms.TabPage();
            this.PluginDescriptionOutdated = new System.Windows.Forms.TextBox();
            this.OutdatedPluginsList = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstalledVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateOne = new System.Windows.Forms.Button();
            this.UpdateAll = new System.Windows.Forms.Button();
            this.Available = new System.Windows.Forms.TabPage();
            this.PluginDescriptionAvailable = new System.Windows.Forms.TextBox();
            this.AvailablePluginsList = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Install = new System.Windows.Forms.Button();
            this.PluginTabs = new System.Windows.Forms.TabControl();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.availablePluginsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pluginManagerMainFormControllerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource)).BeginInit();
            this.Installed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstalledPluginsList)).BeginInit();
            this.Updates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutdatedPluginsList)).BeginInit();
            this.Available.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailablePluginsList)).BeginInit();
            this.PluginTabs.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // availablePluginsBindingSource
            // 
            this.availablePluginsBindingSource.DataMember = "availablePlugins";
            this.availablePluginsBindingSource.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // pluginManagerMainFormControllerBindingSource
            // 
            this.pluginManagerMainFormControllerBindingSource.DataSource = typeof(PpmMain.Controllers.PluginManagerMainFormController);
            // 
            // outdatedPluginsBindingSource1
            // 
            this.outdatedPluginsBindingSource1.DataMember = "outdatedPlugins";
            this.outdatedPluginsBindingSource1.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // installedPluginsBindingSource
            // 
            this.installedPluginsBindingSource.DataMember = "installedPlugins";
            this.installedPluginsBindingSource.DataSource = this.pluginManagerMainFormControllerBindingSource;
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
            // FormProgress
            // 
            this.FormProgress.Location = new System.Drawing.Point(875, 648);
            this.FormProgress.Name = "FormProgress";
            this.FormProgress.Size = new System.Drawing.Size(150, 15);
            this.FormProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.FormProgress.TabIndex = 6;
            this.FormProgress.Visible = false;
            // 
            // Installed
            // 
            this.Installed.Controls.Add(this.PluginDescriptionInstalled);
            this.Installed.Controls.Add(this.InstalledPluginsList);
            this.Installed.Controls.Add(this.Uninstall);
            this.Installed.Location = new System.Drawing.Point(4, 29);
            this.Installed.Name = "Installed";
            this.Installed.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.Installed.Size = new System.Drawing.Size(1027, 619);
            this.Installed.TabIndex = 2;
            this.Installed.Text = "Installed";
            this.Installed.UseVisualStyleBackColor = true;
            // 
            // PluginDescriptionInstalled
            // 
            this.PluginDescriptionInstalled.Location = new System.Drawing.Point(8, 423);
            this.PluginDescriptionInstalled.Multiline = true;
            this.PluginDescriptionInstalled.Name = "PluginDescriptionInstalled";
            this.PluginDescriptionInstalled.Size = new System.Drawing.Size(1013, 166);
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
            this.InstalledPluginsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AnyPluginList_SelectionChanged);
            this.InstalledPluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.AnyPluginList_DataBindingComplete);
            this.InstalledPluginsList.SelectionChanged += new System.EventHandler(this.AnyPluginList_SelectionChanged);
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
            // Uninstall
            // 
            this.Uninstall.Enabled = false;
            this.Uninstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Uninstall.Location = new System.Drawing.Point(931, 13);
            this.Uninstall.Name = "Uninstall";
            this.Uninstall.Size = new System.Drawing.Size(90, 26);
            this.Uninstall.TabIndex = 11;
            this.Uninstall.Text = "Uninstall";
            this.Uninstall.UseVisualStyleBackColor = true;
            this.Uninstall.Click += new System.EventHandler(this.Uninstall_Click);
            // 
            // Updates
            // 
            this.Updates.Controls.Add(this.PluginDescriptionOutdated);
            this.Updates.Controls.Add(this.OutdatedPluginsList);
            this.Updates.Controls.Add(this.UpdateOne);
            this.Updates.Controls.Add(this.UpdateAll);
            this.Updates.Location = new System.Drawing.Point(4, 29);
            this.Updates.Name = "Updates";
            this.Updates.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.Updates.Size = new System.Drawing.Size(1027, 619);
            this.Updates.TabIndex = 1;
            this.Updates.Text = "Updates";
            this.Updates.UseVisualStyleBackColor = true;
            // 
            // PluginDescriptionOutdated
            // 
            this.PluginDescriptionOutdated.Location = new System.Drawing.Point(8, 423);
            this.PluginDescriptionOutdated.Multiline = true;
            this.PluginDescriptionOutdated.Name = "PluginDescriptionOutdated";
            this.PluginDescriptionOutdated.Size = new System.Drawing.Size(1013, 166);
            this.PluginDescriptionOutdated.TabIndex = 14;
            // 
            // OutdatedPluginsList
            // 
            this.OutdatedPluginsList.AllowUserToAddRows = false;
            this.OutdatedPluginsList.AllowUserToDeleteRows = false;
            this.OutdatedPluginsList.AllowUserToOrderColumns = true;
            this.OutdatedPluginsList.AllowUserToResizeColumns = false;
            this.OutdatedPluginsList.AllowUserToResizeRows = false;
            this.OutdatedPluginsList.AutoGenerateColumns = false;
            this.OutdatedPluginsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OutdatedPluginsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.InstalledVersion,
            this.versionDataGridViewTextBoxColumn1});
            this.OutdatedPluginsList.DataSource = this.outdatedPluginsBindingSource1;
            this.OutdatedPluginsList.Location = new System.Drawing.Point(8, 50);
            this.OutdatedPluginsList.Margin = new System.Windows.Forms.Padding(0);
            this.OutdatedPluginsList.MultiSelect = false;
            this.OutdatedPluginsList.Name = "OutdatedPluginsList";
            this.OutdatedPluginsList.ReadOnly = true;
            this.OutdatedPluginsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OutdatedPluginsList.Size = new System.Drawing.Size(1013, 367);
            this.OutdatedPluginsList.TabIndex = 13;
            this.OutdatedPluginsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AnyPluginList_SelectionChanged);
            this.OutdatedPluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.AnyPluginList_DataBindingComplete);
            this.OutdatedPluginsList.SelectionChanged += new System.EventHandler(this.AnyPluginList_SelectionChanged);
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // InstalledVersion
            // 
            this.InstalledVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InstalledVersion.DataPropertyName = "InstalledVersion";
            this.InstalledVersion.HeaderText = "Installed Version";
            this.InstalledVersion.Name = "InstalledVersion";
            this.InstalledVersion.ReadOnly = true;
            // 
            // versionDataGridViewTextBoxColumn1
            // 
            this.versionDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.versionDataGridViewTextBoxColumn1.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn1.HeaderText = "New Version";
            this.versionDataGridViewTextBoxColumn1.Name = "versionDataGridViewTextBoxColumn1";
            this.versionDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // UpdateOne
            // 
            this.UpdateOne.Enabled = false;
            this.UpdateOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UpdateOne.Location = new System.Drawing.Point(845, 13);
            this.UpdateOne.Name = "UpdateOne";
            this.UpdateOne.Size = new System.Drawing.Size(80, 26);
            this.UpdateOne.TabIndex = 12;
            this.UpdateOne.Text = "Update";
            this.UpdateOne.UseVisualStyleBackColor = true;
            this.UpdateOne.Click += new System.EventHandler(this.UpdateOne_Click);
            // 
            // UpdateAll
            // 
            this.UpdateAll.Enabled = false;
            this.UpdateAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UpdateAll.Location = new System.Drawing.Point(931, 13);
            this.UpdateAll.Name = "UpdateAll";
            this.UpdateAll.Size = new System.Drawing.Size(90, 26);
            this.UpdateAll.TabIndex = 11;
            this.UpdateAll.Text = "Update All";
            this.UpdateAll.UseVisualStyleBackColor = true;
            this.UpdateAll.Click += new System.EventHandler(this.UpdateAll_Click);
            // 
            // Available
            // 
            this.Available.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Available.Controls.Add(this.PluginDescriptionAvailable);
            this.Available.Controls.Add(this.AvailablePluginsList);
            this.Available.Controls.Add(this.Install);
            this.Available.Location = new System.Drawing.Point(4, 29);
            this.Available.Margin = new System.Windows.Forms.Padding(0);
            this.Available.Name = "Available";
            this.Available.Size = new System.Drawing.Size(1027, 619);
            this.Available.TabIndex = 0;
            this.Available.Text = "Available";
            this.Available.UseVisualStyleBackColor = true;
            // 
            // PluginDescriptionAvailable
            // 
            this.PluginDescriptionAvailable.Location = new System.Drawing.Point(8, 423);
            this.PluginDescriptionAvailable.Multiline = true;
            this.PluginDescriptionAvailable.Name = "PluginDescriptionAvailable";
            this.PluginDescriptionAvailable.Size = new System.Drawing.Size(1013, 166);
            this.PluginDescriptionAvailable.TabIndex = 12;
            // 
            // AvailablePluginsList
            // 
            this.AvailablePluginsList.AllowUserToAddRows = false;
            this.AvailablePluginsList.AllowUserToDeleteRows = false;
            this.AvailablePluginsList.AllowUserToResizeColumns = false;
            this.AvailablePluginsList.AllowUserToResizeRows = false;
            this.AvailablePluginsList.AutoGenerateColumns = false;
            this.AvailablePluginsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AvailablePluginsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn});
            this.AvailablePluginsList.DataSource = this.availablePluginsBindingSource;
            this.AvailablePluginsList.Location = new System.Drawing.Point(8, 50);
            this.AvailablePluginsList.MultiSelect = false;
            this.AvailablePluginsList.Name = "AvailablePluginsList";
            this.AvailablePluginsList.ReadOnly = true;
            this.AvailablePluginsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AvailablePluginsList.Size = new System.Drawing.Size(1013, 367);
            this.AvailablePluginsList.TabIndex = 11;
            this.AvailablePluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.AnyPluginList_DataBindingComplete);
            this.AvailablePluginsList.SelectionChanged += new System.EventHandler(this.AnyPluginList_SelectionChanged);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Install
            // 
            this.Install.Enabled = false;
            this.Install.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Install.Location = new System.Drawing.Point(931, 13);
            this.Install.Name = "Install";
            this.Install.Size = new System.Drawing.Size(90, 26);
            this.Install.TabIndex = 9;
            this.Install.Text = "Install";
            this.Install.UseVisualStyleBackColor = true;
            this.Install.Click += new System.EventHandler(this.Install_Click);
            // 
            // PluginTabs
            // 
            this.PluginTabs.Controls.Add(this.Available);
            this.PluginTabs.Controls.Add(this.Updates);
            this.PluginTabs.Controls.Add(this.Installed);
            this.PluginTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.PluginTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginTabs.Location = new System.Drawing.Point(0, 24);
            this.PluginTabs.Margin = new System.Windows.Forms.Padding(0);
            this.PluginTabs.Name = "PluginTabs";
            this.PluginTabs.Padding = new System.Drawing.Point(0, 0);
            this.PluginTabs.SelectedIndex = 0;
            this.PluginTabs.Size = new System.Drawing.Size(1035, 652);
            this.PluginTabs.TabIndex = 5;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(769, 648);
            this.ProgressLabel.MinimumSize = new System.Drawing.Size(100, 15);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(100, 15);
            this.ProgressLabel.TabIndex = 7;
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ProgressLabel.Visible = false;
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.Location = new System.Drawing.Point(9, 650);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(101, 13);
            this.CopyrightLabel.TabIndex = 8;
            this.CopyrightLabel.Text = "© 2020 Biblica, Inc.";
            // 
            // SearchText
            // 
            this.SearchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.SearchText.Location = new System.Drawing.Point(12, 68);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(269, 24);
            this.SearchText.TabIndex = 10;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            this.SearchText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchText_KeyUp);
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SearchButton.Location = new System.Drawing.Point(291, 67);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(80, 26);
            this.SearchButton.TabIndex = 16;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1035, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LicenseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // LicenseToolStripMenuItem
            // 
            this.LicenseToolStripMenuItem.Name = "LicenseToolStripMenuItem";
            this.LicenseToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.LicenseToolStripMenuItem.Text = "License";
            this.LicenseToolStripMenuItem.Click += new System.EventHandler(this.LicenseToolStripMenuItem_Click);
            // 
            // PluginManagerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1035, 678);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.FormProgress);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.PluginTabs);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PluginManagerMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paratext Plugin Manager";
            this.Load += new System.EventHandler(this.PluginManagerMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.availablePluginsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pluginManagerMainFormControllerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource)).EndInit();
            this.Installed.ResumeLayout(false);
            this.Installed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstalledPluginsList)).EndInit();
            this.Updates.ResumeLayout(false);
            this.Updates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutdatedPluginsList)).EndInit();
            this.Available.ResumeLayout(false);
            this.Available.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailablePluginsList)).EndInit();
            this.PluginTabs.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.ProgressBar FormProgress;
        private System.Windows.Forms.TabPage Installed;
        private System.Windows.Forms.TextBox PluginDescriptionInstalled;
        private System.Windows.Forms.DataGridView InstalledPluginsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button Uninstall;
        private System.Windows.Forms.TabPage Updates;
        private System.Windows.Forms.TextBox PluginDescriptionOutdated;
        private System.Windows.Forms.DataGridView OutdatedPluginsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstalledVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button UpdateOne;
        private System.Windows.Forms.Button UpdateAll;
        private System.Windows.Forms.TabPage Available;
        private System.Windows.Forms.TextBox PluginDescriptionAvailable;
        private System.Windows.Forms.DataGridView AvailablePluginsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button Install;
        private System.Windows.Forms.TabControl PluginTabs;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.TextBox SearchText;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LicenseToolStripMenuItem;
    }
}