using Microsoft.Win32;

namespace PpmMain.Util
{
    /// <summary>
    /// Interface for a service that fetches the data we need from the registry
    /// </summary>
    interface IRegistryService
    {
        RegistryKey ReadLocalMachineSubKey(string subKey);

        object ReadKeyValue(RegistryKey key, string name);
    }
}
