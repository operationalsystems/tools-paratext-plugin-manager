using PpmMain.Controllers;
using PpmMain.Models;
using System;
using System.Windows.Forms;

namespace PpmMain
{
    public partial class PluginManagerMainForm : Form
    {
        PluginManagerMainFormController Controller { get; set; }

        public PluginManagerMainForm()
        {
            InitializeComponent();
        }

        private void PluginManagerMainForm_Load(object sender, EventArgs e)
        {
            Controller = new PluginManagerMainFormController();
            RefreshAll();
        }

        private void AvailablePluginsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Install.Enabled) Install.Enabled = true;
            PluginDescriptionAvailable.Text = Controller.AvailablePlugins[e.RowIndex].Description;
        }

        private void UpdatedPluginsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!UpdateOne.Enabled) UpdateOne.Enabled = true;
            PluginDescriptionUpdated.Text = Controller.UpdatedPlugins[e.RowIndex].Description;
        }

        private void InstalledPluginsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Uninstall.Enabled) Uninstall.Enabled = true;
            PluginDescriptionInstalled.Text = Controller.InstalledPlugins[e.RowIndex].Description;
        }

        private void Install_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.AvailablePlugins[AvailablePluginsList.CurrentCell.RowIndex];

            DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to install {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Confirm Plugin Install",
                                     MessageBoxButtons.YesNo);
            if (confirmInstall == DialogResult.Yes)
            {
                Controller.InstallPlugin(selectedPlugin);
                RefreshAll();
                MessageBox.Show($"{selectedPlugin.Name} ({selectedPlugin.Version}) has been installed.",
                                     $"Plugin Installed",
                                     MessageBoxButtons.OK);
            }
        }

        private void UpdateOne_Click(object sender, EventArgs e)
        {
            UpdatedPlugin selectedPlugin = Controller.UpdatedPlugins[UpdatedPluginsList.CurrentCell.RowIndex];

            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you wish to update {selectedPlugin.Name} from version {selectedPlugin.InstalledVersion} to {selectedPlugin.Version}?",
                                     $"Confirm Plugin Update",
                                     MessageBoxButtons.YesNo);
            if (confirmUpdate == DialogResult.Yes)
            {
                Controller.UpdatePlugins(new System.Collections.Generic.List<UpdatedPlugin>() { selectedPlugin });
                RefreshAll();
                MessageBox.Show($"{selectedPlugin.Name} has been updated to version {selectedPlugin.Version}.",
                                     $"Plugin Updated",
                                     MessageBoxButtons.OK);
            }
        }

        private void UpdateAll_Click(object sender, EventArgs e)
        {
            int pluginCount = Controller.UpdatedPlugins.Count;
            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you wish to update {pluginCount} plugins?",
                                     $"Confirm Update All",
                                     MessageBoxButtons.YesNo);
            if (confirmUpdate == DialogResult.Yes)
            {
                Controller.UpdatePlugins(Controller.UpdatedPlugins);
                RefreshAll();
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
                Controller.UninstallPlugin(selectedPlugin);
                RefreshAll();
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

        private void UpdatedPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            UpdatedPluginsList.ClearSelection();
            PluginDescriptionUpdated.Clear();
            UpdateOne.Enabled = false;
        }

        private void InstalledPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            InstalledPluginsList.ClearSelection();
            PluginDescriptionInstalled.Clear();
            Uninstall.Enabled = false;
        }

        private void RefreshAll()
        {
            RefreshAvailable();
            RefreshUpdated();
            RefreshInstalled();
        }

        private void RefreshAvailable()
        {
            AvailablePluginsList.DataSource = Controller.AvailablePlugins;
        }

        private void RefreshUpdated()
        {
            UpdatedPluginsList.DataSource = Controller.UpdatedPlugins;
        }

        private void RefreshInstalled()
        {
            InstalledPluginsList.DataSource = Controller.InstalledPlugins;
        }
    }
}
