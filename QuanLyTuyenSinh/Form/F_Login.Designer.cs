namespace QuanLyTuyenSinh.Form
{
    partial class F_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Login));
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            panel2 = new Panel();
            pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            btnLogin = new DevExpress.XtraEditors.SimpleButton();
            lblExit = new DevExpress.XtraEditors.LabelControl();
            txtName = new DevExpress.XtraEditors.TextEdit();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            spinNam = new DevExpress.XtraEditors.SpinEdit();
            txtPass = new DevExpress.XtraEditors.TextEdit();
            splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(F_Wait), true, true, true);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spinNam.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPass.Properties).BeginInit();
            SuspendLayout();
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new Font("Bahnschrift Condensed", 24F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl1.Appearance.ForeColor = Color.DodgerBlue;
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Appearance.Options.UseForeColor = true;
            labelControl1.Location = new Point(129, 267);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(141, 48);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "Đăng nhập";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(118, 78);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(195, 168);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CornflowerBlue;
            panel1.Location = new Point(50, 426);
            panel1.Name = "panel1";
            panel1.Size = new Size(321, 1);
            panel1.TabIndex = 4;
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            pictureEdit1.Location = new Point(50, 381);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Size = new Size(39, 39);
            pictureEdit1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.CornflowerBlue;
            panel2.Location = new Point(51, 480);
            panel2.Name = "panel2";
            panel2.Size = new Size(321, 1);
            panel2.TabIndex = 4;
            // 
            // pictureEdit2
            // 
            pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            pictureEdit2.Location = new Point(51, 435);
            pictureEdit2.Name = "pictureEdit2";
            pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit2.Size = new Size(39, 39);
            pictureEdit2.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            btnLogin.Appearance.Font = new Font("Berlin Sans FB Demi", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.Appearance.Options.UseBackColor = true;
            btnLogin.Appearance.Options.UseFont = true;
            btnLogin.Location = new Point(51, 508);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(321, 36);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Đăng nhập";
            btnLogin.Click += btnLogin_Click;
            // 
            // lblExit
            // 
            lblExit.Appearance.Font = new Font("Bahnschrift SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblExit.Appearance.ForeColor = Color.DodgerBlue;
            lblExit.Appearance.Options.UseFont = true;
            lblExit.Appearance.Options.UseForeColor = true;
            lblExit.Location = new Point(181, 580);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(50, 24);
            lblExit.TabIndex = 7;
            lblExit.Text = "Thoát";
            lblExit.Click += lblExit_Click;
            // 
            // txtName
            // 
            txtName.EditValue = "admin";
            txtName.Location = new Point(96, 389);
            txtName.Name = "txtName";
            txtName.Properties.Appearance.Font = new Font("Bahnschrift Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtName.Properties.Appearance.Options.UseFont = true;
            txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtName.Size = new Size(262, 28);
            txtName.TabIndex = 8;
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new Font("Bahnschrift SemiBold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            labelControl3.Appearance.ForeColor = Color.DodgerBlue;
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Appearance.Options.UseForeColor = true;
            labelControl3.Location = new Point(82, 338);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(118, 21);
            labelControl3.TabIndex = 7;
            labelControl3.Text = "Năm tuyển sinh";
            // 
            // spinNam
            // 
            spinNam.EditValue = new decimal(new int[] { 2023, 0, 0, 0 });
            spinNam.Location = new Point(235, 335);
            spinNam.Name = "spinNam";
            spinNam.Properties.Appearance.Font = new Font("Bahnschrift SemiBold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            spinNam.Properties.Appearance.ForeColor = Color.DimGray;
            spinNam.Properties.Appearance.Options.UseFont = true;
            spinNam.Properties.Appearance.Options.UseForeColor = true;
            spinNam.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            spinNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            spinNam.Properties.IsFloatValue = false;
            spinNam.Properties.MaskSettings.Set("mask", "d");
            spinNam.Properties.MaxValue = new decimal(new int[] { 2099, 0, 0, 0 });
            spinNam.Properties.MinValue = new decimal(new int[] { 2000, 0, 0, 0 });
            spinNam.Size = new Size(95, 26);
            spinNam.TabIndex = 9;
            // 
            // txtPass
            // 
            txtPass.EditValue = "1";
            txtPass.Location = new Point(96, 446);
            txtPass.Name = "txtPass";
            txtPass.Properties.Appearance.Font = new Font("Bahnschrift Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPass.Properties.Appearance.Options.UseFont = true;
            txtPass.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtPass.Properties.UseSystemPasswordChar = true;
            txtPass.Size = new Size(262, 28);
            txtPass.TabIndex = 8;
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // F_Login
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(426, 616);
            Controls.Add(spinNam);
            Controls.Add(txtPass);
            Controls.Add(txtName);
            Controls.Add(labelControl3);
            Controls.Add(lblExit);
            Controls.Add(btnLogin);
            Controls.Add(pictureEdit2);
            Controls.Add(panel2);
            Controls.Add(pictureEdit1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(labelControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "F_Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tuyển sinh";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)spinNam.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPass.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private Panel panel2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.LabelControl lblExit;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spinNam;
        private DevExpress.XtraEditors.TextEdit txtPass;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}