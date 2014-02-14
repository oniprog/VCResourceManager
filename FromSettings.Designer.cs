namespace VCResourceManager
{
    partial class FromSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FromSettings));
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefDataFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRcPath = new System.Windows.Forms.TextBox();
            this.txtResourceHPath = new System.Windows.Forms.TextBox();
            this.btnRefRcPath = new System.Windows.Forms.Button();
            this.btnRefResourceHPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Location = new System.Drawing.Point(108, 15);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(380, 19);
            this.txtDataFolder.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "データフォルダ";
            // 
            // btnRefDataFolder
            // 
            this.btnRefDataFolder.Location = new System.Drawing.Point(497, 15);
            this.btnRefDataFolder.Name = "btnRefDataFolder";
            this.btnRefDataFolder.Size = new System.Drawing.Size(37, 19);
            this.btnRefDataFolder.TabIndex = 4;
            this.btnRefDataFolder.Text = "参照";
            this.btnRefDataFolder.UseVisualStyleBackColor = true;
            this.btnRefDataFolder.Click += new System.EventHandler(this.btnRefDataFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "※無視リソース名は，ignore_dialog.txtに記述してください";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(378, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(459, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "rcファイルの場所";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "resource.hの場所";
            // 
            // txtRcPath
            // 
            this.txtRcPath.Location = new System.Drawing.Point(108, 40);
            this.txtRcPath.Name = "txtRcPath";
            this.txtRcPath.Size = new System.Drawing.Size(380, 19);
            this.txtRcPath.TabIndex = 10;
            // 
            // txtResourceHPath
            // 
            this.txtResourceHPath.Location = new System.Drawing.Point(108, 65);
            this.txtResourceHPath.Name = "txtResourceHPath";
            this.txtResourceHPath.Size = new System.Drawing.Size(380, 19);
            this.txtResourceHPath.TabIndex = 11;
            // 
            // btnRefRcPath
            // 
            this.btnRefRcPath.Location = new System.Drawing.Point(497, 40);
            this.btnRefRcPath.Name = "btnRefRcPath";
            this.btnRefRcPath.Size = new System.Drawing.Size(37, 19);
            this.btnRefRcPath.TabIndex = 12;
            this.btnRefRcPath.Text = "参照";
            this.btnRefRcPath.UseVisualStyleBackColor = true;
            this.btnRefRcPath.Click += new System.EventHandler(this.btnRefRcPath_Click);
            // 
            // btnRefResourceHPath
            // 
            this.btnRefResourceHPath.Location = new System.Drawing.Point(497, 65);
            this.btnRefResourceHPath.Name = "btnRefResourceHPath";
            this.btnRefResourceHPath.Size = new System.Drawing.Size(37, 19);
            this.btnRefResourceHPath.TabIndex = 13;
            this.btnRefResourceHPath.Text = "参照";
            this.btnRefResourceHPath.UseVisualStyleBackColor = true;
            this.btnRefResourceHPath.Click += new System.EventHandler(this.btnRefResourceHPath_Click);
            // 
            // FromSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 128);
            this.Controls.Add(this.btnRefResourceHPath);
            this.Controls.Add(this.btnRefRcPath);
            this.Controls.Add(this.txtResourceHPath);
            this.Controls.Add(this.txtRcPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRefDataFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDataFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FromSettings";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.FromSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefDataFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRcPath;
        private System.Windows.Forms.TextBox txtResourceHPath;
        private System.Windows.Forms.Button btnRefRcPath;
        private System.Windows.Forms.Button btnRefResourceHPath;
    }
}