using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestJimsWorkConfiguration
{
   [TestClass]
   public class TestInstalledApplicationsForDeveloper
   {
      [TestMethod]
      public void TestProgramPresentTFSPowerTools()
      {
         //TODO: Add test to detect if TFS Power Tools is present
      }
      [TestMethod]
      public void TestExpectedVersionTFSPowerTools()
      {
         //TODO: Add test to detect what version of TFS Power Tools is present and if it matches expected version
         //TODO: Set up a config file approach that has a list of assembly names and expected versions of software
      }
      [TestMethod]
      public void TestProgramPresentOpenDBDiff()
      {
         //TODO: Add test to detect if OpenDBDiff is present
      }
   }
}
