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
    /// Interaction logic for frmSearchComics.xaml
    /// </summary>
    public partial class frmSearchComics : Window
    {
        private Comic _comics = null;
        public List<Comic> comicsSearched = new List<Comic>();
        public List<Comic> comicsByTitle = new List<Comic>();
        public List<Comic> comicsByAuthors = new List<Comic>();
        //private ComicManager comicMgr = new ComicManager();
        MainWindow mainWindow = new MainWindow();
        public frmSearchComics()
        {
            InitializeComponent();
        }

        public void btnSearchComicTitle_Click(object sender, RoutedEventArgs e)
        {
            var title = txtSearchTitle.Text;
            var comicMgr = new ComicManager();

            if (_comics == null)
            {

                try
                {
                    comicsSearched = comicMgr.ComicsByTitle(title);
                    //try
                    //{
                    //    mainWindow.dgComics.ItemsSource = comicsSearched;
                    //}
                    //catch (Exception ex)
                    //{

                    //    MessageBox.Show(ex.Message + ex.StackTrace, "Error adding to datagrid here:");
                    //}

                    MessageBox.Show("Comic found!");
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "ERROR:");
                    txtSearchTitle.IsEnabled = true;
                    txtSearchTitle.Clear();
                }
            }
            else
            {
                txtSearchTitle.IsEnabled = true;
                txtSearchTitle.Clear();
                MessageBox.Show("Please enter a comic title.");
            }
        }

        private void btnSearchComicAuthor_Click(object sender, RoutedEventArgs e)
        {
            var authors = txtSearchAuthor.Text;
            var comicMgr = new ComicManager();

            if (_comics == null)
            {

                try
                {
                    comicsSearched = comicMgr.ComicsByAuthors(authors);
                    //try
                    //{
                    //    mainWindow.dgComics.ItemsSource = comicsSearched;
                    //}
                    //catch (Exception ex)
                    //{

                    //    MessageBox.Show(ex.Message + ex.StackTrace, "Error adding to datagrid here:");
                    //}

                    MessageBox.Show("Comic found!");
                    Close();
                }
                catch (Exception ex)
                {

                    throw new ApplicationException(ex.Message + "Comic not found");
                    txtSearchAuthor.IsEnabled = true;
                    txtSearchAuthor.Clear();
                }
            }
            else
            {
                txtSearchTitle.IsEnabled = true;
                txtSearchTitle.Clear();
                MessageBox.Show("Please enter a comic title.");
            }
        }

        private void btnSearchPublisher_Click(object sender, RoutedEventArgs e)
        {
            var publisher = txtSearchPublisher.Text;
            var comicMgr = new ComicManager();

            if (_comics == null)
            {

                try
                {
                    comicsSearched = comicMgr.ComicsByPublisher(publisher);
                    //try
                    //{
                    //    mainWindow.dgComics.ItemsSource = comicsSearched;
                    //}
                    //catch (Exception ex)
                    //{

                    //    MessageBox.Show(ex.Message + ex.StackTrace, "Error adding to datagrid here:");
                    //}

                    MessageBox.Show("Comic found!");
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "ERROR:");
                    txtSearchAuthor.IsEnabled = true;
                    txtSearchAuthor.Clear();
                }
            }
            else
            {
                txtSearchTitle.IsEnabled = true;
                txtSearchTitle.Clear();
                MessageBox.Show("Please enter a comic title.");
            }
        }
    }
}
