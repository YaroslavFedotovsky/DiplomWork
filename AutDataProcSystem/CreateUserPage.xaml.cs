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
    /// Логика взаимодействия для CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public CreateUserPage()
        {
            InitializeComponent();
            // Заполнение ComboBox при загрузке страницы
            LoadOrganization();
            LoadRole();
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

        private void LoadRole()
        {
            try
            {
                // Получаем данные об организациях из базы данных
                DataTable roleTable = db.GetRole();

                // Проходим по строкам DataTable и добавляем значения в ComboBox
                foreach (DataRow row in roleTable.Rows)
                {
                    cmbRole.Items.Add(row["role"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке ролей: " + ex.Message);
            }
        }

        private void SaveForms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string log = loginBox.Text;
                string pass = passBox.Text;
                string nameUs = nameBox.Text;
                string phoneUs = phoneBox.Text;
                string emailUs = emailBox.Text;


                // Проверка, чтобы не добавлять модуль без названия
                if (log == null || pass == null || nameUs == null || phoneUs == null || emailUs == null)
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                // Получаем выбранную организацию из ComboBox
                string selectedOrg = cmbOrg.SelectedItem as string;

                // Проверка, чтобы не добавлять пользователя без выбранной организации
                if (string.IsNullOrWhiteSpace(selectedOrg))
                {
                    MessageBox.Show("Выберите организацию.");
                    return;
                }

                // Получаем выбранную роль из ComboBox
                string selectedRole = cmbRole.SelectedItem as string;

                // Проверка, чтобы не добавлять пользователя без выбранной роли
                if (string.IsNullOrWhiteSpace(selectedRole))
                {
                    MessageBox.Show("Выберите роль.");
                    return;
                }

                // Получаем Id организации по её названию
                int orgId = db.GetOrgIdByName(selectedOrg);

                // Получаем Id роли по её названию
                int roleId = db.GetRoleIdByName(selectedRole);

                // Вызов метода добавления модуля в базу данных
                db.SaveNewUser(log, pass, nameUs, phoneUs, emailUs, orgId, roleId);

                // Очистить текстовые поля и сбросить выбор в ComboBox после успешного добавления
                loginBox.Clear();
                passBox.Clear();
                nameBox.Clear();
                phoneBox.Clear();
                emailBox.Clear();
                cmbOrg.SelectedIndex = -1; // Сбрасываем выбор в ComboBox
                cmbRole.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
