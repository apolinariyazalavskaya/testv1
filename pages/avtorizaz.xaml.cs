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
using сайт_магазина_СП.pages;



namespace сайт_магазина_СП.pages
{
    /// <summary>
    /// Логика взаимодействия для avtorizaz.xaml
    /// </summary>
    public partial class avtorizaz : Page
    {
        public avtorizaz()
        {
            InitializeComponent();
            comborol.ItemsSource = магазин_продуктов_пп03Entities1.GetContext().roles.ToList();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(logintxb.Text) || string.IsNullOrEmpty(passobox.Password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }
            using (var db = new магазин_продуктов_пп03Entities1())
            {
                var user = db.пользователи
                    .AsNoTracking()
                    .FirstOrDefault(u => u.login == logintxb.Text && u.password == passobox.Password);
                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден");
                    return;

                }

                switch (user.role)
                {
                    case "1":
                        NavigationService.Navigate(new pages.admin());
                        break;
                    case "2":
                        NavigationService.Navigate(new pages.catalogproduct());
                        break;
                }
            }

        }
        private void comborol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
       
    
    }
}
