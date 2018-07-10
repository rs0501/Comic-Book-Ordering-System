using ComicBookDataObjects;
using ComicBookLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for frmCustomerAccnt.xaml
    /// </summary>
    public partial class frmCustomerAccnt : Window
    {
        Customer _customer = null;
        CustomerManager _custMgr = null;
        public frmCustomerAccnt(CustomerManager customerManager)
        {
            InitializeComponent();

            _custMgr = customerManager;
        }

        public frmCustomerAccnt(CustomerManager customerManager, Customer customer)
        {
            InitializeComponent();

            _customer = customer;
            _custMgr = customerManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //if (_customer.Equals(null))
            //{
            //    lblWindowTitle.Content = "Create an Account";
            //}
            //else
            //{
            //    lblWindowTitle.Content = "Viewing account for " + _customer.FirstName + " " + _customer.LastName;
            //}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var phoneNum = txtPhone.Text;
            var zip = txtZipCode.Text;
            var email = txtCustEmail.Text;
            var passwordHash = txtPassword.Password;
            var confirmPassword = txtConfirmPassword.Password;

            if (firstName == "")
            {
                MessageBox.Show("Must supply a first name.");
                txtFirstName.Focus();
                return;
            }
            if (lastName == "")
            {
                MessageBox.Show("Must supply a last name.");
                txtLastName.Focus();
                return;
            }
            //if (IsPhoneNumber(phoneNum).Equals(false))
            //{
            //    MessageBox.Show("Must supply a valid phone number with area code.");
            //    txtPhone.Clear();
            //    txtPhone.Focus();
            //    return;
            //}
            if (phoneNum == "" || phoneNum.Length > 18 || phoneNum.Length < 9)
            {
                MessageBox.Show("Must supply a valid phone number with area code.");
                txtPhone.Clear();
                txtPhone.Focus();
                return;
            }
            if (zip == "" || zip.Length < 5 || zip.Length > 5)
            {
                MessageBox.Show("Must supply your 5-digit zip code.");
                txtZipCode.Focus();
                return;
            }
            if (email == "" || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Must supply a valid email address.");
                txtCustEmail.Clear();
                txtCustEmail.Focus();
                return;
            }
            if (passwordHash == "" || passwordHash.Length < 7)
            {
                MessageBox.Show("Must supply a password that is at least 7 characters in length.");
                txtLastName.Focus();
                return;
            }
            if (passwordHash != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            try
            {
                _custMgr = new CustomerManager();
                if (_custMgr.CreateCustomerAccount(firstName, lastName, phoneNum, zip, email))
                {
                    MessageBox.Show("Welcome! Log in to continue.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to submit new customer account. Please check fields and try again");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Please try again.");
                txtFirstName.Clear();
                txtLastName.Clear();
                txtPhone.Clear();
                txtZipCode.Clear();
                txtCustEmail.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var customer = _customer;
            
            // New customer data modified by employee
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var phoneNum = txtPhone.Text;
            var zip = txtZipCode.Text;
            var email = txtCustEmail.Text;
            var passwordHash = txtPassword.Password;
            var confirmPassword = txtConfirmPassword.Password;
            var active = bxCustActiveStatus.IsChecked.Equals(true);

            if (firstName == "")
            {
                MessageBox.Show("Must supply a first name.");
                txtFirstName.Focus();
                return;
            }
            if (lastName == "")
            {
                MessageBox.Show("Must supply a last name.");
                txtLastName.Focus();
                return;
            }
            if (phoneNum == "" || phoneNum.Length > 18 || phoneNum.Length < 9)
            {
                MessageBox.Show("Must supply a valid phone number with area code.");
                txtPhone.Clear();
                txtPhone.Focus();
                return;
            }
            if (zip == "" || zip.Length < 5 || zip.Length > 5)
            {
                MessageBox.Show("Must supply your 5-digit zip code.");
                txtZipCode.Focus();
                return;
            }
            if (email == "" || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Must supply a valid email address.");
                txtCustEmail.Clear();
                txtCustEmail.Focus();
                return;
            }
            if (passwordHash == "" || passwordHash.Length < 7)
            {
                MessageBox.Show("Must supply a password that is at least 7 characters in length.");
                txtLastName.Focus();
                return;
            }
            if (passwordHash != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            try
            {
                _custMgr.UpdateCustomer(customer, firstName, lastName, phoneNum, zip, email, active);
                MessageBox.Show("Customer account updated.");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "Error");
            }
        }

        private void btnViewPull_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //List<Comic> empViewPullList = new List<Comic>();
            //var customer = _customer;
            //var comicMgr = new ComicManager();
            //mainWindow.LoadCustomerPullList(customer);
            //empViewPull = mainWindow._custPullList;
            //mainWindow.dgEmpPullList.ItemsSource = empViewPull;
            //mainWindow.dgEmpPullList.Focus();
            //try
            //{
            //    empViewPullList = comicMgr.PullListForCustomer(customer.CustomerID);
            //    mainWindow.dgEmpPullList.ItemsSource = empViewPullList;
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Problem viewing customer's pull list" + ex.Message);
            //}
            Close();
        }
    }
}
