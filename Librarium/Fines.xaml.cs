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

namespace Librarium
{
    /// <summary>
    /// Logika interakcji dla klasy Fines.xaml
    /// </summary>
    public partial class Fines : Window
    {
        public LibraryDBEntities db = new LibraryDBEntities();
        public Fines()
        {
            InitializeComponent();
            LoadReadersGrid();
            LoadFinesGrid();
        }

        public void LoadReadersGrid()
        {
            var readersList = from r in db.readers
                              select r;

            readersGrid.ItemsSource = readersList.ToList();
        }

        public void LoadFinesGrid()
        {
            var finesList = from f in db.fines
                              select f;

            finesGrid.ItemsSource = finesList.ToList();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            LibraryManagement m = new LibraryManagement();
            Close();
            m.Show();
        }
    }
}
