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
    /// Interaction logic for frmComicDetail.xaml
    /// </summary>
    public partial class frmComicDetail : Window
    {
        Comic _comic = null;
        ComicManager _comicManager = null;
        Customer _customer = null;
        Employee _employee = null;
        //CustomerManager _custMgr = null;
        public frmComicDetail(Comic comic, ComicManager comicManager, Customer customer)
        {
            InitializeComponent();

            _comic = comic;
            _comicManager = comicManager;
            _customer = customer;
        }

        public frmComicDetail(Comic comic, ComicManager comicManager, Employee employee)
        {
            InitializeComponent();

            _comic = comic;
            _comicManager = comicManager;
            _employee = employee;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtComicTitle.Text = _comic.Title;
            txtComicIssue.Text = _comic.Issue.ToString();
            txtComicAuthors.Text = _comic.Authors;
            txtComicPublisher.Text = _comic.Publisher;
            txtComicRating.Text = _comic.Rating;
            txtComicType.Text = _comic.ComicType;
            txtComicDescription.Text = _comic.Description;
            txtPrice.Text = _comic.Price.ToString();
            ckbxStatus.IsChecked = _comic.Status;
        }

        private void btnAddComicToPull_Click(object sender, RoutedEventArgs e)
        {
            var _customerID = _customer.CustomerID;
            var _comicID = _comic.ComicID;
            var _comicManager = new ComicManager();
            var mainWindow = new MainWindow();

            try
            {
                _comicManager.AddComicToPullList(_customerID, _comicID);
                MessageBox.Show("Comic added to your pull list.");
                //mainWindow.LoadCustomerPullList(_customer);
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("A problem occurred adding comic to pull list" + ex.StackTrace);
            }
        }

        private void btnAddComictoOrder_Click(object sender, RoutedEventArgs e)
        {
            var _customerID = _customer.CustomerID;
            var _comicID = _comic.ComicID;
            var _date = DateTime.Now;
            var _comicManager = new ComicManager();
            var mainWindow = new MainWindow();

            try
            {
                _comicManager.AddComicToOrderForm(_customerID, (int)_comicID, _date);
                MessageBox.Show("Comic added to your order.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add comic to order" + ex.Message + ex.StackTrace);
            }
        }

        private void btnRemoveFromPull_Click(object sender, RoutedEventArgs e)
        {
            var _customerID = _customer.CustomerID;
            var _comicID = _comic.ComicID;
            var _comicManager = new ComicManager();
            var mainWindow = new MainWindow();

            try
            {
                _comicManager.RemoveComicFromPullList(_comicID, _customerID);
                MessageBox.Show("Comic removed from your pull list.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to remove from your pull list" + ex.Message + ex.StackTrace);
            }
        }

        private void btnRemoveFromOrder_Click(object sender, RoutedEventArgs e)
        {
            var _customerID = _customer.CustomerID;
            var _comicID = _comic.ComicID;
            var _comicManager = new ComicManager();

            try
            {
                _comicManager.RemoveComicFromOrderForm((int)_comicID, _customerID);
                MessageBox.Show("Comic removed from your order.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to remove from your order" + ex.Message + ex.StackTrace);
            }
        }
    }
}
