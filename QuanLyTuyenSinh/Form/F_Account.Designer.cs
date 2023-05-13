namespace QuanLyTuyenSinh.Form
{
    partial class F_Account
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Account));
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtAccountName = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            txtHoVaTen = new DevExpress.XtraEditors.TextEdit();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            txtNewPass = new DevExpress.XtraEditors.TextEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            txtNewPassRe = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)txtAccountName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtHoVaTen.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtNewPass.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtNewPassRe.Properties).BeginInit();
            SuspendLayout();
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(54, 38);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(56, 16);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Tài khoản";
            // 
            // txtAccountName
            // 
            txtAccountName.Enabled = false;
            txtAccountName.Location = new Point(199, 35);
            txtAccountName.Name = "txtAccountName";
            txtAccountName.Size = new Size(156, 22);
            txtAccountName.TabIndex = 0;
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(54, 84);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(54, 16);
            labelControl2.TabIndex = 0;
            labelControl2.Text = "Họ và tên";
            // 
            // txtHoVaTen
            // 
            txtHoVaTen.Location = new Point(199, 81);
            txtHoVaTen.Name = "txtHoVaTen";
            txtHoVaTen.Size = new Size(156, 22);
            txtHoVaTen.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.ImageOptions.Image = (Image)resources.GetObject("btnCancel.ImageOptions.Image");
            btnCancel.Location = new Point(226, 245);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 24);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.ImageOptions.Image = (Image)resources.GetObject("btnSave.ImageOptions.Image");
            btnSave.Location = new Point(88, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 24);
            btnSave.TabIndex = 3;
            btnSave.Text = "Lưu";
            btnSave.Click += btnSave_Click;
            // 
            // labelControl3
            // 
            labelControl3.Location = new Point(54, 127);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(77, 16);
            labelControl3.TabIndex = 0;
            labelControl3.Text = "Mật khẩu mới";
            // 
            // txtNewPass
            // 
            txtNewPass.Location = new Point(199, 121);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.Properties.UseSystemPasswordChar = true;
            txtNewPass.Size = new Size(156, 22);
            txtNewPass.TabIndex = 1;
            // 
            // labelControl4
            // 
            labelControl4.Location = new Point(54, 170);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new Size(128, 16);
            labelControl4.TabIndex = 0;
            labelControl4.Text = "Nhập lại mật khẩu mời";
            // 
            // txtNewPassRe
            // 
            txtNewPassRe.Location = new Point(199, 164);
            txtNewPassRe.Name = "txtNewPassRe";
            txtNewPassRe.Properties.UseSystemPasswordChar = true;
            txtNewPassRe.Size = new Size(156, 22);
            txtNewPassRe.TabIndex = 2;
            // 
            // F_Account
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 286);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtNewPassRe);
            Controls.Add(labelControl4);
            Controls.Add(txtNewPass);
            Controls.Add(labelControl3);
            Controls.Add(txtHoVaTen);
            Controls.Add(labelControl2);
            Controls.Add(txtAccountName);
            Controls.Add(labelControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "F_Account";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tài khoản";
            ((System.ComponentModel.ISupportInitialize)txtAccountName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtHoVaTen.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtNewPass.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtNewPassRe.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAccountName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtHoVaTen;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNewPass;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtNewPassRe;
    }
}