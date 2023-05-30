using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для redactor.xaml
    /// </summary>
    public partial class redactor : Page
    {
        private product _currentProducts = new product();

        private string _filePath = null;

        private string _photoName = null;

        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";

        public redactor(product selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
            {
                _currentProducts = selectedProduct;
                _filePath = _currentDirectory + _currentProducts.фото_товара;
               
            }
            DataContext = _currentProducts;
            _photoName = _currentProducts.фото_товара;
            Comboproduct_types.ItemsSource = магазин_продуктов_пп03Entities1.GetContext().product_types.ToList();
            Combofirm.ItemsSource = магазин_продуктов_пп03Entities1.GetContext().info_company.ToList();


        }
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentProducts.наименование))
                s.AppendLine("Поле название пустое");
            if (_currentProducts.product_types == null)
                s.AppendLine("Выберите тип");
            if (string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("фото не выбрано пустое");
            return s;
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
        
            if (_currentProducts.ID_товара == 0)
            {
               
                string photo = ChangePhotoName();
                
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentProducts.фото_товара = photo;
               
                магазин_продуктов_пп03Entities1.GetContext().products.Add(_currentProducts);
            }
            try
            { 
                if (_filePath != null)
                {
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentProducts.фото_товара = photo;
                }
                магазин_продуктов_пп03Entities1.GetContext().SaveChanges(); 
                MessageBox.Show("Запись Изменена");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        


        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg) | *.jpg | GIF Files(*.gif) | *.gif";
                if (op.ShowDialog() == true)
                {
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    PhotoProduct.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
        }
       
        string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }
                    
    }
}
