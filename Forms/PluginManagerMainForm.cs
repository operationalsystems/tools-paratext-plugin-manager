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
            RefreshBindings();
        }

        private void AvailablePluginsList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Install.Enabled) Install.Enabled = true;
            if (AvailablePluginsList.SelectedRows.Count == 0) return;
            int index = AvailablePluginsList.SelectedRows[0].Index;
            if (index < 0) return;
            PluginDescriptionAvailable.Text = Controller.AvailablePlugins[index].Description;
        }

        private void OutdatedPluginsList_SelectionChanged(object sender, EventArgs e)
        {
            if (!UpdateOne.Enabled) UpdateOne.Enabled = true;
            if (OutdatedPluginsList.SelectedRows.Count == 0) return;
            int index = OutdatedPluginsList.SelectedRows[0].Index;
            if (index < 0) return;
            PluginDescriptionOutdated.Text = Controller.OutdatedPlugins[index].Description;
        }

        private void InstalledPluginsList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Uninstall.Enabled) Uninstall.Enabled = true;
            if (InstalledPluginsList.SelectedRows.Count == 0) return;
            int index = InstalledPluginsList.SelectedRows[0].Index;
            if (index < 0) return;
            PluginDescriptionInstalled.Text = Controller.InstalledPlugins[index].Description;
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
                RefreshBindings();
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
                Controller.UpdatePlugins(new System.Collections.Generic.List<OutdatedPlugin>() { selectedPlugin });
                RefreshBindings();
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
                Controller.UpdatePlugins(Controller.OutdatedPlugins);
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
                Controller.UninstallPlugin(selectedPlugin);
                RefreshBindings();
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
    }
}
