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
    /// Interaction logic for frmComicAddEdit.xaml
    /// </summary>
    public partial class frmComicAddEdit : Window
    {
        Comic _comic = null;
        ComicManager _comicManager = new ComicManager();
        bool _comicStatus;
        public frmComicAddEdit(ComicManager comicManager)
        {
            InitializeComponent();
            _comicManager = comicManager;
        }

        public frmComicAddEdit(ComicManager comicManager, Comic comic)
        {
            InitializeComponent();
            _comicManager = comicManager;
            _comic = comic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load Comic Type field list
            cboComicType.Items.Add("Individual book");
            cboComicType.Items.Add("Bound trade");
            cboComicType.Items.Add("Graphic novel");
            cboComicType.Items.Add("Manga");
            // Distributor ID
            cboDistributor.Items.Add(1000);
            // Issue numbers
            for (int i = 0; i < 1001; i++)
            {
                cboIssue.Items.Add(i);
            }

            // Quantity of books entered
            for (int i = 0; i < 50; i++)
            {
                cboQuantity.Items.Add(i);
            }

            if(_comic != null)
            {
                // Load the fields with the selected comic's data
                txtTitle.Text = _comic.Title;
                cboIssue.SelectedItem = _comic.Issue;
                txtAuthors.Text = _comic.Authors;
                txtPublisher.Text = _comic.Publisher;
                cboComicType.SelectedItem = _comic.ComicType;
                cboDistributor.SelectedItem = _comic.DistributorID;
                txtRating.Text = _comic.Rating;
                txtDescription.Text = _comic.Description;
                cboQuantity.SelectedItem = _comic.Quantity;
                txtPrice.Text = _comic.Price.ToString();
                ckbxComicStatus.IsChecked = _comic.Status;

                // Load the Edit button, hide Add button
                btnEditComic.Visibility = Visibility.Visible;
                btnAddComic.Visibility = Visibility.Hidden;
            }
        }


        private void btnAddComic_Click(object sender, RoutedEventArgs e)
        {
            decimal priceField = 0;

            var title = txtTitle.Text;
            var issue = cboIssue.SelectedItem;
            var authors = txtAuthors.Text;
            var publisher = txtPublisher.Text;
            var comicType = cboComicType.Text;
            var distributorID = cboDistributor.SelectedItem;
            var rating = txtRating.Text;
            var description = txtDescription.Text;
            var quantity = cboQuantity.SelectedItem;
            var status = _comicStatus;

            // Validing inputs are present for all fields
            if (title == "")
            {
                MessageBox.Show("Must enter a title.");
                txtTitle.Focus();
                return;
            }
            if (issue == null)
            {
                MessageBox.Show("Must choose an issue number. Enter '0' for \ngraphic novels or bound trades without one.");
                txtTitle.Focus();
                return;
            }
            if (authors == "")
            {
                MessageBox.Show("Must list authors and illustrators.");
                txtAuthors.Focus();
                return;
            }
            if (publisher == "")
            {
                MessageBox.Show("Must list the book's publisher. \nE.g. Marvel, Image, Dark Horse, etc.");
                txtPublisher.Focus();
                return;
            }
            if (comicType == "")
            {
                MessageBox.Show("Must select comic type.");
                cboComicType.Focus();
                return;
            }
            if (distributorID == null)
            {
                MessageBox.Show("Must select distributor ID.");
                cboDistributor.Focus();
                return;
            }
            if (rating == "")
            {
                MessageBox.Show("Must enter a rating.");
                txtRating.Focus();
                return;
            }
            if (description == "")
            {
                MessageBox.Show("Must enter a description.");
                txtRating.Focus();
                return;
            }
            if (quantity == null)
            {
                MessageBox.Show("Must enter a quantity.");
                cboQuantity.Focus();
                return;
            }
            // Validates price input, converts to decimal data type
            try
            {
                priceField = Convert.ToDecimal(txtPrice.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Must enter a valid price.");
                txtPrice.Clear();
                txtPrice.Focus();
                return;
            }
            var price = priceField;
            if (_comic == null)
            {
                try
                {
                    _comicManager.AddComicToInventory(title, (int)issue, authors, publisher, comicType, (int)distributorID,
                                                        rating, description, (int)quantity, price, status);
                    MessageBox.Show("Comic added.");
                    Close();
                    //txtTitle.Clear();
                    //txtTitle.Focus();
                    //cboIssue.Equals(null);
                    //txtAuthors.Clear();
                    //txtPublisher.Clear();
                    //cboComicType.Equals(null);
                    //cboDistributor.Equals(null);
                    //txtRating.Clear();
                    //txtDescription.Clear();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ex.StackTrace, "Error");
                }

            }
            else
            {
                MessageBox.Show("Comic not added.");
            }

        }

        private void ckbxComicStatus_Checked(object sender, RoutedEventArgs e)
        {
            _comicStatus = false;

            if (ckbxComicStatus.IsChecked == true)
            {
                _comicStatus = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditComic_Click(object sender, RoutedEventArgs e)
        {
            decimal priceField = 0;
            var comic = _comic;
            var title = txtTitle.Text;
            var issue = cboIssue.SelectedItem;
            var authors = txtAuthors.Text;
            var publisher = txtPublisher.Text;
            var comicType = cboComicType.Text;
            var distributorID = cboDistributor.SelectedItem;
            var rating = txtRating.Text;
            var description = txtDescription.Text;
            var quantity = cboQuantity.SelectedItem;
            var status = _comicStatus;

            // Validing inputs are present for all fields
            if (title == "")
            {
                MessageBox.Show("Must enter a title.");
                txtTitle.Focus();
                return;
            }
            if (issue == null)
            {
                MessageBox.Show("Must choose an issue number. Enter '0' for \ngraphic novels or bound trades without one.");
                txtTitle.Focus();
                return;
            }
            if (authors == "")
            {
                MessageBox.Show("Must list authors and illustrators.");
                txtAuthors.Focus();
                return;
            }
            if (publisher == "")
            {
                MessageBox.Show("Must list the book's publisher. \nE.g. Marvel, Image, Dark Horse, etc.");
                txtPublisher.Focus();
                return;
            }
            if (comicType == "")
            {
                MessageBox.Show("Must select comic type.");
                cboComicType.Focus();
                return;
            }
            if (distributorID == null)
            {
                MessageBox.Show("Must select distributor ID.");
                cboDistributor.Focus();
                return;
            }
            if (rating == "")
            {
                MessageBox.Show("Must enter a rating.");
                txtRating.Focus();
                return;
            }
            if (description == "")
            {
                MessageBox.Show("Must enter a description.");
                txtRating.Focus();
                return;
            }
            if (quantity == null)
            {
                MessageBox.Show("Must enter a quantity.");
                cboQuantity.Focus();
                return;
            }
            // Validates price input, converts to decimal data type
            try
            {
                priceField = Convert.ToDecimal(txtPrice.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Must enter a valid price.");
                txtPrice.Clear();
                txtPrice.Focus();
                return;
            }
            var price = priceField;
            
            try
            {
                _comicManager.UpdateComicInfo(comic, title, (int)issue, authors, publisher, comicType, (int)distributorID, rating, description, (int)quantity, price, status);
                MessageBox.Show("Comic updated.");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace, "Error");
            }

        }
    }
}
