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
    /// Логика взаимодействия для UpdateUserPage.xaml
    /// </summary>
    public partial class UpdateUserPage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public UpdateUserPage()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new MainAdminPage());
        }

        private void ButtonShowUsers_Click(object sender, RoutedEventArgs e)
        {

            this.dataGridUsers.Columns[0].Header = "Логин";
            this.dataGridUsers.Columns[1].Header = "Пароль";
            this.dataGridUsers.Columns[2].Header = "ФИО";
            this.dataGridUsers.Columns[3].Header = "Телефон";
            this.dataGridUsers.Columns[4].Header = "Почта";
            this.dataGridUsers.Columns[5].Header = "Организация";
            this.dataGridUsers.Columns[5].Header = "Роль";

            this.dataGridUsers.Columns[0].Width = 80;
            this.dataGridUsers.Columns[1].Width = 80;
            this.dataGridUsers.Columns[2].Width = 80;
            this.dataGridUsers.Columns[3].Width = 80;
            this.dataGridUsers.Columns[4].Width = 80;
            this.dataGridUsers.Columns[5].Width = 80;
            this.dataGridUsers.Columns[6].Width = 80;
        }
    }
}
