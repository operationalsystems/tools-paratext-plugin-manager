using Microsoft.VisualStudio.TestTools.UnitTesting;
using PpmApp.Util;
using System;

namespace PpmUnitTests
{
    [TestClass]
    public class ParatextUtilTests
    {
        [TestMethod]
        public void TestParatextVersion()
        {
            // make sure that we can get the installed Paratext version
            var paratextVersion = ParatextUtil.Instance.ParatextVersion;
            Assert.IsNotNull(paratextVersion, "Cannot determine Paratext's version.");
            Console.WriteLine($"The installed Paratext version is: '{paratextVersion}'");
        }

        [TestMethod]
        public void TestParatextInstallPath()
        {
            // make sure that we can get the installed Paratext version
            var paratextInstallPath = ParatextUtil.Instance.ParatextInstallPath;
            Assert.IsNotNull(paratextInstallPath, "Cannot determine Paratext's install path.");
            Console.WriteLine($"The installed Paratext path is: '{paratextInstallPath}'");
        }
    }
}
