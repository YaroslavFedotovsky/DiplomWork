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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutDataProcSystem
{
    /// <summary>
    /// Логика взаимодействия для OrganizationPage.xaml
    /// </summary>
    public partial class OrganizationPage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public OrganizationPage()
        {
            InitializeComponent();
            // Заполнение ComboBox при загрузке страницы
            LoadOrganization();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new MainAdminPage());
        }

        private void LoadOrganization()
        {
            try
            {
                // Получаем данные об организациях из базы данных
                DataTable orgTable = db.GetOrganization();

                // Проходим по строкам DataTable и добавляем значения в ComboBox
                foreach (DataRow row in orgTable.Rows)
                {
                    cmbOrg.Items.Add(row["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке организаций: " + ex.Message);
            }
        }

        private void SaveNewOrg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string orgName = orgBox.Text;

                // Проверка, чтобы не добавлять модуль без названия
                if (orgBox == null)
                {
                    MessageBox.Show("Введите название организации.");
                    return;
                }

                // Вызов метода добавления модуля в базу данных
                db.SaveNewOrg(orgName);

                // Очистить текстовые поля и сбросить выбор в ComboBox после успешного добавления
                orgBox.Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
