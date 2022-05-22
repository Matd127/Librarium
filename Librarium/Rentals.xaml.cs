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
    /// Logika interakcji dla klasy Rentals.xaml
    /// </summary>
    public partial class Rentals : Window
    {
        public LibraryDBEntities db = new LibraryDBEntities();
        public Rentals()
        {
            InitializeComponent();
            LoadReadersGrid();
            LoadActualBooksGrid();
            LoadRentalsGrid();
        }

        public void LoadReadersGrid()
        {
            var readersList = from r in db.readers
                              select r;

            ReadersGrid.ItemsSource = readersList.ToList();
        }

        public void LoadActualBooksGrid()
        {
            var booksList = from b in db.books                        
                            select b;

            ActualBooksGrid.ItemsSource = booksList.ToList();
        }

        public void LoadRentalsGrid()
        {
            var rentalsList = from r in db.rentals
                              join rd in db.readers on r.readerId equals rd.readerId
                              join b in db.books on r.bookId equals b.bookId
                              select new
                              {
                                  r.rentalId,
                                  rd.readerId,
                                  b.bookId,
                                  b.title,
                                  rd.name,
                                  rd.surname,
                                  r.date_of_rent,
                                  r.date_of_return
                              };

            RentalsGrid.ItemsSource = rentalsList.ToList();
        }

        public void ClearAll()
        {
            bookId_txt.Clear();
            readerId_txt.Clear();
            rentalId_txt.Clear();
            dateOfRent_txt.Clear();
            dateOfReturn_txt.Clear();
            readerName_txt.Clear();
            readerSurname_txt.Clear();
            bookTitle_txt.Clear();
        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            LibraryManagement m = new LibraryManagement();
            Close();
            m.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            rentals rentalObject = new rentals()
            {
                readerId = int.Parse(readerId_txt.Text),
                bookId = int.Parse(bookId_txt.Text),
                date_of_rent = DateTime.Parse(dateOfRent_txt.Text),
                date_of_return = DateTime.Parse(dateOfReturn_txt.Text)
            };
            db.rentals.Add(rentalObject);
            db.SaveChanges();
            LoadRentalsGrid();
            MessageBox.Show("Książka wypożyczona!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(rentalId_txt.Text);
            var selectReader = from r in db.rentals
                               where r.rentalId == id
                               select r;

            rentals obj = selectReader.SingleOrDefault();
            if (obj != null)
            {
                obj.rentalId = int.Parse(rentalId_txt.Text);
                obj.bookId = int.Parse(bookId_txt.Text);
                obj.readerId = int.Parse(bookId_txt.Text);
                obj.date_of_rent = DateTime.Parse(dateOfRent_txt.Text);
                obj.date_of_return = DateTime.Parse(dateOfReturn_txt.Text);
            }
            db.SaveChanges();
            ClearAll();
            MessageBox.Show("Pomyślnie zaaktualizowano!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadRentalsGrid();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(rentalId_txt.Text);
            var selectReader = from r in db.rentals
                               where r.rentalId == id
                               select r;

            rentals obj = selectReader.SingleOrDefault();
            if (obj != null)
            {
                db.rentals.Remove(obj);
                db.SaveChanges();
            }
            ClearAll();
            MessageBox.Show("Wypożyczenie usunięte!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadRentalsGrid();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void ReadersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            readers r = ReadersGrid.SelectedItem as readers; 
            if (r != null)
            {
                readerId_txt.Text = r.readerId.ToString();
                readerName_txt.Text = r.name.ToString();
                readerSurname_txt.Text = r.surname.ToString();
            }
        }

        private void ActualBooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            books b = ActualBooksGrid.SelectedItem as books;

            if (b != null)
            {
               bookId_txt.Text = b.bookId.ToString();
               bookTitle_txt.Text = b.title.ToString();
               
            }
        }

        private void RentalsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = ((DataGrid)sender).SelectedItem;
            if (selectedItem != null)
            {
                Type type = selectedItem.GetType();
                rentalId_txt.Text = ((int)type.GetProperty("rentalId").GetValue(selectedItem, null)).ToString();
                readerId_txt.Text = ((int)type.GetProperty("readerId").GetValue(selectedItem, null)).ToString();
                bookId_txt.Text = ((int)type.GetProperty("bookId").GetValue(selectedItem, null)).ToString();
                bookTitle_txt.Text = (string)type.GetProperty("title").GetValue(selectedItem, null);
                readerName_txt.Text = (string)type.GetProperty("name").GetValue(selectedItem, null);
                readerSurname_txt.Text = (string)type.GetProperty("surname").GetValue(selectedItem, null);
                dateOfRent_txt.Text = ((DateTime)type.GetProperty("date_of_rent").GetValue(selectedItem, null)).ToString();
                dateOfReturn_txt.Text = ((DateTime)type.GetProperty("date_of_return").GetValue(selectedItem, null)).ToString();

            }
        }
    }
}
