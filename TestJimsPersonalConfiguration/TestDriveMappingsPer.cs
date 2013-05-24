using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsSystemSupport;
using WindowsSystemConfiguration;

namespace TestJimsPersonalConfiguration
{
   [TestClass]
   public class TestDriveMappingsPer
   {
   public PCSystemConfiguration MyPCSystemConfiguration;
   
   [TestInitialize]
   public void TestInitialize()
   {
      MyPCSystemConfiguration = new PCSystemConfiguration();
   }

     [TestMethod]
   public void TestDriveMapping_A_for_Audiobooks()
   {
      {
         StorageDrive d = MyPCSystemConfiguration.StorageDrives.Find(x => x.DriveLetter.ToLower().StartsWith("a:"));
         Assert.IsTrue(d.DriveMapping.ToLower().Contains("audio"));
         //TODO: Add check for specific mapping
      }

   }

}
}