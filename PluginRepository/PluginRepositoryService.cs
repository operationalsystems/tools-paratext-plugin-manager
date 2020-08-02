using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpmMain.PluginRepository
{
    public class PluginRepositoryService : IPluginRepository
    {
        public FileInfo DownloadPlugin(string pluginName, string pluginVersion, DirectoryInfo downloadDirectory)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAvailablePlugins(bool latestOnly = true)
        {
            throw new NotImplementedException();
        }
    }
}
