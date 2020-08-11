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
            InstalledPluginsList.DataSource = Controller.installedPlugins;
        }

        private void InstalledPluginsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Uninstall.Enabled) Uninstall.Enabled = true;
            PluginDescriptionInstalled.Text = Controller.installedPlugins[e.RowIndex].Description;
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.installedPlugins[InstalledPluginsList.CurrentCell.RowIndex];

            DialogResult confirmUninstall = MessageBox.Show($"Are you sure you wish to uninstall {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Uninstalling {selectedPlugin.ShortName.ToUpper()}",
                                     MessageBoxButtons.YesNo);
            if (confirmUninstall == DialogResult.Yes)
            {
                Controller.UninstallPlugin(selectedPlugin);
                RefreshInstalled();
            }
        }

        private void InstalledPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            InstalledPluginsList.ClearSelection();
            PluginDescriptionInstalled.Clear();
            Uninstall.Enabled = false;
        }

        private void RefreshInstalled()
        {
            InstalledPluginsList.DataSource = null;
            InstalledPluginsList.DataSource = Controller.installedPlugins;
        }
    }
}
