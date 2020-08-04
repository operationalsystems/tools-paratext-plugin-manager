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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManagerMainForm));
            this.testGetLatestPluginsButton = new System.Windows.Forms.Button();
            this.testGetAllPluginsButton = new System.Windows.Forms.Button();
            this.testOutputTextbox = new System.Windows.Forms.TextBox();
            this.openPpmDownloadFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testGetLatestPluginsButton
            // 
            this.testGetLatestPluginsButton.Location = new System.Drawing.Point(12, 38);
            this.testGetLatestPluginsButton.Name = "testGetLatestPluginsButton";
            this.testGetLatestPluginsButton.Size = new System.Drawing.Size(293, 26);
            this.testGetLatestPluginsButton.TabIndex = 1;
            this.testGetLatestPluginsButton.Text = "Test Get Latest Plugins (Download all listed TPT)";
            this.testGetLatestPluginsButton.UseVisualStyleBackColor = true;
            this.testGetLatestPluginsButton.Click += new System.EventHandler(this.testGetLatestPluginsButton_Click);
            // 
            // testGetAllPluginsButton
            // 
            this.testGetAllPluginsButton.Location = new System.Drawing.Point(12, 82);
            this.testGetAllPluginsButton.Name = "testGetAllPluginsButton";
            this.testGetAllPluginsButton.Size = new System.Drawing.Size(293, 26);
            this.testGetAllPluginsButton.TabIndex = 2;
            this.testGetAllPluginsButton.Text = "Test Get All Plugins (Download all listed TPT)";
            this.testGetAllPluginsButton.UseVisualStyleBackColor = true;
            this.testGetAllPluginsButton.Click += new System.EventHandler(this.testGetAllPluginsButton_Click);
            // 
            // testOutputTextbox
            // 
            this.testOutputTextbox.Location = new System.Drawing.Point(319, 38);
            this.testOutputTextbox.Multiline = true;
            this.testOutputTextbox.Name = "testOutputTextbox";
            this.testOutputTextbox.Size = new System.Drawing.Size(469, 400);
            this.testOutputTextbox.TabIndex = 3;
            // 
            // openPpmDownloadFolderButton
            // 
            this.openPpmDownloadFolderButton.Location = new System.Drawing.Point(13, 155);
            this.openPpmDownloadFolderButton.Name = "openPpmDownloadFolderButton";
            this.openPpmDownloadFolderButton.Size = new System.Drawing.Size(292, 32);
            this.openPpmDownloadFolderButton.TabIndex = 4;
            this.openPpmDownloadFolderButton.Text = "Open Download Folder";
            this.openPpmDownloadFolderButton.UseVisualStyleBackColor = true;
            this.openPpmDownloadFolderButton.Click += new System.EventHandler(this.openPpmDownloadFolderButton_Click);
            // 
            // PluginManagerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.openPpmDownloadFolderButton);
            this.Controls.Add(this.testOutputTextbox);
            this.Controls.Add(this.testGetAllPluginsButton);
            this.Controls.Add(this.testGetLatestPluginsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginManagerMainForm";
            this.Text = "Paratext Plugin Manager";
            this.Load += new System.EventHandler(this.PluginManagerMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testGetLatestPluginsButton;
        private System.Windows.Forms.Button testGetAllPluginsButton;
        private System.Windows.Forms.TextBox testOutputTextbox;
        private System.Windows.Forms.Button openPpmDownloadFolderButton;
    }
}