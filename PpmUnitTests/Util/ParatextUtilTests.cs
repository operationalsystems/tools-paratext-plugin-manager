﻿/*
Copyright © 2022 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
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
            var paratextVersion = ParatextUtil.ParatextVersion;
            Assert.IsNotNull(paratextVersion, "Cannot determine Paratext's version.");
            Console.WriteLine($@"The installed Paratext version is: '{paratextVersion}'");
        }

        [TestMethod]
        public void TestParatextInstallPath()
        {
            // make sure that we can get the installed Paratext version
            var paratextInstallPath = ParatextUtil.ParatextInstallPath;
            Assert.IsNotNull(paratextInstallPath, "Cannot determine Paratext's install path.");
            Console.WriteLine($@"The installed Paratext path is: '{paratextInstallPath}'");
        }
    }
}
