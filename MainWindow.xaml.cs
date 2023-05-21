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
using SteamMarket.Pages;

namespace SteamMarket
{
    /// <summary>
    /// Страница MainWindow.xaml хранит в себе основные элементы управления и фрейм для отображения страниц
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Market());
            /// <summary>
            ///Проверка роли пользователя: 1 - Админ, 2 - Пользователь
            /// </summary>
            if (App.CurrentUser.RoleId == 1)
            {
                Inventory_PG.Text = "Пользователи";
                Profile_PG.Visibility = Visibility.Collapsed;   
            }
            else
            {
                Profile_PG.Visibility = Visibility.Visible;
                Inventory_PG.Visibility = Visibility.Visible;
            }

       
            MinWidth = this.Width;
            MinHeight = this.Height;
        }
        /// <summary>
        ///Переход на страницу MarketPage
        /// </summary>
        private void Market_PG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameMain.Navigate(new Market());
        }
        /// <summary>
        ///Переход на страницу Inventory со списком предметов пользователя и UsersInfoPage со списком пользователй для администратора
        /// </summary>
        private void Inventory_PG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /// <summary>
            ///Проверка роли пользователя: 1 - Админ, 2 - Пользователь
            /// </summary>
            if (App.CurrentUser.RoleId == 1)
            {
                FrameMain.Navigate(new UsersInfoPage());

            }
            else
            {
                FrameMain.Navigate(new Inventory());

            }
        }
        /// <summary>
        ///Выход из текущего аккаунта
        /// </summary>
        private void LogOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.CurrentUser = null;

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        /// <summary>
        ///Переход на страницу OperationPage со списком операций
        /// </summary>
        private void Operation_PG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameMain.Navigate(new OperationPage());
        }
        /// <summary>
        ///Переход на страницу UserProfile с данными о текущем профиле
        /// </summary>

        private void Profile_PG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameMain.Navigate(new UserProfile());

        }
    }
}
