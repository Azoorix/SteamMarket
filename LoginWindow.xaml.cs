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
    /// Страница LoginPage.xamlслужит для авторизации пользователей
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            MinWidth=this.Width;
            MinHeight=this.Height;
        }

        /// <summary>
        ///Авторизация
        /// </summary>

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var currentUser = App.Context.Users.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password);
                ///<summary>
                ///Проверка на наличие данного пользователя в базе
                ///</summary>

                if (currentUser != null)
                {
                    if (currentUser.Image == null)
                    {
                        currentUser.Image = Properties.Resources.default_image;

                    }
                    App.CurrentUser = currentUser;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Проверьте введённые данные учетной записи и повторите попытку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Отсутствует подключение к базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        ///Открытие страницы регистрации
        /// </summary>


        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }
    }
}
