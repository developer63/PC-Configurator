using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsSystemSupport;
using WindowsSystemConfiguration;

namespace TestJimsWorkConfiguration
{
   [TestClass]
   public class TestDriveMappingsDOL
   {
      public PCSystemConfiguration MyPCSystemConfiguration;
      public WindowsDriveMappings MyDriveSettings;

      [TestInitialize]
      public void TestInitialize()
      {
         MyPCSystemConfiguration = new PCSystemConfiguration();
      }
      [TestMethod]
      public void TestDriveMapping_H_for_HomeDirectory()
      {
         {
            StorageDrive d = MyPCSystemConfiguration.StorageDrives.Find(x => x.DriveLetter.ToLower().StartsWith("h:"));
            Assert.IsTrue(d.DriveMapping.ToLower().Contains("jdaniels"));
            Console.WriteLine("Drive " + d.DriveLetter + " is mapped to " + d.DriveMapping);
         }
      }
   }
}
