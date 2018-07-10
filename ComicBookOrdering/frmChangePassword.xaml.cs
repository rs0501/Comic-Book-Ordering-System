using ComicBookDataObjects;
using ComicBookLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComicBookOrdering
{
    /// <summary>
    /// Interaction logic for frmChangePassword.xaml
    /// </summary>
    public partial class frmChangePassword : Window
    {
        private Customer _customer = null;
        private Employee _employee = null;
        public frmChangePassword(Customer customer)
        {
            // Customer change password mode
            InitializeComponent();

            _customer = customer;
        }

        public frmChangePassword(Employee employee)
        {
            // Employee change password mode
            InitializeComponent();

            _employee = employee;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var oldPassword = bxCurrentPassword.Password;
            var newPassword = bxNewPassword.Password;
            var confirmNewPword = bxConfirmNewPassword.Password;
            var custID = _customer.CustomerID;
            var custMgr = new CustomerManager();

            if (oldPassword == _customer.PasswordHash)
            {
                if (newPassword == confirmNewPword)
                {
                    try
                    {
                        custMgr.UpdateCustomerPassword(custID, oldPassword, newPassword);
                        MessageBox.Show("Password successfully changed.");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR");
                    }
                }
                else
                {
                    MessageBox.Show("New password and confirm new password fields must match.");
                } 
            }
            else
            {
                MessageBox.Show("Current password is incorrect.");
                bxCurrentPassword.Clear();
                bxCurrentPassword.Focus();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
