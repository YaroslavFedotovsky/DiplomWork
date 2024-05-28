using Npgsql;
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
    /// Логика взаимодействия для MainGeneralPage.xaml
    /// </summary>
    public partial class MainGeneralPage : Page
    {
        private DatabaseConnect db = new DatabaseConnect();

        public string _organizationName;

        //Подлючение названия организации
        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;
                organizationTextBlock.Text = value;
            }
        }

        public MainGeneralPage()
        {
            InitializeComponent();
            // Заполняем ComboBox при загрузке страницы
            FillOrganizationComboBox();
        }


        //Возвращение на страницу авторизации
        private void GoToAnotherPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new AuthorizationPage());
        }


        private void FillOrganizationComboBox()
        {
            // Вызываем метод из класса базы данных
            db.FillOrganizationComboBox(comboBoxOrganization);
        }

        private void ButtonShowForms_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что выбрана организация
            if (comboBoxOrganization.SelectedItem != null)
            {
                // Получаем выбранное имя организации из ComboBox
                string selectedOrganization = comboBoxOrganization.SelectedItem.ToString();

                // Вызываем метод для отображения форм для выбранной организации и обновления DataGrid
                db.DisplayFormsForOrganization(selectedOrganization, dataGridForms);
            }
            this.dataGridForms.Columns[0].Header = "Вид работы";
            this.dataGridForms.Columns[1].Header = "Выращиваемая культура";
            this.dataGridForms.Columns[2].Header = "Объем работ, га";
            this.dataGridForms.Columns[3].Header = "Дата";

            this.dataGridForms.Columns[0].Width = 172;
            this.dataGridForms.Columns[1].Width = 172;
            this.dataGridForms.Columns[2].Width = 172;
            this.dataGridForms.Columns[3].Width = 172;
        }
    }
}

