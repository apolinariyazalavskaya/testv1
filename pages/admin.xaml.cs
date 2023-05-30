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
using сайт_магазина_СП.pages;
using сайт_магазина_СП.models;
namespace сайт_магазина_СП.pages
{
    /// <summary>
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        public admin()
        {
            InitializeComponent();
        }       

        private void DataGridproducts_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();

        }

        private void BtnEdit_Click_1(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new pages.redactor((sender as Button).DataContext as product));
        }

        private void BtnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pages.redactor(null));
        }

        private void BtnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedProducts = DataGridproducts.SelectedItems.Cast<product>().ToList();

            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedProducts.Count()}",
           "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    product x = selectedProducts[0];
                    магазин_продуктов_пп03Entities1.GetContext().products.Remove(x);
                    магазин_продуктов_пп03Entities1.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    List<product> products = магазин_продуктов_пп03Entities1.GetContext().products.OrderBy(p =>
                    p.наименование).ToList();
                    DataGridproducts.ItemsSource = null;
                    DataGridproducts.ItemsSource = products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }

            }
        }

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DataGridproducts.ItemsSource = null;

                магазин_продуктов_пп03Entities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                List<product> products = магазин_продуктов_пп03Entities1.GetContext().products.OrderBy(p =>
                p.наименование).ToList();
                DataGridproducts.ItemsSource = products;
            }

        }
    }
}
