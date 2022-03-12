using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPhoneBook.Models;
using MyPhoneBook.Classes;

namespace MyPhoneBook
{
    public partial class frmContact : Form
    {
        private Contact _contact;
        private General.FormAction _action;

        public delegate void ContactFormCloseEvent();
        public ContactFormCloseEvent ContactFormCloseSave;

        public frmContact(Contact parentContact, General.FormAction parentAction)
        {
            InitializeComponent();
            Text = $"{General.ApplicationTitle} - Contact Form";

            _contact = parentContact;
            _action = parentAction;

            if (_contact.Id != 0)
            {
                txtName.Text = _contact.Name;
                txtPhone.Text = _contact.Phone;
                txtAddress.Text = _contact.Address;
            }

            switch (_action)
            {
                case General.FormAction.Add:
                    btnAction.Text = "Create New Contact";
                    break;
                case General.FormAction.Modify:
                    btnAction.Text = "Update Contact";
                    break;
                case General.FormAction.Delete:
                    btnAction.Text = "Remove Contact";
                    break;
            }
        }

        private void ValidateTextbox(TextBox textBox, CancelEventArgs e, string message)
        {
            if (String.IsNullOrEmpty(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                errorProvider.SetError(textBox, message);
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBox, null);
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
                return;

            var message = "";
            
            switch (_action)
            {
                case General.FormAction.Add:
                    message = "Are you sure you want to add this contact?\r\n\r\nplease click YES to proceed and NO to return to form.";
                    break;
                case General.FormAction.Modify:
                    message = "Are you sure you want to modify this contact?\r\n\r\nplease click YES to proceed and NO to return to form.";
                    break;
            }

            var confirmResult = General.ShowMessage(message, General.ShowMessageType.Confirmation);

            if (confirmResult == DialogResult.Yes)
            {
                _contact.Name = txtName.Text;
                _contact.Phone = txtPhone.Text;
                _contact.Address = txtAddress.Text;

                ContactFormCloseSave();
                Close();
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextbox(txtName, e, "Please provide contact name.");
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextbox(txtPhone, e, "Please provide contact phone number.");
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextbox(txtAddress, e, "Please provide contact address.");
        }
    }
}
