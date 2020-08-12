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
            RefreshInstalled();
            RefreshAvailable();
        }

        private void InstalledPluginsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Uninstall.Enabled) Uninstall.Enabled = true;
            PluginDescriptionInstalled.Text = Controller.InstalledPlugins[e.RowIndex].Description;
        }

        private void AvailablePluginsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Uninstall.Enabled) Install.Enabled = true;
            PluginDescriptionAvailable.Text = Controller.InstalledPlugins[e.RowIndex].Description;
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.InstalledPlugins[InstalledPluginsList.CurrentCell.RowIndex];

            DialogResult confirmUninstall = MessageBox.Show($"Are you sure you wish to uninstall {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Uninstalling {selectedPlugin.ShortName.ToUpper()}",
                                     MessageBoxButtons.YesNo);
            if (confirmUninstall == DialogResult.Yes)
            {
                Controller.UninstallPlugin(selectedPlugin);
                RefreshAvailable();
                RefreshInstalled();
            }
        }

        private void Install_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.AvailablePlugins[AvailablePluginsList.CurrentCell.RowIndex];

            DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to install {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Installing {selectedPlugin.ShortName.ToUpper()}",
                                     MessageBoxButtons.YesNo);
            if (confirmInstall == DialogResult.Yes)
            {
                Controller.InstallPlugin(selectedPlugin);
                RefreshAvailable();
                RefreshInstalled();
            }
        }

        private void InstalledPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            InstalledPluginsList.ClearSelection();
            PluginDescriptionInstalled.Clear();
            Uninstall.Enabled = false;
        }

        private void AvailablePluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AvailablePluginsList.ClearSelection();
            PluginDescriptionAvailable.Clear();
            Install.Enabled = false;
        }

        private void RefreshInstalled()
        {
            InstalledPluginsList.DataSource = null;
            InstalledPluginsList.DataSource = Controller.InstalledPlugins;
        }
        private void RefreshAvailable()
        {
            AvailablePluginsList.DataSource = null;
            AvailablePluginsList.DataSource = Controller.AvailablePlugins;
        }
    }
}
