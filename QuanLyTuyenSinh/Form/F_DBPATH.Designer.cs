namespace QuanLyTuyenSinh.Form
{
    partial class F_DBPATH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_DBPATH));
            directXFormContainerControl1 = new DevExpress.XtraEditors.DirectXFormContainerControl();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            btnOpenPath = new DevExpress.XtraEditors.SimpleButton();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            txtDbPath = new DevExpress.XtraEditors.MemoEdit();
            directXFormContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtDbPath.Properties).BeginInit();
            SuspendLayout();
            // 
            // directXFormContainerControl1
            // 
            directXFormContainerControl1.Controls.Add(btnCancel);
            directXFormContainerControl1.Controls.Add(btnSave);
            directXFormContainerControl1.Controls.Add(btnOpenPath);
            directXFormContainerControl1.Controls.Add(labelControl3);
            directXFormContainerControl1.Controls.Add(txtDbPath);
            directXFormContainerControl1.Location = new Point(1, 31);
            directXFormContainerControl1.Name = "directXFormContainerControl1";
            directXFormContainerControl1.Size = new Size(508, 125);
            directXFormContainerControl1.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.ImageOptions.Image = (Image)resources.GetObject("btnCancel.ImageOptions.Image");
            btnCancel.Location = new Point(286, 86);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 45;
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.ImageOptions.Image = (Image)resources.GetObject("btnSave.ImageOptions.Image");
            btnSave.Location = new Point(158, 86);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 46;
            btnSave.Text = "Lưu";
            btnSave.Click += btnSave_Click;
            // 
            // btnOpenPath
            // 
            btnOpenPath.ImageOptions.Image = (Image)resources.GetObject("btnOpenPath.ImageOptions.Image");
            btnOpenPath.Location = new Point(460, 29);
            btnOpenPath.Name = "btnOpenPath";
            btnOpenPath.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnOpenPath.Size = new Size(22, 22);
            btnOpenPath.TabIndex = 44;
            btnOpenPath.Click += btnOpenPath_Click;
            // 
            // labelControl3
            // 
            labelControl3.Location = new Point(21, 29);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(73, 13);
            labelControl3.TabIndex = 42;
            labelControl3.Text = "Vị trí lưu dữ liệu";
            // 
            // txtDbPath
            // 
            txtDbPath.Location = new Point(113, 26);
            txtDbPath.Name = "txtDbPath";
            txtDbPath.Size = new Size(330, 45);
            txtDbPath.TabIndex = 43;
            // 
            // F_DBPATH
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ChildControls.Add(directXFormContainerControl1);
            ClientSize = new Size(510, 157);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_DBPATH";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đường dẫn lưu dữ liệu";
            directXFormContainerControl1.ResumeLayout(false);
            directXFormContainerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtDbPath.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.DirectXFormContainerControl directXFormContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnOpenPath;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.MemoEdit txtDbPath;
    }
}