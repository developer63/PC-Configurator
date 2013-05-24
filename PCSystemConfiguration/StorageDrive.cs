using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsSystemConfiguration
{
   #region Storage device helper classes
   /// <summary>
   /// A Windows storage drive device, with its local drive letter/name, and remote mapping, if any
   /// </summary>
   public class StorageDrive
   {
      public string DriveLetter { get; set; }
      public string DriveMapping { get; set; }
   }
   /// <summary>
   /// List of storage drive devices, e.g., hard drives, network drives, CD/DVD drives and their mapping, if any
   /// </summary>
   public class DriveList : List<StorageDrive>
   {
      public DriveList()
      {
         DriveInfo[] drives = DriveInfo.GetDrives();
         foreach (DriveInfo d in drives)
         {
            if (d.IsReady)
            {
               StorageDrive myStorageDrive = new StorageDrive();
               myStorageDrive.DriveLetter = d.Name;
               myStorageDrive.DriveMapping = WindowsSystemSupport.WindowsDriveMappings.DriveUNCMapping(myStorageDrive.DriveLetter);
               //myStorageDrive.DriveMapping = WindowsSystemSupport.WindowsDriveMappings.GetRemoteNameInfo(myStorageDrive.DriveLetter).universalName;
               this.Add(myStorageDrive);
            }
         }
      }
   }

   #endregion
}
