using PpmMain.Controllers;
using PpmMain.Models;
using PpmMain.Util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PpmMain
{
    public partial class PluginManagerMainForm : Form
    {
        PluginManagerMainFormController Controller { get; set; }

        public PluginManagerMainForm()
        {
            InitializeComponent();
            CopyrightLabel.Text = MainConsts.CopyrightText;
        }

        private void PluginManagerMainForm_Load(object sender, EventArgs e)
        {
            Controller = new PluginManagerMainFormController();
            RefreshBindings();
        }

        /// <summary>
        /// This method handles selecting an plugin from a list.
        /// </summary>
        /// <param name="sender">The list that had a selection change event.</param>
        /// <param name="e">The selection change event.</param>
        private void AnyPluginsList_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            List<PluginDescription> source = (List<PluginDescription>)grid.DataSource;
            
            if (grid.SelectedRows.Count == 0) return;
            int index = grid.SelectedRows[0].Index;
            if (index < 0) return;

            switch (grid.Name)
            {
                case "AvailablePluginsList":
                    {
                        if (!Install.Enabled) Install.Enabled = true;
                        PluginDescriptionAvailable.Text = source[index].Description;
                        break;
                    }
                case "OutdatedPluginsList":
                    {
                        if (!UpdateOne.Enabled) UpdateOne.Enabled = true;
                        PluginDescriptionOutdated.Text = source[index].Description;
                        break;
                    }
                case "InstalledPluginsList":
                    {
                        if (!Uninstall.Enabled) Uninstall.Enabled = true;
                        PluginDescriptionInstalled.Text = source[index].Description;
                        break;
                    }
            }
        }

        private void Install_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.AvailablePlugins[AvailablePluginsList.CurrentCell.RowIndex];

            DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to install {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Confirm Plugin Install",
                                     MessageBoxButtons.YesNo);
            if (confirmInstall == DialogResult.Yes)
            {
                FormProgress.Visible = true;
                ProgressLabel.Visible = true;
                ProgressLabel.Text = MainConsts.ProgressBarInstalling;

                Controller.InstallPlugin(selectedPlugin);
                RefreshBindings();

                ProgressLabel.Text = "";
                ProgressLabel.Visible = false;
                FormProgress.Visible = false;

                MessageBox.Show($"{selectedPlugin.Name} ({selectedPlugin.Version}) has been installed.",
                                     $"Plugin Installed",
                                     MessageBoxButtons.OK);
            }
        }

        private void UpdateOne_Click(object sender, EventArgs e)
        {
            OutdatedPlugin selectedPlugin = Controller.OutdatedPlugins[OutdatedPluginsList.CurrentCell.RowIndex];

            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you wish to update {selectedPlugin.Name} from version {selectedPlugin.InstalledVersion} to {selectedPlugin.Version}?",
                                     $"Confirm Plugin Update",
                                     MessageBoxButtons.YesNo);
            if (confirmUpdate == DialogResult.Yes)
            {
                FormProgress.Visible = true;
                ProgressLabel.Visible = true;
                ProgressLabel.Text = MainConsts.ProgressBarUpdating;


                Controller.UpdatePlugins(new System.Collections.Generic.List<OutdatedPlugin>() { selectedPlugin });
                RefreshBindings();

                ProgressLabel.Text = "";
                ProgressLabel.Visible = false;
                FormProgress.Visible = false;

                MessageBox.Show($"{selectedPlugin.Name} has been updated to version {selectedPlugin.Version}.",
                                     $"Plugin Updated",
                                     MessageBoxButtons.OK);
            }
        }

        private void UpdateAll_Click(object sender, EventArgs e)
        {
            int pluginCount = Controller.OutdatedPlugins.Count;
            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you wish to update {pluginCount} plugins?",
                                     $"Confirm Update All",
                                     MessageBoxButtons.YesNo);
            if (confirmUpdate == DialogResult.Yes)
            {
                FormProgress.Visible = true;
                ProgressLabel.Visible = true;
                ProgressLabel.Text = MainConsts.ProgressBarUpdating;

                Controller.UpdatePlugins(Controller.OutdatedPlugins);
                RefreshBindings();

                ProgressLabel.Text = "";
                ProgressLabel.Visible = false;
                FormProgress.Visible = false;

                RefreshBindings();
                MessageBox.Show($"All plugins have been updated.",
                                     $"All Plugins Updated",
                                     MessageBoxButtons.OK);
            }
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.InstalledPlugins[InstalledPluginsList.CurrentCell.RowIndex];

            DialogResult confirmUninstall = MessageBox.Show($"Are you sure you wish to uninstall {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Confirm Plugin Uninstall",
                                     MessageBoxButtons.YesNo);
            if (confirmUninstall == DialogResult.Yes)
            {
                FormProgress.Visible = true;
                ProgressLabel.Visible = true;
                ProgressLabel.Text = MainConsts.ProgressBarUninstalling;

                Controller.UninstallPlugin(selectedPlugin);
                RefreshBindings();

                ProgressLabel.Text = "";
                ProgressLabel.Visible = false;
                FormProgress.Visible = false;

                MessageBox.Show($"{selectedPlugin.Name} ({selectedPlugin.Version}) has been uninstalled.",
                     $"Plugin Uninstalled",
                     MessageBoxButtons.OK);
            }
        }

        private void AvailablePluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AvailablePluginsList.ClearSelection();
            PluginDescriptionAvailable.Clear();
            Install.Enabled = false;
        }

        private void OutdatedPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            OutdatedPluginsList.ClearSelection();
            PluginDescriptionOutdated.Clear();
            UpdateOne.Enabled = false;
        }

        private void InstalledPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            InstalledPluginsList.ClearSelection();
            PluginDescriptionInstalled.Clear();
            Uninstall.Enabled = false;
        }

        private void RefreshBindings()
        {
            AvailablePluginsList.DataSource = Controller.AvailablePlugins;
            OutdatedPluginsList.DataSource = Controller.OutdatedPlugins;
            InstalledPluginsList.DataSource = Controller.InstalledPlugins;
        }

        private void InstalledPluginsList_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OutdatedPluginsList_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
