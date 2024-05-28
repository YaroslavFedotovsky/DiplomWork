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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void GoToAnotherPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new MainUserPage());
        }



        private void LoginToTheSystem(object sender, RoutedEventArgs e)
        {
            string login = logField.Text;
            string password = passField.Password;

            if (db.AuthenticateUser(login, password))
            {
                int userRoleId = db.GetUserRoleId(login, password);

                string organizationName = db.GetOrganizationNameForUser(login, password);

                if (userRoleId == 1)
                {
                    // Перенаправление на первую страницу для роли 1.
                    MainUserPage mainUserPage = new MainUserPage();
                    mainUserPage.OrganizationName = organizationName;
                    contentFrame.Navigate(mainUserPage);
                }
                else if (userRoleId == 2)
                {
                    // Перенаправление на вторую страницу для роли 2.
                    MainGeneralPage anotherPage = new MainGeneralPage();
                    anotherPage.OrganizationName = organizationName;
                    contentFrame.Navigate(anotherPage);
                }
                else
                {
                    // Обработка других значений роли или ошибок.
                    MessageBox.Show("Неверная роль пользователя.");
                }
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль.");
            }
        }
    }
}
