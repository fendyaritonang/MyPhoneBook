using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPhoneBook.Classes;
using MyPhoneBook.Models;

namespace MyPhoneBook
{
    public partial class MainForm : Form
    {
        private PhoneBook _phoneBook = new PhoneBook();
        private List<Contact> _contactView = new List<Contact>();
        private BindingSource _bindingSource = new BindingSource();
        private Contact _contact;
        private General.FormAction _action;
        private DataSync _dataSync = new DataSync(General.PhoneBookPath);
        private int _sortIndex = 1;

        public MainForm()
        {
            InitializeComponent();
            Text = General.ApplicationTitle;
        }

        private void OpenContactForm(frmContact contactForm)
        {
            contactForm.ContactFormCloseSave += ContactFormCloseSave;
            contactForm.ShowDialog();
        }

        private void ContactFormCloseSave()
        {
            try
            {
                switch (_action)
                {
                    case General.FormAction.Add:
                        _phoneBook.AddContact(_contact);
                        txtSearch.Text = "";
                        break;
                    case General.FormAction.Modify:
                        _phoneBook.ModifyContact(_contact);
                        break;
                }
                _dataSync.SavePhoneBook(_phoneBook);

                _contactView = new List<Contact>(_phoneBook.FindContact(txtSearch.Text));

                if (_action == General.FormAction.Modify)
                {
                    if (_contactView.FindIndex(c => c.Id == _contact.Id) == -1)
                    {
                        _contactView.Add(_contact);
                    }
                }

                RefreshDatagrid(_contactView);
            }
            catch (Exception ex)
            {
                General.ShowMessage($"ContactFormCloseSave - {ex.Message}", General.ShowMessageType.Error);                
            }
        }

        private void RefreshDatagrid(List<Contact> contacts)
        {
            switch (_sortIndex)
            {
                case 3:
                    _contactView = new List<Contact>(contacts.OrderBy(x => x.Address).ToList());
                    break;
                case 2:
                    _contactView = new List<Contact>(contacts.OrderBy(x => x.Phone).ToList());
                    break;
                case 0:
                    _contactView = new List<Contact>(contacts.OrderBy(x => x.Id).ToList());
                    break;
                default:
                    _contactView = new List<Contact>(contacts.OrderBy(x => x.Name).ToList());
                    break;
            }

            _bindingSource.DataSource = _contactView;            
            lblTotal.Text = _contactView.Count.ToString();

            if (dgrMain.Columns.Count > 0)
            {
                dgrMain.Columns[_sortIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

                if (_contact != null)
                {
                    int idx = _contactView.FindIndex(c => c.Id == _contact.Id);
                    if (idx >= 0)
                        dgrMain.CurrentCell = dgrMain.Rows[idx].Cells[0];
                }
            }                
        }

        private bool EligibleModifyDeleteAction()
        {
            if (dgrMain.Rows.Count == 0)
            {
                General.ShowMessage("You don't have any contact to be removed or modified", General.ShowMessageType.Information);
                return false;
            }
            else if (dgrMain.CurrentCell == null)
            {
                General.ShowMessage("Please select from list the contact that'd like to remove or modify", General.ShowMessageType.Information);
                return false;
            }
            return true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            General.Log.Info("Application started");

            try
            {
                _phoneBook = _dataSync.LoadSavedPhoneBook;

                RefreshDatagrid(_phoneBook.Contacts);

                dgrMain.DataSource = _bindingSource;
                dgrMain.ReadOnly = true;

                dgrMain.Columns[0].Width = 60;
                dgrMain.Columns[1].Width = 150;
                dgrMain.Columns[1].HeaderText = "Full Name";
                dgrMain.Columns[2].Width = 120;
                dgrMain.Columns[2].HeaderText = "Phone Number";
                dgrMain.Columns[3].Width = 400;
                
                dgrMain.Columns[_sortIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }
            catch (Exception ex)
            {                
                General.ShowMessage($"MainForm_Load - {ex.Message}", General.ShowMessageType.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _contact = new Contact();
                _action = General.FormAction.Add;
                var contactForm = new frmContact(_contact, _action);
                OpenContactForm(contactForm);
            }
            catch (Exception ex)
            {
                General.ShowMessage($"btnAdd_Click - {ex.Message}", General.ShowMessageType.Error);
            }            
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (!EligibleModifyDeleteAction())
                return;
            try
            {
                int currentCellRowIndex = dgrMain.CurrentCell.RowIndex;
                _contact = new Contact
                {
                    Id = Convert.ToInt32(dgrMain.Rows[currentCellRowIndex].Cells[0].Value),
                    Name = dgrMain.Rows[currentCellRowIndex].Cells[1].Value.ToString(),
                    Phone = dgrMain.Rows[currentCellRowIndex].Cells[2].Value.ToString(),
                    Address = dgrMain.Rows[currentCellRowIndex].Cells[3].Value.ToString()
                };

                _action = General.FormAction.Modify;
                var contactForm = new frmContact(_contact, _action);
                OpenContactForm(contactForm);
            }
            catch (Exception ex)
            {                
                General.ShowMessage($"btnEdit_Click_1 - {ex.Message}", General.ShowMessageType.Error);
            }            
        }

        private void btnRemove_Click_1(object sender, EventArgs e)
        {
            if (!EligibleModifyDeleteAction())
                return;

            try
            {
                int currentCellRowIndex = dgrMain.CurrentCell.RowIndex;
                var contactName = dgrMain.Rows[currentCellRowIndex].Cells[1].Value.ToString();
                var message = $"Are you sure you want to remove {contactName} from your contact list?\r\n\r\nplease click YES to proceed and NO to return to form.";
                var confirmResult = General.ShowMessage(message, General.ShowMessageType.Confirmation);

                if (confirmResult == DialogResult.Yes)
                {
                    int contactId = Convert.ToInt32(dgrMain.Rows[currentCellRowIndex].Cells[0].Value);
                    _phoneBook.DeleteContact(contactId);
                    _dataSync.SavePhoneBook(_phoneBook);

                    _contactView = new List<Contact>(_phoneBook.FindContact(txtSearch.Text));
                    RefreshDatagrid(_contactView);
                }
            }
            catch (Exception ex)
            {
                General.ShowMessage($"btnRemove_Click_1 - {ex.Message}", General.ShowMessageType.Error);                
            }            
        }

        private void dgrMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click_1(null, null);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _contactView = new List<Contact>(_phoneBook.FindContact(txtSearch.Text));
                RefreshDatagrid(_contactView);
            }
            catch(Exception ex)
            {                
                General.ShowMessage($"btnSearch_Click - {ex.Message}", General.ShowMessageType.Error);
            }            
        }

        private void dgrMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                _sortIndex = e.ColumnIndex;
                RefreshDatagrid(_contactView);
            }
            catch(Exception ex)
            {
                General.ShowMessage($"dgrMain_ColumnHeaderMouseClick - {ex.Message}", General.ShowMessageType.Error);
            }            
        }

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                _contactView = new List<Contact>(_phoneBook.FindContact(txtSearch.Text));
                RefreshDatagrid(_contactView);
            }
            catch(Exception ex)
            {
                General.ShowMessage($"btnResetSearch_Click - {ex.Message}", General.ShowMessageType.Error);
            }            
        }
    }
}
