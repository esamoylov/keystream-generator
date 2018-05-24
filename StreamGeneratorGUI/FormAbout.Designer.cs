namespace StreamGeneratorGUI
{
    partial class FormAbout
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
            this.lblProdName = new System.Windows.Forms.Label();
            this.lblProdVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProdName
            // 
            this.lblProdName.AutoSize = true;
            this.lblProdName.Location = new System.Drawing.Point(24, 30);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(38, 15);
            this.lblProdName.TabIndex = 0;
            this.lblProdName.Text = "label1";
            // 
            // lblProdVersion
            // 
            this.lblProdVersion.AutoSize = true;
            this.lblProdVersion.Location = new System.Drawing.Point(24, 74);
            this.lblProdVersion.Name = "lblProdVersion";
            this.lblProdVersion.Size = new System.Drawing.Size(38, 15);
            this.lblProdVersion.TabIndex = 1;
            this.lblProdVersion.Text = "label2";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(24, 121);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(38, 15);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "label3";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(107, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 194);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblProdVersion);
            this.Controls.Add(this.lblProdName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAbout";
            this.Text = "О Программе";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProdName;
        private System.Windows.Forms.Label lblProdVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Button btnOK;
    }
}