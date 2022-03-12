
namespace MyPhoneBook
{
    partial class frmContact
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
            this.components = new System.ComponentModel.Container();
            this.lblCaptionName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCaptionPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblCaptionAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCaptionName
            // 
            this.lblCaptionName.AutoSize = true;
            this.lblCaptionName.Location = new System.Drawing.Point(13, 13);
            this.lblCaptionName.Name = "lblCaptionName";
            this.lblCaptionName.Size = new System.Drawing.Size(57, 13);
            this.lblCaptionName.TabIndex = 0;
            this.lblCaptionName.Text = "Full Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 10);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(346, 20);
            this.txtName.TabIndex = 0;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // lblCaptionPhone
            // 
            this.lblCaptionPhone.AutoSize = true;
            this.lblCaptionPhone.Location = new System.Drawing.Point(13, 45);
            this.lblCaptionPhone.Name = "lblCaptionPhone";
            this.lblCaptionPhone.Size = new System.Drawing.Size(81, 13);
            this.lblCaptionPhone.TabIndex = 2;
            this.lblCaptionPhone.Text = "Phone Number:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(101, 42);
            this.txtPhone.MaxLength = 20;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(77, 20);
            this.txtPhone.TabIndex = 1;
            this.txtPhone.Validating += new System.ComponentModel.CancelEventHandler(this.txtPhone_Validating);
            // 
            // lblCaptionAddress
            // 
            this.lblCaptionAddress.AutoSize = true;
            this.lblCaptionAddress.Location = new System.Drawing.Point(13, 78);
            this.lblCaptionAddress.Name = "lblCaptionAddress";
            this.lblCaptionAddress.Size = new System.Drawing.Size(48, 13);
            this.lblCaptionAddress.TabIndex = 4;
            this.lblCaptionAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(101, 75);
            this.txtAddress.MaxLength = 250;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(346, 20);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(this.txtAddress_Validating);
            // 
            // btnAction
            // 
            this.btnAction.BackColor = System.Drawing.Color.DarkBlue;
            this.btnAction.ForeColor = System.Drawing.Color.White;
            this.btnAction.Location = new System.Drawing.Point(16, 112);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(431, 35);
            this.btnAction.TabIndex = 3;
            this.btnAction.Text = "ACTION";
            this.btnAction.UseVisualStyleBackColor = false;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frmContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 161);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblCaptionAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblCaptionPhone);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCaptionName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Contact Detail";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCaptionName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCaptionPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblCaptionAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}