namespace PhilosopherProblemEmu
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLogArea = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputLogButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepIntoButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getHelpButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveLog = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxLogArea
            // 
            this.textBoxLogArea.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxLogArea.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBoxLogArea.Location = new System.Drawing.Point(620, 73);
            this.textBoxLogArea.Multiline = true;
            this.textBoxLogArea.Name = "textBoxLogArea";
            this.textBoxLogArea.ReadOnly = true;
            this.textBoxLogArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogArea.Size = new System.Drawing.Size(300, 420);
            this.textBoxLogArea.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuToolStripMenuItem,
            this.controlMenuToolStripMenuItem,
            this.helpMenuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1034, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuToolStripMenuItem
            // 
            this.fileMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputLogButtonToolStripMenuItem,
            this.settingsButtonToolStripMenuItem,
            this.exitButtonToolStripMenuItem});
            this.fileMenuToolStripMenuItem.Name = "fileMenuToolStripMenuItem";
            this.fileMenuToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.fileMenuToolStripMenuItem.Text = "文件(&F)";
            // 
            // outputLogButtonToolStripMenuItem
            // 
            this.outputLogButtonToolStripMenuItem.Name = "outputLogButtonToolStripMenuItem";
            this.outputLogButtonToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.outputLogButtonToolStripMenuItem.Text = "导出日志(&L)";
            this.outputLogButtonToolStripMenuItem.Click += new System.EventHandler(this.outputLogButtonToolStripMenuItem_Click);
            // 
            // settingsButtonToolStripMenuItem
            // 
            this.settingsButtonToolStripMenuItem.Name = "settingsButtonToolStripMenuItem";
            this.settingsButtonToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.settingsButtonToolStripMenuItem.Text = "设置(&U)";
            this.settingsButtonToolStripMenuItem.Click += new System.EventHandler(this.settingsButtonToolStripMenuItem_Click);
            // 
            // exitButtonToolStripMenuItem
            // 
            this.exitButtonToolStripMenuItem.Name = "exitButtonToolStripMenuItem";
            this.exitButtonToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitButtonToolStripMenuItem.Text = "退出(&X)";
            this.exitButtonToolStripMenuItem.Click += new System.EventHandler(this.exitButtonToolStripMenuItem_Click);
            // 
            // controlMenuToolStripMenuItem
            // 
            this.controlMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButtonToolStripMenuItem,
            this.pauseButtonToolStripMenuItem,
            this.stepIntoButtonToolStripMenuItem,
            this.resetButtonToolStripMenuItem});
            this.controlMenuToolStripMenuItem.Name = "controlMenuToolStripMenuItem";
            this.controlMenuToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.controlMenuToolStripMenuItem.Text = "控制(&C)";
            // 
            // startButtonToolStripMenuItem
            // 
            this.startButtonToolStripMenuItem.Name = "startButtonToolStripMenuItem";
            this.startButtonToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.startButtonToolStripMenuItem.Text = "开始(&I)";
            this.startButtonToolStripMenuItem.Click += new System.EventHandler(this.startButtonToolStripMenuItem_Click);
            // 
            // pauseButtonToolStripMenuItem
            // 
            this.pauseButtonToolStripMenuItem.Name = "pauseButtonToolStripMenuItem";
            this.pauseButtonToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.pauseButtonToolStripMenuItem.Text = "暂停(&P)";
            this.pauseButtonToolStripMenuItem.Click += new System.EventHandler(this.pauseButtonToolStripMenuItem_Click);
            // 
            // stepIntoButtonToolStripMenuItem
            // 
            this.stepIntoButtonToolStripMenuItem.Name = "stepIntoButtonToolStripMenuItem";
            this.stepIntoButtonToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.stepIntoButtonToolStripMenuItem.Text = "步进(&S)";
            this.stepIntoButtonToolStripMenuItem.Click += new System.EventHandler(this.stepIntoButtonToolStripMenuItem_Click);
            // 
            // resetButtonToolStripMenuItem
            // 
            this.resetButtonToolStripMenuItem.Name = "resetButtonToolStripMenuItem";
            this.resetButtonToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.resetButtonToolStripMenuItem.Text = "重置(&R)";
            this.resetButtonToolStripMenuItem.Click += new System.EventHandler(this.resetButtonToolStripMenuItem_Click);
            // 
            // helpMenuToolStripMenuItem
            // 
            this.helpMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getHelpButtonToolStripMenuItem,
            this.aboutButtonToolStripMenuItem});
            this.helpMenuToolStripMenuItem.Name = "helpMenuToolStripMenuItem";
            this.helpMenuToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.helpMenuToolStripMenuItem.Text = "帮助(&A)";
            // 
            // getHelpButtonToolStripMenuItem
            // 
            this.getHelpButtonToolStripMenuItem.Name = "getHelpButtonToolStripMenuItem";
            this.getHelpButtonToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.getHelpButtonToolStripMenuItem.Text = "获取帮助(&H)";
            // 
            // aboutButtonToolStripMenuItem
            // 
            this.aboutButtonToolStripMenuItem.Name = "aboutButtonToolStripMenuItem";
            this.aboutButtonToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aboutButtonToolStripMenuItem.Text = "关于(&C)";
            this.aboutButtonToolStripMenuItem.Click += new System.EventHandler(this.aboutButtonToolStripMenuItem_Click);
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Location = new System.Drawing.Point(650, 523);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(90, 30);
            this.buttonSaveLog.TabIndex = 2;
            this.buttonSaveLog.Text = "保存日志";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.buttonSaveLog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 621);
            this.Controls.Add(this.buttonSaveLog);
            this.Controls.Add(this.textBoxLogArea);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "测试 - 哲学家进餐过程模拟";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputLogButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepIntoButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getHelpButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutButtonToolStripMenuItem;
        public System.Windows.Forms.TextBox textBoxLogArea;
        private System.Windows.Forms.Button buttonSaveLog;
    }
}

