using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsSystemConfiguration;
using WindowsSystemConfiguration;

namespace CheckPCConfiguration
{

   public partial class formCheckConfiguration : Form
   {
      WindowsSystemConfiguration.PCSystemConfiguration MyPCSystemConfiguration;

      public formCheckConfiguration()
      {
         InitializeComponent();
         MyPCSystemConfiguration = new WindowsSystemConfiguration.PCSystemConfiguration();
         
      }

      private void buttonGetSystemInfo_Click(object sender, EventArgs e)
      {
         textBox1.Clear();
         foreach (StorageDrive s in MyPCSystemConfiguration.StorageDrives)
         {
            textBox1.AppendLine(s.DriveLetter + " " + s.DriveMapping);
         }
      }

      private void buttonSpecialFolders_Click(object sender, EventArgs e)
      {
         textBox1.Clear();
         foreach (KeyValuePair<string, string> pair in MyPCSystemConfiguration.MySpecialFolders)
         {
            textBox1.AppendLine(pair.Key +" " + pair.Value);
         }

      }
   }

   // Uses an Extension Method to add functionality to a multiline textbox to append a line of text
   // See http://stackoverflow.com/questions/8536958/how-to-add-a-line-to-a-multiline-textbox
   public static class WinFormsExtensions
   {
      public static void AppendLine(this TextBox source, string value)
      {
         if (source.Text.Length == 0)
            source.Text = value;
         else
            source.AppendText("\r\n" + value);
      }
   }
}
