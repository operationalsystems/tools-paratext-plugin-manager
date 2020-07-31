using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PpmMain.LocalInstallerService;

namespace PpmUnitTests
{
    [TestClass]
    public class LocalInstallerServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(LocalInstallerService.SimpleTest);
        }
    }
}
