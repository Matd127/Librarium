using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logika interakcji dla klasy Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        public LibraryDBEntities db = new LibraryDBEntities();
        public Books()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void LoadGrid()
        {
            var booksList = from b in db.books
                            select b;

            datagrid.ItemsSource = booksList.ToList();
        }

        public void ClearAll()
        {
            title_txt.Clear();
            author_txt.Clear();
            no_of_pages_txt.Clear();
            year_of_publication_txt.Clear();
            search_txt.Clear();
        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            LibraryManagement m = new LibraryManagement();
            Close();
            m.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            books bookObject = new books()
            {
                title = title_txt.Text,
                author = author_txt.Text,
                no_of_pages = short.Parse(no_of_pages_txt.Text),
                year_of_publication = short.Parse(year_of_publication_txt.Text)
            };
            db.books.Add(bookObject);
            db.SaveChanges();
            LoadGrid();
            MessageBox.Show("Pomyślnie dodano nową książkę!", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(search_txt.Text);
            var selectedBook = from b in db.books
                             where b.bookId == id
                             select b;

            books obj = selectedBook.SingleOrDefault();

            if(obj!= null) {
                obj.title = title_txt.Text;
                obj.author = author_txt.Text;
                obj.no_of_pages = short.Parse(no_of_pages_txt.Text);
                obj.year_of_publication = short.Parse(year_of_publication_txt.Text);
            }

            db.SaveChanges();
            LoadGrid();
            MessageBox.Show("Pomyślnie zaaktualizowano!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(search_txt.Text);
            var selectedBook = from b in db.books
                             where b.bookId == id
                             select b;

            books obj = selectedBook.SingleOrDefault();
            if(obj != null)
            {
                db.books.Remove(obj);
                db.SaveChanges();
            }
            LoadGrid();
            MessageBox.Show("Książka została usunięta!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearAll();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            books b = datagrid.SelectedItem as books;

            if(b != null)
            {
                title_txt.Text = b.title.ToString();
                author_txt.Text = b.author;
                no_of_pages_txt.Text = (b.no_of_pages).ToString();
                year_of_publication_txt.Text = (b.year_of_publication).ToString();
                search_txt.Text = (b.bookId).ToString();
            }
        }
            
    }
            
          
}
