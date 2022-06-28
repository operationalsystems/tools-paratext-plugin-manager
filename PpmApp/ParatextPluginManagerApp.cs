using Microsoft.Extensions.Logging;
using System.Windows.Forms;

namespace PpmApp
{
    class ParatextPluginManagerApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // set up logging filters
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog();
            });

            ILogger logger = loggerFactory.CreateLogger<ParatextPluginManagerApp>();
            logger.LogInformation("Example log message");

            // set up UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PluginManagerMainForm());
        }
    }
}
