using PpmMain.Controllers;
using PpmMain.LocalInstaller;
using PpmMain.Models;
using PpmMain.PluginRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PpmMain
{
    public partial class PluginManagerMainForm : Form
    {
        IPluginManagerMainFormController Controller { get; set; }

        public PluginManagerMainForm()
        {
            InitializeComponent();

            Controller = new PluginManagerMainFormController();
        }

        private void PluginManagerMainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
