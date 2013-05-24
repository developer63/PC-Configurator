using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsSystemConfiguration;
using System.IO;
namespace TestJimsPersonalConfiguration
{
   [TestClass]
   public class TestInstalledApplications
   {
      public PCSystemConfiguration MyPCSystemConfiguration;

      [TestInitialize]
      public void TestInitialize()
      {
         MyPCSystemConfiguration = new PCSystemConfiguration();
      }
      [TestMethod]
      public void TestEccoPresent()
      {
         Assert.IsTrue(File.Exists(MyPCSystemConfiguration.ProgramFilesX86Folder + @"\" + "ECCO" + @"\" + "ecco32.exe"));
      }
   }
}
