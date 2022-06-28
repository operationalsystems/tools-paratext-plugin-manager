using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PpmApp.Controllers;
using PpmApp.PluginRepository;
using System;
using System.Windows.Forms;

namespace PpmApp
{
    class ParatextPluginManagerApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // set up Generic Host around the PPM app to leverage dependency injection and logging services.
            var host = CreateHost(args);

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var pluginManagerMainForm = services.GetRequiredService<PluginManagerMainForm>();

                    // set up UI
                    Application.Run(pluginManagerMainForm);
                }
                catch (Exception ex)
                {
                    var messageText = "Error: Please contact support." 
                        + (ex == null ? 
                            string.Empty
                            : Environment.NewLine + Environment.NewLine
                            + "Details: " + ex + Environment.NewLine);

                    MessageBox.Show(messageText, "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Creates, configures, and builds the application host.
        /// </summary>
        /// <param name="args">Program arguments array.</param>
        /// <returns>The Host</returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<PluginRepositoryService>();
                services.AddSingleton<PluginManagerMainFormController>();
                services.AddScoped<PluginManagerMainForm>();
                services.AddLogging(configure =>
                {
                    configure
                        .AddConsole()
                        .AddEventLog();
                });
            })
            .Build();
    }
}
