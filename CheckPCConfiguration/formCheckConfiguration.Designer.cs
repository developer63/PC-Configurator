namespace CheckPCConfiguration
{
   partial class formCheckConfiguration
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.buttonGetSystemInfo = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.buttonSpecialFolders = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // buttonGetSystemInfo
         // 
         this.buttonGetSystemInfo.Location = new System.Drawing.Point(25, 162);
         this.buttonGetSystemInfo.Name = "buttonGetSystemInfo";
         this.buttonGetSystemInfo.Size = new System.Drawing.Size(122, 23);
         this.buttonGetSystemInfo.TabIndex = 0;
         this.buttonGetSystemInfo.Text = "Get System Info";
         this.buttonGetSystemInfo.UseVisualStyleBackColor = true;
         this.buttonGetSystemInfo.Click += new System.EventHandler(this.buttonGetSystemInfo_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(25, 22);
         this.textBox1.Multiline = true;
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(517, 134);
         this.textBox1.TabIndex = 1;
         // 
         // buttonSpecialFolders
         // 
         this.buttonSpecialFolders.Location = new System.Drawing.Point(173, 162);
         this.buttonSpecialFolders.Name = "buttonSpecialFolders";
         this.buttonSpecialFolders.Size = new System.Drawing.Size(100, 23);
         this.buttonSpecialFolders.TabIndex = 2;
         this.buttonSpecialFolders.Text = "Special Folders";
         this.buttonSpecialFolders.UseVisualStyleBackColor = true;
         this.buttonSpecialFolders.Click += new System.EventHandler(this.buttonSpecialFolders_Click);
         // 
         // formCheckConfiguration
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(582, 210);
         this.Controls.Add(this.buttonSpecialFolders);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.buttonGetSystemInfo);
         this.Name = "formCheckConfiguration";
         this.Text = "Form1";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonGetSystemInfo;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Button buttonSpecialFolders;
   }
}

