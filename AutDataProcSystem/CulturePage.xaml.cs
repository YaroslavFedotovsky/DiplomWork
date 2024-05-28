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
    /// Логика взаимодействия для CulturePage.xaml
    /// </summary>
    public partial class CulturePage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public CulturePage()
        {
            InitializeComponent();
            // Заполнение ComboBox при загрузке страницы
            LoadCultures();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new MainAdminPage());
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

        private void SaveNewCult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cultName = cultBox.Text;

                // Проверка, чтобы не добавлять модуль без названия
                if (cultBox == null)
                {
                    MessageBox.Show("Введите название выращиваемой культуры.");
                    return;
                }

                // Вызов метода добавления модуля в базу данных
                db.SaveNewCult(cultName);

                // Очистить текстовые поля и сбросить выбор в ComboBox после успешного добавления
                cultBox.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
