using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsSystemSupport
{
   /// <summary>
   /// Windows Special Folders list in a dictionary
   /// </summary>
   public class WindowsSpecialFolders: Dictionary<String,String>
   {
      public string ProgramFilesX86 { get; private set; }
      // Constructor
      public WindowsSpecialFolders()
      {
         this.Add("Favorites", Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
         this.Add("MyDocuments", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
         this.Add("MyPictures", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
         this.Add("ProgramFiles", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
         this.Add("ProgramFilesX86", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
         this.Add("Windows", Environment.GetFolderPath(Environment.SpecialFolder.Windows));
         this.Add("Startup", Environment.GetFolderPath(Environment.SpecialFolder.Startup));
         this.Add("StartMenu", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
         this.Add("MyMusic", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
         this.Add("LocalApplicationData", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
         this.Add("InternetCache", Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
         this.Add("History", Environment.GetFolderPath(Environment.SpecialFolder.History));
         this.Add("ApplicationData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
      }
   }
}
