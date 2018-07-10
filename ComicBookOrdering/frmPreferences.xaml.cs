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
    /// Interaction logic for frmPreferences.xaml
    /// </summary>
    public partial class frmPreferences : Window
    {
        MainWindow mainWindow = new MainWindow();
        public frmPreferences()
        {
            InitializeComponent();
        }

        private void btnSettingsChange_Click(object sender, RoutedEventArgs e)
        {
            //mainWindow
        }
    }
}
