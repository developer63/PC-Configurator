using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WindowsSystemConfiguration;


namespace WindowsSystemConfiguration
{

   /// <summary>
   /// Represents configuration details of a single PC/Windows system and its related devices and installed software and services.
   /// Includes:
   ///   List of Mapped Drives
   ///   Location of Windows System
   ///   Location of Program Files 
   ///   Location of Program Files, 32 bit (x86)
   /// </summary>
   public class PCSystemConfiguration
   {
      private DriveList driveList;

      // List of Drive Letters and mappings
      public DriveList StorageDrives
      {
         get { return driveList; }
         set { driveList = value; }
      }
      
      public WindowsSystemSupport.WindowsSpecialFolders MySpecialFolders { get; private set; }
      public string ProgramFilesFolder { get; set; }
      public string ProgramFilesX86Folder { get; set; }
      public PCSystemConfiguration()
      {
         driveList = new DriveList();
         MySpecialFolders = new WindowsSystemSupport.WindowsSpecialFolders();
      }
   }
}
