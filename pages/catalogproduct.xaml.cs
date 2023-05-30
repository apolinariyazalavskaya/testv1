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

using сайт_магазина_СП.models;

namespace сайт_магазина_СП.pages
{
    /// <summary>
    /// Логика взаимодействия для catalogproduct.xaml
    /// </summary>
    public partial class catalogproduct : Page
    {
        int _itemcount = 0;
        int _allitemcount = 0;
        int _pagenumber = 0;
        int _pagecount = 0;
        List<product> products;
        public catalogproduct()
        {
            InitializeComponent();
           
            var productTypes = магазин_продуктов_пп03Entities1.GetContext().product_types.OrderBy(p =>
    p.название_типа_продукта).ToList();
            productTypes.Insert(0, new product_types
            {
                название_типа_продукта = "Все типы"
            }
            );
            combobox_.ItemsSource = productTypes;
            combobox_.SelectedIndex = 0;
        }

        public void InitializeListBoxPages()
        {
            ListBoxPageCount.Items.Clear();
            _pagecount = _itemcount / 10;
            if (_itemcount % 10 != 0)
                _pagecount++;
            for (int i = 1; i <= _pagecount; i++)
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = i.ToString();
                ListBoxPageCount.Items.Add(itm);
            }
        }
        void LoadData()
        {
            магазин_продуктов_пп03Entities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            products = магазин_продуктов_пп03Entities1.GetContext().products.OrderBy(p => p.наименование).ToList();
            _allitemcount = products.Count;
            _itemcount = _allitemcount;
            ListBoxproducts.ItemsSource = магазин_продуктов_пп03Entities1.GetContext().products.OrderBy(p => p.наименование).ToList();
            _pagenumber = 1;
            InitializeListBoxPages();
            int k = products.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxproducts.ItemsSource = products.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxproducts.ItemsSource = products.GetRange((_pagenumber - 1) * 10, 10);
            TextBlockCount.Text = $"Результат запроса: {_itemcount} записей из {_allitemcount}";
        }


        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void combobox__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var currentproducts = магазин_продуктов_пп03Entities1.GetContext().products.OrderBy(p => p.наименование).ToList();
            if (combobox_.SelectedIndex > 0)
                currentproducts = currentproducts.Where(p => p.тип_товара == (combobox_.SelectedItem as product_types).ID_тип_продукта).ToList();
            currentproducts = currentproducts.Where(p => p.наименование.ToLower().Contains(search.Text.ToLower())).ToList();
            if (combosort.SelectedIndex >= 0)
            {
                if (combosort.SelectedIndex == 0)
                    currentproducts = currentproducts.OrderBy(p => p.наименование).ToList();
                if (combosort.SelectedIndex == 1)
                    currentproducts = currentproducts.OrderByDescending(p => p.наименование).ToList();
                if (combosort.SelectedIndex == 2)
                    currentproducts = currentproducts.OrderBy(p => p.цена).ToList();
                if (combosort.SelectedIndex == 3)
                    currentproducts = currentproducts.OrderByDescending(p => p.цена).ToList();

            }

            _pagenumber = 1;
            products = currentproducts;
            ListBoxproducts.ItemsSource = currentproducts;
            _itemcount = currentproducts.Count;
            InitializeListBoxPages();
            int k = products.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxproducts.ItemsSource = products.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxproducts.ItemsSource = products.GetRange((_pagenumber - 1) * 10, 10);

            TextBlockCount.Text = $" Результат запроса: {currentproducts.Count} записей из { _allitemcount  } ";
        }


        private void combosort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ListBoxproducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if ((_pagenumber > 1))
                _pagenumber--;
            ListBoxPageCount.SelectedIndex = _pagenumber - 1;
        }

        private void ListBoxPageCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxPageCount.SelectedItems.Count == 0)
                return;
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);

            _pagenumber = Convert.ToInt32(lbi.Content);
            int k = products.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxproducts.ItemsSource = products.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxproducts.ItemsSource = products.GetRange((_pagenumber - 1) * 10, 10);
            TextBlockCount.Text = $" Результат запроса: {_itemcount} записей из {_allitemcount}";
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {

            if ((_pagenumber < _pagecount))
                _pagenumber++;
            ListBoxPageCount.SelectedIndex = _pagenumber - 1;
        }
       

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }

        }
    }
}

