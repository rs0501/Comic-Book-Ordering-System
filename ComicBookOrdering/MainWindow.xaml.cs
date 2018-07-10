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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComicBookOrdering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Customer _customer = null;
        private Employee _employee = null;
        private Comic _comic = null;
        private List<Comic> _comics;
        private List<Comic> _comicsSearched;
        public List<Comic> _custPullList;
        private List<Comic> _custOrderList;
        private List<Comic> _comicsAvailable;
        private List<Customer> _customers;
        private List<Customer> _customersCurrent;
        private List<Customer> _customersInactive;
        private ComicManager _comicManager = new ComicManager();
        private CustomerManager _custManager = new CustomerManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //btnAddComicWindow.IsEnabled = false;
            hideAllTabs();
            RefreshComicsAvailable();
            _comics = _comicManager.RetrieveAvailableComics(true);
            dgComics.ItemsSource = _comics;
        }

        private void mnuQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mnuPreferences_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshComicsAvailable()
        {
            _comicsAvailable = _comicManager.AvailableComics;
        }

        //private void RefreshComicsAvailable(bool status = true)
        //{
        //    if (status)
        //    {
        //        _comicsAvailable = _comicManager.RetrieveAvailableComics(status);
        //    }
        //}

        public void LoadCustomerPullList(Customer _customer)
        {
            try
            {
                _custPullList = _comicManager.PullListForCustomer(_customer.CustomerID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred" + ex.Message + ex.StackTrace);
            }
        }

        public void LoadCustomerOrderForm(Customer _customer)
        {
            try
            {
                _custOrderList = _comicManager.OrderFormForCustomer(_customer.CustomerID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred" + ex.Message + ex.StackTrace);
            }

        }

        private void LoadCurrentCustomers()
        {
            try
            {
                _customersCurrent = _custManager.CurrentCustomersList();
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred" + ex.Message + ex.StackTrace);
            }
        }

        private void hideAllTabs()
        {
            tabsetMain.Visibility = Visibility.Hidden;
            tabsetEmp.Visibility = Visibility.Hidden;
        }

        private void showCustTabs()
        {
            tabsetMain.Visibility = Visibility.Visible;
        }

        private void showEmpTabs()
        {
            tabsetEmp.Visibility = Visibility.Visible;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            var custMgr = new CustomerManager();
            
            if (_customer == null)
            {
                try
                {
                    _customer = custMgr.AuthenticateLogin(email, password);

                    if (_customer.Active == false)
                    {
                        MessageBox.Show("Your account is not active. Please talk \nwith our helpful staff to proceed further.");
                        txtEmail.Clear();
                        txtPassword.Clear();
                        hideAllTabs();
                        MessageBox.Show("Logged out.");
                        _customer = null;
                        txtEmail.IsEnabled = true;
                        txtEmail.Focus();
                        txtPassword.IsEnabled = true;
                        btnLogin.Content = "Login";
                        btnLogin.IsDefault = true;
                        btnLogin.IsEnabled = true;
                        btnLoginEmp.IsEnabled = true;
                        btnCreateAccount.IsEnabled = true;
                        return;
                    }

                    MessageBox.Show("Logged in!");
                    txtEmail.Clear();
                    txtPassword.Clear();
                    txtEmail.IsEnabled = false;
                    txtPassword.IsEnabled = false;
                    btnLogin.Content = "Log out";
                    btnLogin.IsDefault = false;
                    btnCreateAccount.IsEnabled = false;
                    btnLoginEmp.IsEnabled = false;
                    showCustTabs();

                    // Load the Pull List
                    LoadCustomerPullList(_customer);
                    dgCustPullList.ItemsSource = _custPullList;

                    // Load the Change Customer Info page's fields
                    txtFirstName.Text = _customer.FirstName;
                    txtLastName.Text = _customer.LastName;
                    txtPhone.Text = _customer.PhoneNumber;
                    txtZipCode.Text = _customer.Zip;
                    txtCustEmail.Text = _customer.Email;

                    // Load the Customer's Order Form fields ... and list
                    txtOrderCustName.Text = _customer.FirstName + " " + _customer.LastName;
                    txtOrderCustPhone.Text = _customer.PhoneNumber;
                    txtOrderCustEmail.Text = _customer.Email;
                    LoadCustomerOrderForm(_customer);
                    dgCustOrderFormList.ItemsSource = _custOrderList;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error");
                    txtEmail.IsEnabled = true;
                    txtEmail.Clear();
                    txtEmail.Focus();
                    txtPassword.IsEnabled = true;
                    txtPassword.Clear();
                    btnLogin.Content = "Login";
                    btnLogin.IsDefault = true;

                }
            }
            else
            {
                _customer = null;
                txtEmail.IsEnabled = true;
                txtEmail.Clear();
                txtEmail.Focus();
                txtPassword.IsEnabled = true;
                txtPassword.Clear();
                btnLogin.Content = "Login";
                btnLogin.IsDefault = true;
                btnLogin.IsEnabled = true;
                btnLoginEmp.IsEnabled = true;
                btnCreateAccount.IsEnabled = true;
                txtFirstName.Clear();
                txtLastName.Clear();
                txtPhone.Clear();
                txtZipCode.Clear();
                txtCustEmail.Clear();
                hideAllTabs();
                MessageBox.Show("Logged out.");
            }
        }

        //private void tabComics_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if(_comics==null)
        //    {
        //        try
        //        {
        //            _comics = _comicManager.RetrieveAvailableComics(true);
        //            dgComics.ItemsSource = _comics;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message + ex.StackTrace, "Error:");
        //        }
        //    }
        //    else
        //    {
        //        RefreshComicsAvailable();
        //        dgComics.ItemsSource = _comicsAvailable;
        //        //MessageBox.Show("Say something.");
        //    }
        //}

        private void btnLoginEmp_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Password;
            var empMgr = new EmployeeManager();

            if (_employee == null)
            {
                try
                {
                    _employee = empMgr.AuthenticateEmpLogin(email, password);
                    MessageBox.Show("Logged in!");
                    txtEmail.Clear();
                    txtPassword.Clear();
                    txtEmail.IsEnabled = false;
                    txtPassword.IsEnabled = false;
                    btnAddComicWindow.IsEnabled = true;
                    btnLoginEmp.Content = "Log out";
                    btnLoginEmp.IsDefault = false;
                    btnLogin.IsEnabled = false;
                    btnCreateAccount.IsEnabled = false;
                    showEmpTabs();

                    //Load the current available comics list for inventory page
                    RefreshComicsAvailable();
                    dgComicsEmpView.ItemsSource = _comicsAvailable;

                    //Load the current customers list
                    LoadCurrentCustomers();
                    _customers = _custManager.CurrentCustomers(true);
                    dgCustomersEmpView.ItemsSource = _customers;

                    btnLogin.IsDefault = false;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error");
                    txtEmail.IsEnabled = true;
                    txtEmail.Clear();
                    txtEmail.Focus();
                    txtPassword.IsEnabled = true;
                    txtPassword.Clear();
                    btnLogin.Content = "Login";
                    btnLogin.IsDefault = true;
                }
            }
            else
            {
                _employee = null;
                txtEmail.IsEnabled = true;
                txtEmail.Clear();
                txtEmail.Focus();
                txtPassword.IsEnabled = true;
                txtPassword.Clear();
                btnLogin.Content = "Login";
                btnLogin.IsDefault = true;
                btnLogin.IsEnabled = true;
                btnLoginEmp.IsEnabled = true;
                btnCreateAccount.IsEnabled = true;
                btnLoginEmp.Content = "Employee Login";
                hideAllTabs();
                MessageBox.Show("Logged out.");
            }
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            var createAccnt = new frmCustomerAccnt(_custManager);
            createAccnt.ShowDialog();
        }

        private void btnAddComicWindow_Click(object sender, RoutedEventArgs e)
        {
            var addEditForm = new frmComicAddEdit(_comicManager);
            addEditForm.ShowDialog();
        }

        private void btnChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            var customer = _customer;
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var phoneNum = txtPhone.Text;
            var email = txtCustEmail.Text;
            var zip = txtZipCode.Text;
            var password = _customer.PasswordHash;
            var active = true;
            var custMgr = new CustomerManager();

            try
            {
                custMgr.UpdateCustomer(customer, firstName, lastName, phoneNum, zip, email, active);
                MessageBox.Show("Customer information successfully changed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occurred. Email not updated.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //Cancel changes being made to customer info, reload the fields with original data
            txtFirstName.Text = _customer.FirstName;
            txtLastName.Text = _customer.LastName;
            txtPhone.Text = _customer.PhoneNumber;
            txtZipCode.Text = _customer.Zip;
            txtCustEmail.Text = _customer.Email;
        }

        private void btnSearchComics_Click(object sender, RoutedEventArgs e)
        {
            // Load search for comics window
            var comicSearchWindow = new frmSearchComics();
            comicSearchWindow.ShowDialog();
            try
            {
                _comicsSearched = comicSearchWindow.comicsSearched;

                dgComics.ItemsSource = _comicsSearched;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                comicSearchWindow.Close();
            }
        }

        //private void tabComicsEmpView_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if(dgComicsEmpView.ItemsSource == null)
        //    try
        //    {
        //        RefreshComicsAvailable();
        //        dgComicsEmpView.ItemsSource = _comicsAvailable;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message + ex.StackTrace, "A problem occurred:");
        //    }
        //}

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // Load change password window
            var changePasswordWindow = new frmChangePassword(_customer);
            changePasswordWindow.Show();

            //var oldPassword = txtOldPassword.Password;
            //var newPassword = txtNewPassword.Password;
            //var confirmNewPword = txtConfirmNewPassword.Password;
            //var custID = txtCustomerID.Text;
            //var custMgr = new CustomerManager();

            //if (newPassword == confirmNewPword)
            //{
            //    try
            //    {
            //        custMgr.UpdateCustomerPassword(_customer.CustomerID, oldPassword, newPassword);
            //        MessageBox.Show("Password successfully changed.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "An error occurred. Password not updated.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("New password and confirm new password fields must match.");
            //}
        }

        private void dgComics_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var comic = (Comic)dgComics.SelectedItem;

            var comicDetail = new frmComicDetail(comic, _comicManager, _customer);
            comicDetail.ShowDialog();
        }

        private void dgComicsEmpView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var comic = (Comic)dgComicsEmpView.SelectedItem;

            var comicAddEdit = new frmComicAddEdit(_comicManager, comic);
            comicAddEdit.ShowDialog();

        }

        private void btnSearchComicsEmp_Click(object sender, RoutedEventArgs e)
        {
            var comicSearchWindow = new frmSearchComics();
            comicSearchWindow.ShowDialog();
            try
            {
                _comicsSearched = comicSearchWindow.comicsSearched;
                dgComicsEmpView.ItemsSource = _comicsSearched;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Search results could not be loaded" + ex.Message + ex.StackTrace);
            }
            finally
            {
                comicSearchWindow.Close();
            }
        }

        private void dgCustomersEmpView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var customer = (Customer)dgCustomersEmpView.SelectedItem;
            var custDetail = new frmCustomerAccnt(_custManager, customer);
            custDetail.Show();
            custDetail.txtFirstName.Text = customer.FirstName;
            custDetail.txtLastName.Text = customer.LastName;
            custDetail.txtPhone.Text = customer.PhoneNumber;
            custDetail.txtZipCode.Text = customer.Zip;
            custDetail.txtCustEmail.Text = customer.Email;
            custDetail.btnChange.Visibility = Visibility.Visible;
            custDetail.btnSubmit.Visibility = Visibility.Hidden;
            custDetail.lblActive.Visibility = Visibility.Visible;
            custDetail.txtPassword.Password = customer.PasswordHash;
            custDetail.txtConfirmPassword.Password = customer.PasswordHash;
            custDetail.bxCustActiveStatus.Visibility = Visibility.Visible;
            custDetail.btnViewPull.Visibility = Visibility.Visible;
            custDetail.lblWindowTitle.Content = "Modify Customer Detail";

            try
            {
                LoadCustomerPullList(customer);
                dgEmpPullList.ItemsSource = _custPullList;
                lblEmpViewPullList.Content = customer.FirstName + " " + customer.LastName;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error" + ex.StackTrace);
            }
        }

        private void btnRefreshPull_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerPullList(_customer);
            dgCustPullList.ItemsSource = _custPullList;
        }

        //private void tabOrderForm_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //if (_custOrderList.Equals(null))
        //        //{
        //        //    MessageBox.Show("Add comics to begin an order.");
        //        //}
        //        //else
        //        //{
        //            LoadCustomerOrderForm(_customer);
        //            dgCustOrderFormList.ItemsSource = _custOrderList;        
        //        //}

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("An error occurred. " + ex.Message + ex.StackTrace);
        //    }
        //}

        private void btnSubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order submitted.\nThank you!");
            _custOrderList.Clear();
        }

        private void btnRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            decimal _orderTotal = 0;

            try
            {
                LoadCustomerOrderForm(_customer);
                dgCustOrderFormList.ItemsSource = _custOrderList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occurred getting your order list" + ex.Message + ex.StackTrace);
            }

            try
            {
                foreach (Comic _comic in _custOrderList)
                {
                    _orderTotal += _comic.Price;
                }

                txtOrderTotal.Text = "$" + _orderTotal.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occurred getting your order list total" + ex.Message + ex.StackTrace);
            }
        }

        private void dgCustPullList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var comic = (Comic)dgCustPullList.SelectedItem;

            var comicDetail = new frmComicDetail(comic, _comicManager, _customer);
            comicDetail.btnAddComicToPull.Visibility = Visibility.Hidden;
            comicDetail.btnRemoveFromPull.Visibility = Visibility.Visible;
            comicDetail.ShowDialog();
        }

        private void dgCustOrderFormList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var comic = (Comic)dgCustOrderFormList.SelectedItem;

            var comicDetail = new frmComicDetail(comic, _comicManager, _customer);
            comicDetail.btnAddComictoOrder.Visibility = Visibility.Hidden;
            comicDetail.btnRemoveFromOrder.Visibility = Visibility.Visible;
            comicDetail.ShowDialog();
        }

        private void btnInactiveCust_Click(object sender, RoutedEventArgs e)
        {
            _customersInactive = _custManager.DeactivatedCustomersList();
            dgCustomersEmpView.ItemsSource = _customersInactive;
        }

        //private void tabCustPullList_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        dgEmpPullList.ItemsSource = _custPullList;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("An error occurred" + ex.Message);
        //    }
        //}
    }
}
