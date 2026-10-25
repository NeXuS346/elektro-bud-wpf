using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace projWPF
{
    public class Product
    {
        public string Ean { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Sector { get; set; }
        public string Shelf { get; set; }
        public string Level { get; set; }
    }
    // dokumentacja do automatically implemented properties (get/set): https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties#automatically-implemented-properties

    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        //dokumentacja do observableCollection: https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=net-10.0

        public MainWindow()
        {
            InitializeComponent();
            ProductsGrid.ItemsSource = Products;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ean = TxtEan.Text.Trim();
            string name = TxtName.Text.Trim();
            string category = TxtCategory.Text.Trim();
            string sectorStr = TxtSector.Text.Trim();
            string shelfStr = TxtShelf.Text.Trim();
            string level = TxtLevel.Text.Trim();

            if (ean.Length != 13 || !long.TryParse(ean, out _))
            {
                MessageBox.Show("Wpisz poprawny kod EAN - 13 cyfr", "Walidacja", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (sectorStr == "" || !int.TryParse(sectorStr, out int sector) || sector <= 0)
            {
                MessageBox.Show("Sektor musi byc nieujemna liczba", "Walidacja", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (shelfStr == "" || !int.TryParse(shelfStr, out int shelf) || shelf < 0)
            {
                MessageBox.Show("polka nie moze byc nieujemna", "Walidacja", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Products.Add(new Product
            {
                Ean = ean,
                Name = name,
                Category = category,
                Sector = sectorStr,
                Shelf = shelfStr,
                Level = level
            });

            TxtEan.Clear();
            TxtName.Clear();
            TxtCategory.Clear();
            TxtSector.Clear();
            TxtShelf.Clear();
            TxtLevel.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Product product)
            {
                Products.Remove(product);
            }
        }
    }
}

