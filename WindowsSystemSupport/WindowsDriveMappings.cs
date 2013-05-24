using System;
using System.Runtime.InteropServices;
using System.ComponentModel;       //Win32Exception

using System.Text;
using System.IO;

namespace WindowsSystemSupport
{
   /* This needs cleaning up -- errr but I'm paid by the hour so I don't have the time. I mostly ripped the wrapper and modified it. It provides an alternative to pointer arithmetic though...*/
   /*
   typedef struct _REMOTE_NAME_INFO 
   {
       LPTSTR lpUniversalName;
       LPTSTR lpConnectionName;
       LPTSTR lpRemainingPath;
   } REMOTE_NAME_INFO;
   */

   [StructLayout(LayoutKind.Sequential)]
   struct _REMOTE_NAME_INFO
   {
      public IntPtr lpUniversalName;
      public IntPtr lpConnectionName;
      public IntPtr lpRemainingPath;
   }

   public struct RemoteNameInfo
   {
      public string universalName;
      public string connectionName;
      public string remainingPath;
   }

   public class WindowsDriveMappings
   {
      private enum ResourceScope
      {
         RESOURCE_CONNECTED = 1,
         RESOURCE_GLOBALNET,
         RESOURCE_REMEMBERED,
         RESOURCE_RECENT,
         RESOURCE_CONTEXT
      }
      private enum ResourceType
      {
         RESOURCETYPE_ANY,
         RESOURCETYPE_DISK,
         RESOURCETYPE_PRINT,
         RESOURCETYPE_RESERVED
      }
      private enum ResourceUsage
      {
         RESOURCEUSAGE_CONNECTABLE = 0x00000001,
         RESOURCEUSAGE_CONTAINER = 0x00000002,
         RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
         RESOURCEUSAGE_SIBLING = 0x00000008,
         RESOURCEUSAGE_ATTACHED = 0x00000010
      }
      private enum ResourceDisplayType
      {
         RESOURCEDISPLAYTYPE_GENERIC,
         RESOURCEDISPLAYTYPE_DOMAIN,
         RESOURCEDISPLAYTYPE_SERVER,
         RESOURCEDISPLAYTYPE_SHARE,
         RESOURCEDISPLAYTYPE_FILE,
         RESOURCEDISPLAYTYPE_GROUP,
         RESOURCEDISPLAYTYPE_NETWORK,
         RESOURCEDISPLAYTYPE_ROOT,
         RESOURCEDISPLAYTYPE_SHAREADMIN,
         RESOURCEDISPLAYTYPE_DIRECTORY,
         RESOURCEDISPLAYTYPE_TREE,
         RESOURCEDISPLAYTYPE_NDSCONTAINER
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct NETRESOURCE
      {
         public ResourceScope oResourceScope;
         public ResourceType oResourceType;
         public ResourceDisplayType oDisplayType;
         public ResourceUsage oResourceUsage;
         public string sLocalName;
         public string sRemoteName;
         public string sComments;
         public string sProvider;
      }
      [DllImport("mpr.dll")]
      private static extern int WNetAddConnection2
         (ref NETRESOURCE oNetworkResource, string sPassword,
         string sUserName, int iFlags);

      [DllImport("mpr.dll")]
      private static extern int WNetCancelConnection2
         (string sLocalName, uint iFlags, int iForce);

      [DllImport("mpr.dll")]
      static extern uint WNetGetConnection(string lpLocalName, StringBuilder lpRemoteName, ref int lpnLength);

      // Constants for use by WNetGetUniversalName
      const int UNIVERSAL_NAME_INFO_LEVEL = 0x00000001;
      const int REMOTE_NAME_INFO_LEVEL = 0x00000002;
      const int ERROR_MORE_DATA = 234;
      const int ERROR_NOT_CONNECTED = 2250;
      const int NOERROR = 0;

      [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
      [return: MarshalAs(UnmanagedType.U4)]
      private static extern int WNetGetUniversalNameW(
          string lpLocalPath,
          [MarshalAs(UnmanagedType.U4)] int dwInfoLevel,
          IntPtr lpBuffer,
          [MarshalAs(UnmanagedType.U4)] ref int lpBufferSize);

      internal static bool IsLocalDrive(String driveName)
      {
         bool isLocal = true;  // assume local until disproved

         // strip trailing backslashes from driveName
         driveName = driveName.Substring(0, 2);

         int length = 256; // to be on safe side 
         StringBuilder networkShare = new StringBuilder(length);
         uint status = WNetGetConnection(driveName, networkShare, ref length);

         // does a network share exist for this drive?
         if (networkShare.Length != 0)
         {
            // now networkShare contains a UNC path in format \\MachineName\ShareName
            // retrieve the MachineName portion
            String shareName = networkShare.ToString();
            string[] splitShares = shareName.Split('\\');
            // the 3rd array element now contains the machine name
            if (Environment.MachineName == splitShares[2])
               isLocal = true;
            else
               isLocal = false;
         }
         return isLocal;
      }
 
      public static void MapNetworkDrive(string sDriveLetter, string sNetworkPath)
      {
         //Checks if the last character is \ as this causes error on mapping a drive.
         if (sNetworkPath.Substring(sNetworkPath.Length - 1, 1) == @"\")
         {
            sNetworkPath = sNetworkPath.Substring(0, sNetworkPath.Length - 1);
         }

         NETRESOURCE oNetworkResource = new NETRESOURCE();
         oNetworkResource.oResourceType = ResourceType.RESOURCETYPE_DISK;
         oNetworkResource.sLocalName = sDriveLetter + ":";
         oNetworkResource.sRemoteName = sNetworkPath;

         //If Drive is already mapped disconnect the current 
         //mapping before adding the new mapping
         if (IsDriveMapped(sDriveLetter))
         {
            DisconnectNetworkDrive(sDriveLetter, true);
         }

         WNetAddConnection2(ref oNetworkResource, null, null, 0);
      }

      public static int DisconnectNetworkDrive(string sDriveLetter, bool bForceDisconnect)
      {
         if (bForceDisconnect)
         {
            return WNetCancelConnection2(sDriveLetter + ":", 0, 1);
         }
         else
         {
            return WNetCancelConnection2(sDriveLetter + ":", 0, 0);
         }
      }

      public static bool IsDriveMapped(string sDriveLetter)
      {
         string[] DriveList = Environment.GetLogicalDrives();
         for (int i = 0; i < DriveList.Length; i++)
         {
            if (sDriveLetter + ":\\" == DriveList[i].ToString())
            {
               return true;
            }
         }
         return false;
      }
      /// <summary>
      /// Return the UNC remote mapping for a given drive letter/name
      /// </summary>
      /// <param name="driveName"></param>
      /// <returns></returns>
      public static string DriveUNCMapping(String driveName)
      {
         string driveMapping = ""; // Assume empty mapping until disproven
         // strip trailing backslashes from driveName
         driveName = driveName.Substring(0, 2);

         int length = 256; // to be on safe side 
         StringBuilder networkShare = new StringBuilder(length);
         uint status = WNetGetConnection(driveName, networkShare, ref length);

         // does a network share exist for this drive?
         if (networkShare.Length != 0)
         {
            // now networkShare contains a UNC path in format \\MachineName\ShareName
            // retrieve the MachineName portion
            String shareName = networkShare.ToString();
            string[] splitShares = shareName.Split('\\');
            // the 3rd array element now contains the machine name
            if (Environment.MachineName == splitShares[2]) {
              // Local device, no mapping involved
              driveMapping = "";
            }
            else {
              // Remote device, has a mapping
              driveMapping = shareName;
            }
         }
         return driveMapping;
      }

      /// <summary>
      /// Based on code from here: http://www.pinvoke.net/default.aspx/advapi32/WNetGetUniversalName.html 
      /// Note that when running this process elevated as an administrator, it won't see the drives mapped for the non-admin user.
      /// So if running within Visual Studio with admin rights, it will *appear* to give incorrect results.
      /// http://stackoverflow.com/questions/11268337/driveinfo-getdrives-not-returning-mapped-drives-when-run-as-administrator
      /// </summary>
      /// <param name="localPath"></param>
      /// <returns></returns>
      public static RemoteNameInfo GetRemoteNameInfo(string localPath)
      {
         // The return value.
         RemoteNameInfo retVal;
         _REMOTE_NAME_INFO rni;

         // The pointer in memory to the structure.
         IntPtr buffer = IntPtr.Zero;

         // Wrap in a try/catch block for cleanup.
         try
         {
            // First, call WNetGetUniversalName to get the size.
            int size = 0;

            // Make the call.
            // Pass IntPtr.Size because the API doesn't like null, even though
            // size is zero.  We know that IntPtr.Size will be
            // aligned correctly.
            int apiRetVal = WNetGetUniversalNameW(localPath, REMOTE_NAME_INFO_LEVEL, (IntPtr)IntPtr.Size, ref size);

            //  if the return value is ERROR_NOT_CONNECTED, then
            //  this is a local path
            if (apiRetVal == ERROR_NOT_CONNECTED)
            {
               retVal = new RemoteNameInfo();
               retVal.connectionName = Path.GetPathRoot(localPath);
               retVal.remainingPath = localPath.Substring(Path.GetPathRoot(localPath).Length);
               retVal.universalName = localPath;
               return retVal;
            }

            // If the return value is not ERROR_MORE_DATA, then
            // raise an exception.
            if (apiRetVal != ERROR_MORE_DATA)
               // Throw an exception.
               throw new System.ComponentModel.Win32Exception();

            // Allocate the memory.
            buffer = Marshal.AllocCoTaskMem(size);

            // Now make the call.
            apiRetVal = WNetGetUniversalNameW(localPath, REMOTE_NAME_INFO_LEVEL, buffer, ref size);

            // If it didn't succeed, then throw.
            if (apiRetVal != NOERROR)
               // Throw an exception.
               throw new System.ComponentModel.Win32Exception();

            // Now get the string.  It's all in the same buffer, but
            // the pointer is first, so offset the pointer by IntPtr.Size
            // and pass to PtrToStringAuto.
            //retVal = Marshal.PtrToStringAuto(new IntPtr(buffer.ToInt64() + IntPtr.Size));

            rni = (_REMOTE_NAME_INFO)Marshal.PtrToStructure(buffer, typeof(_REMOTE_NAME_INFO));

            retVal.connectionName = Marshal.PtrToStringAuto(rni.lpConnectionName);
            retVal.remainingPath = Marshal.PtrToStringAuto(rni.lpRemainingPath);
            retVal.universalName = Marshal.PtrToStringAuto(rni.lpUniversalName);

            return retVal;
         }
         finally
         {
            // Release the buffer.
            Marshal.FreeCoTaskMem(buffer);
         }
      }
   }
}
