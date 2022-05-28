using Librarium.Models;
using System;
using System.Collections;
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

        public void ClearAll()
        {
            fineId_txt.Clear();
            readerId_txt.Clear();
            name_txt.Clear();
            surname_txt.Clear();
            typeOfFine_txt.Clear();
            cost_txt.Clear();
            isPaid.IsChecked = false;
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
                            join r in db.readers on f.readerId equals r.readerId
                            select new
                            {
                                f.fineId,
                                r.readerId,
                                r.name,
                                r.surname,
                                f.type_of_fine,
                                f.cost,
                                f.isPaid_
                            };

            finesGrid.ItemsSource = finesList.ToList();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            readers r = readersGrid.SelectedItem as readers;
            if (r != null)
            {
                name_txt.Text = r.name.ToString();
                surname_txt.Text = r.surname.ToString();
                readerId_txt.Text = r.readerId.ToString();
            }
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = ((DataGrid)sender).SelectedItem;
            if (selectedItem != null)
            {
                Type type = selectedItem.GetType();
                readerId_txt.Text = ((int)type.GetProperty("readerId").GetValue(selectedItem, null)).ToString();
                fineId_txt.Text = ((int)type.GetProperty("fineId").GetValue(selectedItem, null)).ToString();
                name_txt.Text = (string)type.GetProperty("name").GetValue(selectedItem, null);
                typeOfFine_txt.Text = (string)type.GetProperty("type_of_fine").GetValue(selectedItem, null);
                surname_txt.Text = (string)type.GetProperty("surname").GetValue(selectedItem, null);
                cost_txt.Text = ((int)type.GetProperty("cost").GetValue(selectedItem, null)).ToString();
                bool paid = ((bool)type.GetProperty("isPaid_").GetValue(selectedItem, null));

                if (paid == true) isPaid.IsChecked = true;
                else isPaid.IsChecked = false;
            }

        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            LibraryManagement m = new LibraryManagement();
            Close();
            m.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;

            if (isPaid.IsChecked == true) status = true;

            fines finesObject = new fines()
            {
                readerId = int.Parse(readerId_txt.Text),
                type_of_fine = typeOfFine_txt.Text,
                cost = int.Parse(cost_txt.Text),
                isPaid_ = status
            
            };
            db.fines.Add(finesObject);
            db.SaveChanges();
            LoadReadersGrid();
            LoadFinesGrid();
            MessageBox.Show("Nałożono karę!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;

            if (isPaid.IsChecked == true) status = true;

            int id = int.Parse(fineId_txt.Text);
            var selectFine = from f in db.fines
                             where f.fineId == id
                             select f;

            fines obj = selectFine.SingleOrDefault();
            if (obj != null)
            {
                obj.readerId = int.Parse(readerId_txt.Text);
                obj.fineId =  int.Parse(fineId_txt.Text);
                obj.type_of_fine = typeOfFine_txt.Text;
                obj.cost = int.Parse(cost_txt.Text);
                obj.isPaid_ = status;


            }
            db.SaveChanges();
            ClearAll();
            MessageBox.Show("Pomyślnie zaaktualizowano!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadReadersGrid();
            LoadFinesGrid();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(fineId_txt.Text);
            var selectFine = from f in db.fines
                             where f.fineId == id
                             select f;

            fines obj = selectFine.SingleOrDefault();
            if (obj != null)
            {
                db.fines.Remove(obj);
                db.SaveChanges();
            }
            MessageBox.Show("Kara została usunięta!", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearAll();
            LoadReadersGrid();
            LoadFinesGrid();

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }
    }
}
