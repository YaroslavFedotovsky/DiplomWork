using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для SittingUserPage.xaml
    /// </summary>
    public partial class SittingUserPage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public SittingUserPage()
        {
            InitializeComponent();
            // Заполнение ComboBox при загрузке страницы
            LoadCultures();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new MainUserPage());
        }

        private void LoadCultures()
        {
            try
            {
                // Получаем данные о культурах из базы данных
                DataTable culturesTable = db.GetCultures();

                // Проходим по строкам DataTable и добавляем значения в ComboBox
                foreach (DataRow row in culturesTable.Rows)
                {
                    cmbCultures.Items.Add(row["culture_name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке культур: " + ex.Message);
            }
        }

        private void SaveForms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orgId = 1;
                int stageId = 1;
                int volumeWork = Convert.ToInt32(valueBox.Text);

                // Проверка, чтобы не добавлять модуль без названия
                if (volumeWork == 0)
                {
                    MessageBox.Show("Введите количество выполненной работы.");
                    return;
                }

                // Получаем выбранную специальность из ComboBox
                string selectedSpecialty = cmbCultures.SelectedItem as string;

                // Проверка, чтобы не добавлять модуль без выбранной специальности
                if (string.IsNullOrWhiteSpace(selectedSpecialty))
                {
                    MessageBox.Show("Выберите культуру.");
                    return;
                }

                // Получаем Id специальности по её названию
                int cultureId = db.GetCultureIdByName(selectedSpecialty);

                // Вызов метода добавления модуля в базу данных
                db.SaveForms(orgId, stageId, cultureId, volumeWork);

                // Очистить текстовые поля и сбросить выбор в ComboBox после успешного добавления
                valueBox.Clear();
                cmbCultures.SelectedIndex = -1; // Сбрасываем выбор в ComboBox
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
