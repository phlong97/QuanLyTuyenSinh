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
            labelControl1.Location = new Point(117, 210);
            labelControl1.Margin = new Padding(2);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(111, 39);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "Đăng nhập";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(94, 62);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(156, 134);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CornflowerBlue;
            panel1.Location = new Point(38, 352);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(257, 1);
            panel1.TabIndex = 4;
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            pictureEdit1.Location = new Point(38, 316);
            pictureEdit1.Margin = new Padding(2);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Size = new Size(31, 31);
            pictureEdit1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.CornflowerBlue;
            panel2.Location = new Point(39, 395);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(257, 1);
            panel2.TabIndex = 4;
            // 
            // pictureEdit2
            // 
            pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            pictureEdit2.Location = new Point(39, 359);
            pictureEdit2.Margin = new Padding(2);
            pictureEdit2.Name = "pictureEdit2";
            pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit2.Size = new Size(31, 31);
            pictureEdit2.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            btnLogin.Appearance.Font = new Font("Berlin Sans FB Demi", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.Appearance.Options.UseBackColor = true;
            btnLogin.Appearance.Options.UseFont = true;
            btnLogin.Location = new Point(44, 417);
            btnLogin.Margin = new Padding(2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(256, 29);
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
            lblExit.Location = new Point(152, 475);
            lblExit.Margin = new Padding(2);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(40, 19);
            lblExit.TabIndex = 7;
            lblExit.Text = "Thoát";
            lblExit.Click += lblExit_Click;
            // 
            // txtName
            // 
            txtName.EditValue = "";
            txtName.Location = new Point(75, 322);
            txtName.Margin = new Padding(2);
            txtName.Name = "txtName";
            txtName.Properties.Appearance.Font = new Font("Bahnschrift Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtName.Properties.Appearance.Options.UseFont = true;
            txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtName.Size = new Size(210, 24);
            txtName.TabIndex = 8;
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new Font("Bahnschrift SemiBold", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            labelControl3.Appearance.ForeColor = Color.DodgerBlue;
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Appearance.Options.UseForeColor = true;
            labelControl3.Location = new Point(75, 270);
            labelControl3.Margin = new Padding(2);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(100, 17);
            labelControl3.TabIndex = 7;
            labelControl3.Text = "Năm tuyển sinh";
            // 
            // spinNam
            // 
            spinNam.EditValue = new decimal(new int[] { 2023, 0, 0, 0 });
            spinNam.Location = new Point(188, 268);
            spinNam.Margin = new Padding(2);
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
            spinNam.Size = new Size(62, 22);
            spinNam.TabIndex = 9;
            // 
            // txtPass
            // 
            txtPass.EditValue = "";
            txtPass.Location = new Point(75, 364);
            txtPass.Margin = new Padding(2);
            txtPass.Name = "txtPass";
            txtPass.Properties.Appearance.Font = new Font("Bahnschrift Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPass.Properties.Appearance.Options.UseFont = true;
            txtPass.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtPass.Properties.UseSystemPasswordChar = true;
            txtPass.Size = new Size(210, 24);
            txtPass.TabIndex = 8;
            // 
            // F_Login
            // 
            AcceptButton = btnLogin;
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(348, 535);
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
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "F_Login";
            StartPosition = FormStartPosition.CenterScreen;
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
    }
}