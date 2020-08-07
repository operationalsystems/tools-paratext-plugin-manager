using PpmMain.Models;
using PpmMain.PluginRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    }
}
