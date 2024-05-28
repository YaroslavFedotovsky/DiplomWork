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

namespace AutDataProcSystem
{
    /// <summary>
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public string _organizationName;

        //Подлючение названия организации
        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;
                //organizationTextBlock.Text = value;
            }
        }

        public MainAdminPage()
        {
            InitializeComponent();
        }

        //Возвращение на страницу авторизации
        private void GoToAnotherPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new AuthorizationPage());
        }

        //Переход на страницу Добавление пользователя
        private void GoToCreate_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new CreateUserPage());
        }

        //Переход на страницу Редактирование Пользователя
        private void GoToUpdate_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new UpdateUserPage());
        }

        //Переход на страницу Организации
        private void GoToOrganization_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new OrganizationPage());
        }

        //Переход на страницу Посевные культуры
        private void GoToCulture_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new CulturePage());
        }
    }
}
