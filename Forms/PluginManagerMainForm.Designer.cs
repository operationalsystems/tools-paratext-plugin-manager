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
            this.Available = new System.Windows.Forms.TabPage();
            this.PluginDescriptionAvailable = new System.Windows.Forms.TextBox();
            this.AvailablePluginsList = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.availablePluginsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pluginManagerMainFormControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Install = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Updates = new System.Windows.Forms.TabPage();
            this.PluginDescriptionOutdated = new System.Windows.Forms.TextBox();
            this.OutdatedPluginsList = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstalledVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outdatedPluginsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.UpdateOne = new System.Windows.Forms.Button();
            this.UpdateAll = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Installed = new System.Windows.Forms.TabPage();
            this.PluginDescriptionInstalled = new System.Windows.Forms.TextBox();
            this.InstalledPluginsList = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.PluginTabs.SuspendLayout();
            this.Available.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailablePluginsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.availablePluginsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pluginManagerMainFormControllerBindingSource)).BeginInit();
            this.Updates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutdatedPluginsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource1)).BeginInit();
            this.Installed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstalledPluginsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PluginTabs
            // 
            this.PluginTabs.Controls.Add(this.Available);
            this.PluginTabs.Controls.Add(this.Updates);
            this.PluginTabs.Controls.Add(this.Installed);
            this.PluginTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginTabs.Location = new System.Drawing.Point(0, 0);
            this.PluginTabs.Name = "PluginTabs";
            this.PluginTabs.SelectedIndex = 0;
            this.PluginTabs.Size = new System.Drawing.Size(1037, 672);
            this.PluginTabs.TabIndex = 5;
            // 
            // Available
            // 
            this.Available.Controls.Add(this.PluginDescriptionAvailable);
            this.Available.Controls.Add(this.AvailablePluginsList);
            this.Available.Controls.Add(this.Install);
            this.Available.Controls.Add(this.button1);
            this.Available.Controls.Add(this.textBox1);
            this.Available.Location = new System.Drawing.Point(4, 29);
            this.Available.Name = "Available";
            this.Available.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.Available.Size = new System.Drawing.Size(1029, 639);
            this.Available.TabIndex = 0;
            this.Available.Text = "Available";
            this.Available.UseVisualStyleBackColor = true;
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
            this.AvailablePluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.AvailablePluginList_DataBindingComplete);
            this.AvailablePluginsList.SelectionChanged += new System.EventHandler(this.AvailablePluginsList_SelectionChanged);
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
            // availablePluginsBindingSource
            // 
            this.availablePluginsBindingSource.DataMember = "availablePlugins";
            this.availablePluginsBindingSource.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // pluginManagerMainFormControllerBindingSource
            // 
            this.pluginManagerMainFormControllerBindingSource.DataSource = typeof(PpmMain.Controllers.PluginManagerMainFormController);
            // 
            // Install
            // 
            this.Install.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Install.Location = new System.Drawing.Point(926, 13);
            this.Install.Name = "Install";
            this.Install.Size = new System.Drawing.Size(90, 26);
            this.Install.TabIndex = 9;
            this.Install.Text = "Install";
            this.Install.UseVisualStyleBackColor = true;
            this.Install.Click += new System.EventHandler(this.Install_Click);
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
            // Updates
            // 
            this.Updates.Controls.Add(this.PluginDescriptionOutdated);
            this.Updates.Controls.Add(this.OutdatedPluginsList);
            this.Updates.Controls.Add(this.UpdateOne);
            this.Updates.Controls.Add(this.UpdateAll);
            this.Updates.Controls.Add(this.button2);
            this.Updates.Controls.Add(this.textBox2);
            this.Updates.Location = new System.Drawing.Point(4, 29);
            this.Updates.Name = "Updates";
            this.Updates.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.Updates.Size = new System.Drawing.Size(1029, 639);
            this.Updates.TabIndex = 1;
            this.Updates.Text = "Updates";
            this.Updates.UseVisualStyleBackColor = true;
            // 
            // PluginDescriptionOutdated
            // 
            this.PluginDescriptionOutdated.Location = new System.Drawing.Point(8, 423);
            this.PluginDescriptionOutdated.Multiline = true;
            this.PluginDescriptionOutdated.Name = "PluginDescriptionOutdated";
            this.PluginDescriptionOutdated.Size = new System.Drawing.Size(1013, 208);
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
            this.OutdatedPluginsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OutdatedPluginsList_SelectionChanged);
            this.OutdatedPluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.OutdatedPluginList_DataBindingComplete);
            this.OutdatedPluginsList.SelectionChanged += new System.EventHandler(this.OutdatedPluginsList_SelectionChanged);
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
            // outdatedPluginsBindingSource1
            // 
            this.outdatedPluginsBindingSource1.DataMember = "outdatedPlugins";
            this.outdatedPluginsBindingSource1.DataSource = this.pluginManagerMainFormControllerBindingSource;
            // 
            // UpdateOne
            // 
            this.UpdateOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UpdateOne.Location = new System.Drawing.Point(840, 13);
            this.UpdateOne.Name = "UpdateOne";
            this.UpdateOne.Size = new System.Drawing.Size(80, 26);
            this.UpdateOne.TabIndex = 12;
            this.UpdateOne.Text = "Update";
            this.UpdateOne.UseVisualStyleBackColor = true;
            this.UpdateOne.Click += new System.EventHandler(this.UpdateOne_Click);
            // 
            // UpdateAll
            // 
            this.UpdateAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UpdateAll.Location = new System.Drawing.Point(926, 13);
            this.UpdateAll.Name = "UpdateAll";
            this.UpdateAll.Size = new System.Drawing.Size(90, 26);
            this.UpdateAll.TabIndex = 11;
            this.UpdateAll.Text = "Update All";
            this.UpdateAll.UseVisualStyleBackColor = true;
            this.UpdateAll.Click += new System.EventHandler(this.UpdateAll_Click);
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
            // Installed
            // 
            this.Installed.Controls.Add(this.PluginDescriptionInstalled);
            this.Installed.Controls.Add(this.InstalledPluginsList);
            this.Installed.Controls.Add(this.Uninstall);
            this.Installed.Controls.Add(this.button3);
            this.Installed.Controls.Add(this.textBox3);
            this.Installed.Location = new System.Drawing.Point(4, 29);
            this.Installed.Name = "Installed";
            this.Installed.Padding = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.Installed.Size = new System.Drawing.Size(1029, 639);
            this.Installed.TabIndex = 2;
            this.Installed.Text = "Installed";
            this.Installed.UseVisualStyleBackColor = true;
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
            this.InstalledPluginsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InstalledPluginsList_SelectionChanged);
            this.InstalledPluginsList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.InstalledPluginList_DataBindingComplete);
            this.InstalledPluginsList.SelectionChanged += new System.EventHandler(this.InstalledPluginsList_SelectionChanged);
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
            this.Available.ResumeLayout(false);
            this.Available.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailablePluginsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.availablePluginsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pluginManagerMainFormControllerBindingSource)).EndInit();
            this.Updates.ResumeLayout(false);
            this.Updates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutdatedPluginsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource1)).EndInit();
            this.Installed.ResumeLayout(false);
            this.Installed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstalledPluginsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outdatedPluginsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl PluginTabs;
        private System.Windows.Forms.TabPage Available;
        private System.Windows.Forms.TabPage Updates;
        private System.Windows.Forms.TabPage Installed;
        private System.Windows.Forms.Button Install;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button UpdateOne;
        private System.Windows.Forms.Button UpdateAll;
        private System.Windows.Forms.Button Uninstall;
        private System.Windows.Forms.DataGridView AvailablePluginsList;
        private System.Windows.Forms.TextBox PluginDescriptionAvailable;
        private System.Windows.Forms.TextBox PluginDescriptionOutdated;
        private System.Windows.Forms.DataGridView OutdatedPluginsList;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstalledVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn1;
    }
}