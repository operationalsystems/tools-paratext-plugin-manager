using PpmMain.Models;
using PpmMain.PluginRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PpmMain
{
    public partial class PluginManagerMainForm : Form
    {
        IPluginRepository RemotePluginRepository { get; set; }

        public PluginManagerMainForm()
        {
            InitializeComponent();

            // set up the Plugin Repository Service
            RemotePluginRepository = new S3PluginRepositoryService();
        }

        private void PluginManagerMainForm_Load(object sender, EventArgs e)
        {

        }

        private void testGetLatestPluginsButton_Click(object sender, EventArgs e)
        {
            List<PluginDescription> latestPlugins = RemotePluginRepository.GetAvailablePlugins(true);

            testOutputTextbox.Clear();
            latestPlugins.ForEach(pluginDescription =>
            {
                testOutputTextbox.Text += $"{pluginDescription.ToString()}\r\n\r\n";

                if (pluginDescription.ShortName.Equals("TPT", StringComparison.InvariantCultureIgnoreCase))
                {
                    RemotePluginRepository.DownloadPlugin(pluginDescription.ShortName, pluginDescription.Version);
                }
            });
        }

        private void testGetAllPluginsButton_Click(object sender, EventArgs e)
        {
            List<PluginDescription> latestPlugins = RemotePluginRepository.GetAvailablePlugins(false);

            testOutputTextbox.Clear();
            latestPlugins.ForEach(pluginDescription =>
            {
                testOutputTextbox.Text += $"{pluginDescription.ToString()}\r\n\r\n";

                if (pluginDescription.ShortName.Equals("TPT", StringComparison.InvariantCultureIgnoreCase))
                {
                    RemotePluginRepository.DownloadPlugin(pluginDescription.ShortName, pluginDescription.Version);
                }
            });
        }

        private void openPpmDownloadFolderButton_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = ((S3PluginRepositoryService)RemotePluginRepository).TemporaryDownloadDirectory.FullName,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
    }
}
