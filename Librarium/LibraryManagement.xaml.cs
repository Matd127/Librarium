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
    /// Logika interakcji dla klasy LibraryManagement.xaml
    /// </summary>
    public partial class LibraryManagement : Window
    {
        public LibraryManagement()
        {
            InitializeComponent();
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            Books b = new Books();
            Close();
            b.Show();
        }

        private void Readers_Click(object sender, RoutedEventArgs e)
        {
            Readers r = new Readers();
            Close();
            r.Show();
        }

        private void Rentals_Click(object sender, RoutedEventArgs e)
        {
            Rentals r = new Rentals();
            Close();
            r.Show();
        }

        private void Fines_Click(object sender, RoutedEventArgs e)
        {
            Fines f = new Fines();
            Close();
            f.Show();
        }
    }
}
