namespace VCResourceManager
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxRes = new System.Windows.Forms.ListBox();
            this.listBoxCommon = new System.Windows.Forms.ListBox();
            this.btnToCommon = new System.Windows.Forms.Button();
            this.btnToResUpdate = new System.Windows.Forms.Button();
            this.btnRefreshRes = new System.Windows.Forms.Button();
            this.btnRefreshCommon = new System.Windows.Forms.Button();
            this.chkShowDiffrence = new System.Windows.Forms.CheckBox();
            this.btnToResAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectAllRes = new System.Windows.Forms.Button();
            this.btnSelectAllCommon = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新規作成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.過去に開いたファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "リソースファイル";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "共通領域";
            // 
            // listBoxRes
            // 
            this.listBoxRes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.listBoxRes.FormattingEnabled = true;
            this.listBoxRes.ItemHeight = 12;
            this.listBoxRes.Location = new System.Drawing.Point(16, 72);
            this.listBoxRes.Name = "listBoxRes";
            this.listBoxRes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxRes.Size = new System.Drawing.Size(280, 376);
            this.listBoxRes.Sorted = true;
            this.listBoxRes.TabIndex = 4;
            // 
            // listBoxCommon
            // 
            this.listBoxCommon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.listBoxCommon.FormattingEnabled = true;
            this.listBoxCommon.ItemHeight = 12;
            this.listBoxCommon.Location = new System.Drawing.Point(423, 72);
            this.listBoxCommon.Name = "listBoxCommon";
            this.listBoxCommon.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCommon.Size = new System.Drawing.Size(292, 376);
            this.listBoxCommon.Sorted = true;
            this.listBoxCommon.TabIndex = 5;
            // 
            // btnToCommon
            // 
            this.btnToCommon.Location = new System.Drawing.Point(313, 204);
            this.btnToCommon.Name = "btnToCommon";
            this.btnToCommon.Size = new System.Drawing.Size(92, 40);
            this.btnToCommon.TabIndex = 6;
            this.btnToCommon.Text = "→";
            this.btnToCommon.UseVisualStyleBackColor = true;
            this.btnToCommon.Click += new System.EventHandler(this.btnToCommon_Click);
            // 
            // btnToResUpdate
            // 
            this.btnToResUpdate.Location = new System.Drawing.Point(313, 269);
            this.btnToResUpdate.Name = "btnToResUpdate";
            this.btnToResUpdate.Size = new System.Drawing.Size(92, 40);
            this.btnToResUpdate.TabIndex = 7;
            this.btnToResUpdate.Text = "←  更新";
            this.btnToResUpdate.UseVisualStyleBackColor = true;
            this.btnToResUpdate.Click += new System.EventHandler(this.btnToRes_Click);
            // 
            // btnRefreshRes
            // 
            this.btnRefreshRes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefreshRes.Location = new System.Drawing.Point(16, 454);
            this.btnRefreshRes.Name = "btnRefreshRes";
            this.btnRefreshRes.Size = new System.Drawing.Size(92, 40);
            this.btnRefreshRes.TabIndex = 9;
            this.btnRefreshRes.Text = "更新";
            this.btnRefreshRes.UseVisualStyleBackColor = true;
            this.btnRefreshRes.Click += new System.EventHandler(this.btnRefreshRes_Click);
            // 
            // btnRefreshCommon
            // 
            this.btnRefreshCommon.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefreshCommon.Location = new System.Drawing.Point(423, 454);
            this.btnRefreshCommon.Name = "btnRefreshCommon";
            this.btnRefreshCommon.Size = new System.Drawing.Size(92, 40);
            this.btnRefreshCommon.TabIndex = 10;
            this.btnRefreshCommon.Text = "更新";
            this.btnRefreshCommon.UseVisualStyleBackColor = true;
            this.btnRefreshCommon.Click += new System.EventHandler(this.btnRefreshCommon_Click);
            // 
            // chkShowDiffrence
            // 
            this.chkShowDiffrence.AutoSize = true;
            this.chkShowDiffrence.Location = new System.Drawing.Point(104, 56);
            this.chkShowDiffrence.Name = "chkShowDiffrence";
            this.chkShowDiffrence.Size = new System.Drawing.Size(174, 16);
            this.chkShowDiffrence.TabIndex = 11;
            this.chkShowDiffrence.Text = "差異があるリソースだけ表示する";
            this.chkShowDiffrence.UseVisualStyleBackColor = true;
            this.chkShowDiffrence.CheckedChanged += new System.EventHandler(this.chkShowDiffrence_CheckedChanged);
            // 
            // btnToResAdd
            // 
            this.btnToResAdd.Location = new System.Drawing.Point(313, 357);
            this.btnToResAdd.Name = "btnToResAdd";
            this.btnToResAdd.Size = new System.Drawing.Size(92, 40);
            this.btnToResAdd.TabIndex = 12;
            this.btnToResAdd.Text = "←  追加";
            this.btnToResAdd.UseVisualStyleBackColor = true;
            this.btnToResAdd.Click += new System.EventHandler(this.btnToResAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "※すでにリソースにある\r\nものだけを更新します";
            // 
            // btnSelectAllRes
            // 
            this.btnSelectAllRes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectAllRes.Location = new System.Drawing.Point(114, 454);
            this.btnSelectAllRes.Name = "btnSelectAllRes";
            this.btnSelectAllRes.Size = new System.Drawing.Size(92, 40);
            this.btnSelectAllRes.TabIndex = 14;
            this.btnSelectAllRes.Text = "全選択";
            this.btnSelectAllRes.UseVisualStyleBackColor = true;
            this.btnSelectAllRes.Click += new System.EventHandler(this.btnSelectAllRes_Click);
            // 
            // btnSelectAllCommon
            // 
            this.btnSelectAllCommon.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectAllCommon.Location = new System.Drawing.Point(521, 454);
            this.btnSelectAllCommon.Name = "btnSelectAllCommon";
            this.btnSelectAllCommon.Size = new System.Drawing.Size(92, 40);
            this.btnSelectAllCommon.TabIndex = 16;
            this.btnSelectAllCommon.Text = "全選択";
            this.btnSelectAllCommon.UseVisualStyleBackColor = true;
            this.btnSelectAllCommon.Click += new System.EventHandler(this.btnSelectAllCommon_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(727, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.保存ToolStripMenuItem,
            this.過去に開いたファイルToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成ToolStripMenuItem
            // 
            this.新規作成ToolStripMenuItem.Name = "新規作成ToolStripMenuItem";
            this.新規作成ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.新規作成ToolStripMenuItem.Text = "新規作成(&N)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem2.Text = "開く(&O)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.保存ToolStripMenuItem.Text = "保存(&S)";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 過去に開いたファイルToolStripMenuItem
            // 
            this.過去に開いたファイルToolStripMenuItem.Name = "過去に開いたファイルToolStripMenuItem";
            this.過去に開いたファイルToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.過去に開いたファイルToolStripMenuItem.Text = "最近使ったファイル";
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.設定ToolStripMenuItem.Text = "設定(&S)";
            this.設定ToolStripMenuItem.Click += new System.EventHandler(this.設定ToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 504);
            this.Controls.Add(this.btnSelectAllCommon);
            this.Controls.Add(this.btnSelectAllRes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnToResAdd);
            this.Controls.Add(this.chkShowDiffrence);
            this.Controls.Add(this.btnRefreshCommon);
            this.Controls.Add(this.btnRefreshRes);
            this.Controls.Add(this.btnToResUpdate);
            this.Controls.Add(this.btnToCommon);
            this.Controls.Add(this.listBoxCommon);
            this.Controls.Add(this.listBoxRes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "VCのダイアログリソース管理システム";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxRes;
        private System.Windows.Forms.ListBox listBoxCommon;
        private System.Windows.Forms.Button btnToCommon;
        private System.Windows.Forms.Button btnToResUpdate;
        private System.Windows.Forms.Button btnRefreshRes;
        private System.Windows.Forms.Button btnRefreshCommon;
        private System.Windows.Forms.CheckBox chkShowDiffrence;
        private System.Windows.Forms.Button btnToResAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectAllRes;
        private System.Windows.Forms.Button btnSelectAllCommon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新規作成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 過去に開いたファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
    }
}

