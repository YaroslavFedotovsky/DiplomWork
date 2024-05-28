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
    /// Логика взаимодействия для MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Page
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

        public MainUserPage()
        {
            InitializeComponent();
            
        }

        //Возвращение на страницу авторизации
        private void GoToAnotherPage_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new AuthorizationPage());
        }

        //Переход на страницу Засев
        private void GoToSitting_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new SittingUserPage());
        }

        //Переход на страницу Подкормка
        private void GoToFeeding_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new FeedingUserPage());
        }

        //Переход на страницу Химпрополка
        private void GoToChemicalRegiment_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new ChemicalRegimentUserPage());
        }

        //Переход на страницу Уборка
        private void GoToHarvesting_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new HarvestingUserPage());
        }
    }
}
