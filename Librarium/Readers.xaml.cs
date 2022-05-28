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
using System.Windows.Shapes;

namespace Librarium
{
    /// <summary>
    /// Logika interakcji dla klasy Readers.xaml
    /// </summary>
    public partial class Readers : Window
    {
        public LibraryDBEntities db = new LibraryDBEntities();
        public Readers()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LibraryManagement m = new LibraryManagement();
            Close();
            m.Show();
        }
        public void LoadGrid()
        {
            var readersList = from r in db.readers
                              select r;

            datagrid.ItemsSource = readersList.ToList();
        }

        public void ClearAll()
        {
            name_txt.Clear();
            surname_txt.Clear();
            street_txt.Clear();
            houseNo_txt.Clear();
            postal_code_txt.Clear();
            city_txt.Clear();
            voivodeship_txt.Clear();
            phone_number_txt.Clear();
            email_txt.Clear();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                readers readerObject = new readers()
                {
                    name = name_txt.Text,
                    surname = surname_txt.Text,
                    street = street_txt.Text,
                    house_no = houseNo_txt.Text,
                    postal_code = postal_code_txt.Text,
                    city = city_txt.Text,
                    voivodeship = voivodeship_txt.Text,
                    phone_number = phone_number_txt.Text,
                    email = email_txt.Text
                };

                db.readers.Add(readerObject);
                db.SaveChanges();
                LoadGrid();
                MessageBox.Show("Pomyślnie dodano nowego czytelnika!", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Dane są nieprawidłowe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(search_txt.Text);
            var selectedReader = from r in db.readers
                                 where r.readerId == id
                                 select r;

            readers obj = selectedReader.SingleOrDefault();

            if(obj != null)
            {
                obj.name = name_txt.Text;
                obj.surname = surname_txt.Text;
                obj.street = street_txt.Text;
                obj.house_no = houseNo_txt.Text;
                obj.postal_code = postal_code_txt.Text;
                obj.city = city_txt.Text;
                obj.voivodeship = voivodeship_txt.Text;
                obj.phone_number = phone_number_txt.Text;
                obj.email = email_txt.Text;
            }
            db.SaveChanges();
            LoadGrid();
            MessageBox.Show("Pomyślnie zaaktualizowano!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(search_txt.Text);
            var selectedReader = from r in db.readers
                             where r.readerId == id
                             select r;

            readers obj = selectedReader.SingleOrDefault();
            if (obj != null)
            {
                db.readers.Remove(obj);
                db.SaveChanges();
            }
            LoadGrid();
            MessageBox.Show("Czytelnik został usunięty!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearAll();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            readers r = datagrid.SelectedItem as readers;
            if (r != null)
            {
                name_txt.Text = r.name.ToString();
                surname_txt.Text = r.surname.ToString();
                street_txt.Text = r.street.ToString();
                houseNo_txt.Text = r.house_no.ToString();
                postal_code_txt.Text = r.postal_code.ToString();
                city_txt.Text = r.name.ToString();
                voivodeship_txt.Text = r.name.ToString();
                phone_number_txt.Text = r.name.ToString();
                email_txt.Text = r.name.ToString();
                search_txt.Text = r.readerId.ToString();
            }
        }
    }
}
