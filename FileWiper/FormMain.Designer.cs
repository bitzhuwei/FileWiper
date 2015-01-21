namespace FileWiper
{
    partial class FormMain
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
            this.btnWipeFiles = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnUnregister = new System.Windows.Forms.Button();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.btnDeleteFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWipeFiles
            // 
            this.btnWipeFiles.Location = new System.Drawing.Point(12, 12);
            this.btnWipeFiles.Name = "btnWipeFiles";
            this.btnWipeFiles.Size = new System.Drawing.Size(228, 23);
            this.btnWipeFiles.TabIndex = 0;
            this.btnWipeFiles.Text = "Wipe Files";
            this.btnWipeFiles.UseVisualStyleBackColor = true;
            this.btnWipeFiles.Click += new System.EventHandler(this.btnWipeFiles_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(12, 70);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(228, 23);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "Register for System Right Menu";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnUnregister
            // 
            this.btnUnregister.Location = new System.Drawing.Point(12, 99);
            this.btnUnregister.Name = "btnUnregister";
            this.btnUnregister.Size = new System.Drawing.Size(228, 23);
            this.btnUnregister.TabIndex = 0;
            this.btnUnregister.Text = "Unregister for System Right Menu";
            this.btnUnregister.UseVisualStyleBackColor = true;
            this.btnUnregister.Click += new System.EventHandler(this.btnUnregister_Click);
            // 
            // openFileDlg
            // 
            this.openFileDlg.Multiselect = true;
            this.openFileDlg.Title = "Select files to wipe their contents";
            // 
            // btnDeleteFiles
            // 
            this.btnDeleteFiles.Location = new System.Drawing.Point(12, 41);
            this.btnDeleteFiles.Name = "btnDeleteFiles";
            this.btnDeleteFiles.Size = new System.Drawing.Size(228, 23);
            this.btnDeleteFiles.TabIndex = 0;
            this.btnDeleteFiles.Text = "Delete Files";
            this.btnDeleteFiles.UseVisualStyleBackColor = true;
            this.btnDeleteFiles.Click += new System.EventHandler(this.btnDeleteFiles_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 131);
            this.Controls.Add(this.btnUnregister);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnDeleteFiles);
            this.Controls.Add(this.btnWipeFiles);
            this.Name = "FormMain";
            this.Text = "File Wiper @ http://bitzhuwei.cnblogs.com";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWipeFiles;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnUnregister;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.Button btnDeleteFiles;
    }
}