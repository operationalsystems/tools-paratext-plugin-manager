using PpmMain.Controllers;
using PpmMain.Forms;
using PpmMain.Models;
using PpmMain.Properties;
using PpmMain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace PpmMain
{
    public partial class PluginManagerMainForm : Form
    {
        /// <summary>
        /// The controller that will handle business logic for this view.
        /// </summary>
        PluginManagerMainFormController Controller { get; set; }

        public PluginManagerMainForm()
        {
            InitializeComponent();
            CopyrightLabel.Text = MainConsts.CopyrightText;
        }

        /// <summary>
        /// This method executes when the form first loads.
        /// </summary>
        /// <param name="sender">The form.</param>
        /// <param name="e">The load event.</param>
        private void PluginManagerMainForm_Load(object sender, EventArgs e)
        {
            Controller = new PluginManagerMainFormController();
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
            PluginDescription selectedPlugin = Controller.AvailablePlugins[AvailablePluginsList.CurrentCell.RowIndex];

            LicenseForm eulaPrompt = new LicenseForm();
            eulaPrompt.FormType = LicenseForm.FormTypes.Prompt;
            eulaPrompt.FormTitle = $"{selectedPlugin.Name} {MainConsts.LicenseFormTitle}";
            eulaPrompt.LicenseText = selectedPlugin.License;
            eulaPrompt.OnAccept = () =>
            {
                eulaPrompt.Close();

                /// Confirm that the user wishes to install the plugin.
                DialogResult confirmInstall = MessageBox.Show($"Are you sure you wish to install {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                            $"Confirm Plugin Install",
                                            MessageBoxButtons.YesNo);
                if (confirmInstall == DialogResult.Yes)
                {
                    ShowProgressBar(MainConsts.ProgressBarInstalling);

                    /// We need to move the work into the background so that the progress bar keeps moving.
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                    {
                        Controller.InstallPlugin(selectedPlugin);
                        RefreshBindings();
                    });
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
                    {
                        HideProgressBar();

                        MessageBox.Show(@$"
{selectedPlugin.Name} ({selectedPlugin.Version}) has been installed.

{MainConsts.PluginListChangedMessage}",
                                $"Plugin Installed",
                                MessageBoxButtons.OK);
                    });
                    worker.RunWorkerAsync();
                }
            };
            eulaPrompt.OnDismiss = () =>
            {
                eulaPrompt.Close();
                MessageBox.Show("Installation cancelled.",
$"Plugin Not Installed",
MessageBoxButtons.OK);
            };
            eulaPrompt.Show();
        }

        /// <summary>
        /// This method handles clicking the "Update" button.
        /// </summary>
        /// <param name="sender">The Updates list.</param>
        /// <param name="e">The click event.</param>
        private void UpdateOne_Click(object sender, EventArgs e)
        {
            OutdatedPlugin selectedPlugin = Controller.OutdatedPlugins[OutdatedPluginsList.CurrentCell.RowIndex];

            /// Confirm that the user wishes to update the plugin.
            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you wish to update {selectedPlugin.Name} from version {selectedPlugin.InstalledVersion} to {selectedPlugin.Version}?",
                                     $"Confirm Plugin Update",
                                     MessageBoxButtons.YesNo);
            if (confirmUpdate == DialogResult.Yes)
            {
                ShowProgressBar(MainConsts.ProgressBarUpdating);

                /// We need to move the work into the background so that the progress bar keeps moving.
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                {
                    Controller.UpdatePlugins(new System.Collections.Generic.List<OutdatedPlugin>() { selectedPlugin });
                    RefreshBindings();
                });
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
                {
                    HideProgressBar();

                    MessageBox.Show(@$"
{selectedPlugin.Name} has been updated to version {selectedPlugin.Version}.

{MainConsts.PluginListChangedMessage}",
                      $"Plugin Updated",
                      MessageBoxButtons.OK);
                });
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// This method handles clicking the "Update All" button.
        /// </summary>
        /// <param name="sender">The Updates list.</param>
        /// <param name="e">The click event.</param>
        private void UpdateAll_Click(object sender, EventArgs e)
        {
            int pluginCount = Controller.OutdatedPlugins.Count;

            /// Confirm that the user wishes to update all the plugins.
            DialogResult confirmUpdate = MessageBox.Show($"Are you sure you wish to update {pluginCount} plugins?",
                                     $"Confirm Update All",
                                     MessageBoxButtons.YesNo);
            if (confirmUpdate == DialogResult.Yes)
            {
                ShowProgressBar(MainConsts.ProgressBarUpdating);

                /// We need to move the work into the background so that the progress bar keeps moving.
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                {
                    Controller.UpdatePlugins(Controller.OutdatedPlugins);
                    RefreshBindings();
                });
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
                {
                    HideProgressBar();

                    MessageBox.Show(@$"
All plugins have been updated.

{MainConsts.PluginListChangedMessage}",
                         $"All Plugins Updated",
                         MessageBoxButtons.OK);
                });
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// This method handles clicking the "Uninstall" button.
        /// </summary>
        /// <param name="sender">The Installed Plugins list.</param>
        /// <param name="e">The click event.</param>
        private void Uninstall_Click(object sender, EventArgs e)
        {
            PluginDescription selectedPlugin = Controller.InstalledPlugins[InstalledPluginsList.CurrentCell.RowIndex];

            /// Confirm that the user wishes to uninstall the plugin.
            DialogResult confirmUninstall = MessageBox.Show($"Are you sure you wish to uninstall {selectedPlugin.Name} ({selectedPlugin.Version})?",
                                     $"Confirm Plugin Uninstall",
                                     MessageBoxButtons.YesNo);
            if (confirmUninstall == DialogResult.Yes)
            {
                ShowProgressBar(MainConsts.ProgressBarUninstalling);

                /// We need to move the work into the background so that the progress bar keeps moving.
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                {
                    Controller.UninstallPlugin(selectedPlugin);
                    RefreshBindings();
                });
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
                {
                    HideProgressBar();

                    MessageBox.Show(@$"
{selectedPlugin.Name} ({selectedPlugin.Version}) has been uninstalled.

{MainConsts.PluginListChangedMessage}",
                         $"Plugin Uninstalled",
                         MessageBoxButtons.OK);
                });
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// This method handles clearing any selections when the data binding is updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// This method refreshes all the data bindings.
        /// </summary>
        private void RefreshBindings()
        {
            AvailablePluginsList.DataSource = Controller.AvailablePlugins;
            OutdatedPluginsList.DataSource = Controller.OutdatedPlugins;
            InstalledPluginsList.DataSource = Controller.InstalledPlugins;
            UpdateAll.Enabled = (Controller.OutdatedPlugins.Count > 0);

        }

        /// <summary>
        /// This method handles updating the search filter to change what plugins appear in the list.
        /// </summary>
        private void UpdateSearchFilter()
        {
            Controller.FilterCriteria = SearchText.Text;
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
    }
}
