/*
Copyright © 2022 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Microsoft.Extensions.Logging;
using PpmApp.Controllers;
using PpmApp.Forms;
using PpmApp.Models;
using PpmApp.Properties;
using PpmApp.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace PpmApp
{
    public partial class PluginManagerMainForm : Form
    {
        /// <summary>
        /// Type-specific logger (injected).
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The controller that will handle business logic for this view.
        /// </summary>
        private readonly PluginManagerMainFormController _controller;

        /// <summary>
        /// Basic constructor. Dependency Injected.
        /// </summary>
        /// <param name="logger">Class logger instance</param>
        public PluginManagerMainForm(ILogger<PluginManagerMainForm> logger, PluginManagerMainFormController controller)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));

            InitializeComponent();
            CopyrightLabel.Text = MainConsts.Copyright;
        }

        /// <summary>
        /// This method executes when the form first loads.
        /// </summary>
        /// <param name="sender">The form.</param>
        /// <param name="e">The load event.</param>
        private void PluginManagerMainForm_Load(object sender, EventArgs e)
        {
            RefreshBindings();
        }

        /// <summary>
        /// This method handles a search button being clicked.
        /// </summary>
        /// <param name="sender">The button being clicked.</param>
        /// <param name="e">The click event.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            UpdateSearchFilter();
        }

        /// <summary>
        /// This method handles hitting enter in the search field.
        /// </summary>
        /// <param name="sender">The search text box.</param>
        /// <param name="e">The key up event.</param>
        private void SearchText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateSearchFilter();
        }

        /// <summary>
        /// This method clears the search when all text has been removed from the search box.
        /// </summary>
        /// <param name="sender">The search text box.</param>
        /// <param name="e">The changed text event.</param>
        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(SearchText.Text))
            {
                UpdateSearchFilter();
                SearchButton.Enabled = false;
            }
            else
            {
                SearchButton.Enabled = true;
            }
        }

        /// <summary>
        /// This method handles selecting an plugin from a list.
        /// </summary>
        /// <param name="sender">The list that had a selection change event.</param>
        /// <param name="e">The selection change event.</param>
        private void AnyPluginList_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            if (grid.SelectedRows.Count == 0) return;
            List<PluginDescription> availablePlugins = grid.DataSource as List<PluginDescription>;
            List<OutdatedPlugin> outdatedPlugins = grid.DataSource as List<OutdatedPlugin>;
            List<PluginDescription> installedPlugins = grid.DataSource as List<PluginDescription>;

            int index = grid.SelectedRows[0].Index;
            if (index < 0) return;

            switch (grid.Name)
            {
                case "AvailablePluginsList":
                    {
                        if (!Install.Enabled) Install.Enabled = true;
                        PluginDescriptionAvailable.Text = availablePlugins[index].Description;
                        break;
                    }
                case "OutdatedPluginsList":
                    {
                        if (!UpdateOne.Enabled) UpdateOne.Enabled = true;
                        PluginDescriptionOutdated.Text = outdatedPlugins[index].VersionDescription;
                        break;
                    }
                case "InstalledPluginsList":
                    {
                        if (!Uninstall.Enabled) Uninstall.Enabled = true;
                        PluginDescriptionInstalled.Text = installedPlugins[index].Description;
                        break;
                    }
            }
        }

        /// <summary>
        /// This method handles clicking the "Install" button.
        /// </summary>
        /// <param name="sender">The Available Plugins list.</param>
        /// <param name="e">The click event.</param>
        private void Install_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = _controller.AvailablePlugins[AvailablePluginsList.CurrentCell.RowIndex];

            /// Confirm that the user wishes to install the plugin.
            DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to install {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                        $"Confirm Plugin Install",
                                        MessageBoxButtons.YesNo);
            if (DialogResult.Yes != confirmInstall) return;

            LicenseForm eulaPrompt = new LicenseForm
            {
                FormType = LicenseForm.FormTypes.Prompt,
                FormTitle = $"{selectedPlugin.Name} - {MainConsts.LicenseFormTitle}",
                LicenseText = selectedPlugin.License
            };
            eulaPrompt.OnAccept = () =>
            {
                eulaPrompt.DialogResult = DialogResult.Yes;
                eulaPrompt.Close();
            };
            eulaPrompt.OnDismiss = () =>
            {
                eulaPrompt.DialogResult = DialogResult.No;
                eulaPrompt.Close();
            };

            eulaPrompt.ShowDialog();

            if (DialogResult.Yes != eulaPrompt.DialogResult)
            {
                MessageBox.Show("Installation cancelled.", "Plugin Not Installed", MessageBoxButtons.OK);
                return;
            };

            if (!CheckParatextAndPromptToProceed())
            {
                return;
            }

            ShowProgressBar(MainConsts.ProgressBarInstalling);

            BackgroundWorker backgroundworker = new BackgroundWorker();
            backgroundworker.DoWork += (sender, args) =>
            {
                _controller.InstallPlugin(selectedPlugin);
            };
            backgroundworker.RunWorkerCompleted += (sender, args) =>
            {
                RefreshBindings();
                HideProgressBar();

                MessageBox.Show(@$"
{selectedPlugin.Name} ({selectedPlugin.Version}) has been installed.

{MainConsts.PluginListChangedMessage}",
                                 $"Plugin Installed",
                                 MessageBoxButtons.OK);
            };
            backgroundworker.RunWorkerAsync();
        }

        /// <summary>
        /// This method handles clicking the "Update" button.
        /// </summary>
        /// <param name="sender">The Updates list.</param>
        /// <param name="e">The click event.</param>
        private void UpdateOne_Click(object sender, EventArgs e)
        {
            OutdatedPlugin selectedPlugin = _controller.OutdatedPlugins[OutdatedPluginsList.CurrentCell.RowIndex];
            DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to update {selectedPlugin.Name} from version {selectedPlugin.InstalledVersion} to {selectedPlugin.Version}?",
                         $"Confirm Plugin Update",
                         MessageBoxButtons.YesNo);

            if (DialogResult.Yes != confirmInstall) return;

            LicenseForm eulaPrompt = new LicenseForm
            {
                FormType = LicenseForm.FormTypes.Prompt,
                FormTitle = $"{selectedPlugin.Name} - {MainConsts.LicenseFormTitle}",
                LicenseText = selectedPlugin.License
            };
            eulaPrompt.OnAccept = () =>
            {
                eulaPrompt.DialogResult = DialogResult.Yes;
                eulaPrompt.Close();
            };
            eulaPrompt.OnDismiss = () =>
            {
                eulaPrompt.DialogResult = DialogResult.No;
                eulaPrompt.Close();
            };

            eulaPrompt.ShowDialog();

            if (DialogResult.Yes != eulaPrompt.DialogResult)
            {
                MessageBox.Show("Update cancelled.", "Plugin Not Updated", MessageBoxButtons.OK);
                return;
            };

            if (!CheckParatextAndPromptToProceed())
            {
                return;
            }

            ShowProgressBar(MainConsts.ProgressBarUpdating);

            BackgroundWorker backgroundworker = new BackgroundWorker();
            backgroundworker.DoWork += (sender, args) =>
            {
                _controller.InstallPlugin(selectedPlugin);
            };
            backgroundworker.RunWorkerCompleted += (sender, args) =>
            {
                RefreshBindings();

                HideProgressBar();

                MessageBox.Show(@$"
{selectedPlugin.Name} has been updated to version {selectedPlugin.Version}.

{MainConsts.PluginListChangedMessage}",
                  "Plugin Updated",
                  MessageBoxButtons.OK);
            };
            backgroundworker.RunWorkerAsync();
        }

        /// <summary>
        /// This method handles clicking the "Update All" button.
        /// </summary>
        /// <param name="sender">The Updates list.</param>
        /// <param name="e">The click event.</param>
        private void UpdateAll_Click(object sender, EventArgs e)
        {
            Queue<MethodInvoker> installationQueue = new Queue<MethodInvoker>();
            void installNextPlugin()
            {
                if (installationQueue.Count > 0)
                {
                    MethodInvoker installPlugin = installationQueue.Dequeue();
                    installPlugin();
                }
            }
            for (int i = 0; i < _controller.OutdatedPlugins.Count; i++)
            {
                OutdatedPlugin selectedPlugin = _controller.OutdatedPlugins[i];

                installationQueue.Enqueue(() =>
                {
                    DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to update {selectedPlugin.Name} from version {selectedPlugin.InstalledVersion} to {selectedPlugin.Version}?",
                                 "Confirm Plugin Update",
                                 MessageBoxButtons.YesNo);

                    if (DialogResult.Yes != confirmInstall)
                    {
                        installNextPlugin();
                        return;
                    }

                    LicenseForm eulaPrompt = new LicenseForm
                    {
                        FormType = LicenseForm.FormTypes.Prompt,
                        FormTitle = $"{selectedPlugin.Name} - {MainConsts.LicenseFormTitle}",
                        LicenseText = selectedPlugin.License
                    };
                    eulaPrompt.OnAccept = () =>
                    {
                        eulaPrompt.DialogResult = DialogResult.Yes;
                        eulaPrompt.Close();
                    };
                    eulaPrompt.OnDismiss = () =>
                    {
                        eulaPrompt.DialogResult = DialogResult.No;
                        eulaPrompt.Close();
                    };
                    eulaPrompt.ShowDialog();

                    if (DialogResult.Yes != eulaPrompt.DialogResult)
                    {
                        MessageBox.Show("Update cancelled.", "Plugin Not Updated", MessageBoxButtons.OK);
                        installNextPlugin();
                        return;
                    }

                    // let's check Paratext on every plugin update, in case someone decides to start Paratext up again in between plugin installs
                    if (!CheckParatextAndPromptToProceed())
                    {
                        return;
                    }

                    ShowProgressBar(MainConsts.ProgressBarUpdating);

                    BackgroundWorker backgroundworker = new BackgroundWorker();
                    backgroundworker.DoWork += (sender, args) =>
                    {
                        _controller.InstallPlugin(selectedPlugin);
                    };
                    backgroundworker.RunWorkerCompleted += (sender, args) =>
                    {
                        RefreshBindings();
                        HideProgressBar();

                        MessageBox.Show(@$"
{selectedPlugin.Name} has been updated to version {selectedPlugin.Version}.

{MainConsts.PluginListChangedMessage}",
                          $"Plugin Updated",
                          MessageBoxButtons.OK);
                        installNextPlugin();
                    };
                    backgroundworker.RunWorkerAsync();
                });
            }
            installNextPlugin();
        }

        /// <summary>
        /// This method handles clicking the "Uninstall" button.
        /// </summary>
        /// <param name="sender">The Installed Plugins list.</param>
        /// <param name="e">The click event.</param>
        private void Uninstall_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = _controller.InstalledPlugins[InstalledPluginsList.CurrentCell.RowIndex];

            /// Confirm that the user wishes to uninstall the plugin.
            DialogResult confirmUninstall = MessageBox.Show(
                $"Are you sure you wish to uninstall {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     "Confirm Plugin Uninstall",
                                     MessageBoxButtons.YesNo);
            if (DialogResult.Yes != confirmUninstall) return;

            if (!CheckParatextAndPromptToProceed())
            {
                return;
            }

            ShowProgressBar(MainConsts.ProgressBarUninstalling);

            BackgroundWorker backgroundworker = new BackgroundWorker();
            backgroundworker.DoWork += (sender, args) =>
            {
                _controller.UninstallPlugin(selectedPlugin);
            };
            backgroundworker.RunWorkerCompleted += (sender, args) =>
            {
                RefreshBindings();

                HideProgressBar();

                MessageBox.Show(@$"
{selectedPlugin.Name} ({selectedPlugin.Version}) has been uninstalled.

{MainConsts.PluginListChangedMessage}",
                     "Plugin Uninstalled",
                     MessageBoxButtons.OK);
            };
            backgroundworker.RunWorkerAsync();
        }

        /// <summary>
        /// This method handles clearing any selections when the data binding is updated.
        /// </summary>
        /// <param name="sender">The data source.</param>
        /// <param name="e">The binding event.</param>
        private void AnyPluginList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            grid.ClearSelection();

            switch (grid.Name)
            {
                case "AvailablePluginsList":
                    {
                        Install.Enabled = false;
                        PluginDescriptionAvailable.Clear();
                        break;
                    }
                case "OutdatedPluginsList":
                    {
                        UpdateOne.Enabled = false;
                        PluginDescriptionOutdated.Clear();
                        break;
                    }
                case "InstalledPluginsList":
                    {
                        Uninstall.Enabled = false;
                        PluginDescriptionInstalled.Clear();
                        break;
                    }
            }
        }

        /// <summary>
        /// This method handles clicking on the License menu item.
        /// </summary>
        /// <param name="sender">The menu item.</param>
        /// <param name="e">The click event.</param>
        private void LicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pluginName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            LicenseForm eulaForm = new LicenseForm();
            eulaForm.FormType = LicenseForm.FormTypes.Info;
            eulaForm.FormTitle = $"{pluginName} - {MainConsts.LicenseFormTitle}";
            eulaForm.LicenseText = Resources.PPM_EULA;
            eulaForm.OnDismiss = () => eulaForm.Close();
            eulaForm.Show();
        }

        /// <summary>
        /// This method refreshes all the data bindings.
        /// </summary>
        private void RefreshBindings()
        {
            AvailablePluginsList.DataSource = _controller.AvailablePlugins;
            OutdatedPluginsList.DataSource = _controller.OutdatedPlugins;
            InstalledPluginsList.DataSource = _controller.InstalledPlugins;
            UpdateAll.Enabled = (_controller.OutdatedPlugins.Count > 0);
        }

        /// <summary>
        /// This method handles updating the search filter to change what plugins appear in the list.
        /// </summary>
        private void UpdateSearchFilter()
        {
            _controller.FilterCriteria = SearchText.Text;
            RefreshBindings();
        }

        /// <summary>
        /// This method shows the progress bar and sets the associated label text.
        /// </summary>
        /// <param name="labelText">The label text to display.</param>
        private void ShowProgressBar(string labelText)
        {
            FormProgress.Visible = true;
            ProgressLabel.Visible = true;
            ProgressLabel.Text = labelText;
            this.Enabled = false;
        }

        /// <summary>
        /// This method hides the progress bar and clears the associated label text.
        /// </summary>
        private void HideProgressBar()
        {
            this.Enabled = true;
            ProgressLabel.Text = "";
            ProgressLabel.Visible = false;
            FormProgress.Visible = false;
        }

        /// <summary>
        /// Opens a link to the support URL from the plugin
        /// </summary>
        /// <param name="sender">The control that sent this event</param>
        /// <param name="e">The event information that triggered this call</param>
        private void contactSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Call the Process.Start method to open the default browser
            //with a URL:
            Process.Start(MainConsts.SUPPORT_URL);
        }

        /// <summary>
        /// This is a utility function for checking if Paratext is running. If it is, prompt if the user wants to close Paratext and proceed or to stop; Otherwise, if Paratext isn't running, indicate that we're OK to proceed.
        /// </summary>
        /// <returns>true: Paratext isn't running or was killed. false: Paratext was running and user doesn't want to kill and proceed.</returns>
        private static bool CheckParatextAndPromptToProceed()
        {
            // make sure that Paratext isn't running first
            if (ParatextUtil.IsParatextRunning())
            {
                var result = MessageBox.Show("Paratext is running. Would you like to close Paratext and proceed?", "Paratext is running", MessageBoxButtons.YesNo);

                switch (result)
                {
                    case DialogResult.Yes:
                        ParatextUtil.CloseParatext();
                        return true;
                    default:
                        return false;
                }
            }

            // Paratext isn't running. We're OK to proceed.
            return true;
        }
    }
}
