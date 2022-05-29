using Librarium.Models;
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

namespace Librarium
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LibraryDBEntities db = new LibraryDBEntities();
        public MainWindow()
        {
            InitializeComponent();          
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            var ifExistUser = (from u in db.users
                        where u.login == Login.Text
                        && u.password == Password.Password.ToString()
                        select u).Count();

            if(ifExistUser > 0)
            {
                LibraryManagement m = new LibraryManagement();
                Close();
                m.Show();
            }

            if (ifExistUser == 0)
            {
                MessageBox.Show("Bledny login lub haslo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
