
namespace DhcpClassClient
{
    partial class frmMain
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llUpdateDiscover = new System.Windows.Forms.LinkLabel();
            this.comboDiscover = new System.Windows.Forms.ComboBox();
            this.llRequest = new System.Windows.Forms.LinkLabel();
            this.llDiscover = new System.Windows.Forms.LinkLabel();
            this.comboInterface = new System.Windows.Forms.ComboBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabClient = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 25);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(58, 21);
            this.mnuFile.Text = "文件(&F)";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(116, 22);
            this.mnuFileExit.Text = "退出(&X)";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llUpdateDiscover);
            this.panel1.Controls.Add(this.comboDiscover);
            this.panel1.Controls.Add(this.llRequest);
            this.panel1.Controls.Add(this.llDiscover);
            this.panel1.Controls.Add(this.comboInterface);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 57);
            this.panel1.TabIndex = 1;
            // 
            // llUpdateDiscover
            // 
            this.llUpdateDiscover.AutoSize = true;
            this.llUpdateDiscover.Location = new System.Drawing.Point(579, 24);
            this.llUpdateDiscover.Name = "llUpdateDiscover";
            this.llUpdateDiscover.Size = new System.Drawing.Size(77, 12);
            this.llUpdateDiscover.TabIndex = 4;
            this.llUpdateDiscover.TabStop = true;
            this.llUpdateDiscover.Text = "更新Discover";
            this.llUpdateDiscover.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llUpdateDiscover_LinkClicked);
            // 
            // comboDiscover
            // 
            this.comboDiscover.FormattingEnabled = true;
            this.comboDiscover.Location = new System.Drawing.Point(350, 16);
            this.comboDiscover.Name = "comboDiscover";
            this.comboDiscover.Size = new System.Drawing.Size(214, 20);
            this.comboDiscover.TabIndex = 3;
            this.comboDiscover.SelectedIndexChanged += new System.EventHandler(this.comboDiscover_SelectedIndexChanged);
            // 
            // llRequest
            // 
            this.llRequest.AutoSize = true;
            this.llRequest.Location = new System.Drawing.Point(662, 24);
            this.llRequest.Name = "llRequest";
            this.llRequest.Size = new System.Drawing.Size(71, 12);
            this.llRequest.TabIndex = 2;
            this.llRequest.TabStop = true;
            this.llRequest.Text = "发送Request";
            this.llRequest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // llDiscover
            // 
            this.llDiscover.AutoSize = true;
            this.llDiscover.Location = new System.Drawing.Point(250, 19);
            this.llDiscover.Name = "llDiscover";
            this.llDiscover.Size = new System.Drawing.Size(77, 12);
            this.llDiscover.TabIndex = 1;
            this.llDiscover.TabStop = true;
            this.llDiscover.Text = "发送Discover";
            this.llDiscover.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDiscover_LinkClicked);
            // 
            // comboInterface
            // 
            this.comboInterface.FormattingEnabled = true;
            this.comboInterface.Location = new System.Drawing.Point(12, 16);
            this.comboInterface.Name = "comboInterface";
            this.comboInterface.Size = new System.Drawing.Size(214, 20);
            this.comboInterface.TabIndex = 0;
            this.comboInterface.SelectedIndexChanged += new System.EventHandler(this.comboInterface_SelectedIndexChanged);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabClient);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 82);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(800, 368);
            this.tcMain.TabIndex = 2;
            // 
            // tabClient
            // 
            this.tabClient.Controls.Add(this.txtLog);
            this.tabClient.Controls.Add(this.panel2);
            this.tabClient.Location = new System.Drawing.Point(4, 22);
            this.tabClient.Name = "tabClient";
            this.tabClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabClient.Size = new System.Drawing.Size(792, 342);
            this.tabClient.TabIndex = 0;
            this.tabClient.Text = "客户消息";
            this.tabClient.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(786, 274);
            this.txtLog.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 62);
            this.panel2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 342);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Text = "客户端";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tabClient.ResumeLayout(false);
            this.tabClient.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabClient;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboInterface;
        private System.Windows.Forms.LinkLabel llDiscover;
        private System.Windows.Forms.LinkLabel llRequest;
        private System.Windows.Forms.ComboBox comboDiscover;
        private System.Windows.Forms.LinkLabel llUpdateDiscover;
    }
}

