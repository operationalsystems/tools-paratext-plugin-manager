using Microsoft.Win32;

namespace PpmMain.Util
{
    /// <summary>
    /// Fetches data from the registry
    /// </summary>
    public class RegistryService : IRegistryService
    {
        private readonly RegistryKey localMachine;

        public RegistryService()
        {
            localMachine = Registry.LocalMachine;
        }
        public RegistryKey ReadLocalMachineSubKey(string subKey)
        {
            return localMachine.OpenSubKey(subKey);
        }

        public object ReadKeyValue(RegistryKey key, string name)
        {
            return key.GetValue(name);
        }
    }
}
