using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SteamMarket.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Xml;

namespace SteamMarket.Pages
{
    /// <summary>
    /// Страница UserProfile.xaml служит для редактирования пароля, логина, аватара и почты, а также предоставляет возможность удалить аккаунт
    /// </summary>
    public partial class UserProfile : Page
    {
        private byte[]? _mainImageData;

        public UserProfile()
        {
            InitializeComponent();
            TBoxLogin.Text = App.CurrentUser.Login;
            TBoxPassword.Text = App.CurrentUser.Password;
            TBoxMail.Text = App.CurrentUser.Email;
            if (App.CurrentUser.Image != null)
                UserImg.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(App.CurrentUser.Image);
            else
            {
                UserImg.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(Properties.Resources.default_image);
            }
        }
        /// <summary>
        /// Изменение аватара
        /// </summary>
        private void BtnSetImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Image |*.png; *.jpg; *.jpeg"
            };
            if (ofd.ShowDialog() == true)
                _mainImageData = File.ReadAllBytes(ofd.FileName);

            if (_mainImageData != null)
                UserImg.Source = (ImageSource?)new ImageSourceConverter().ConvertFrom(_mainImageData);
            
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            /// Вызов метода проверки ошибок
            /// </summary>
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                App.CurrentUser.Login = TBoxLogin.Text;
                if (_mainImageData != null)
                    App.CurrentUser.Image = _mainImageData;
                App.CurrentUser.Email = TBoxMail.Text;
                App.CurrentUser.Password = TBoxPassword.Text;
                App.Context.SaveChanges();
            }
        }



        /// <summary>
        /// Метод проверки ошибок
        /// </summary>
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (String.IsNullOrWhiteSpace(TBoxLogin.Text))
            {
                errorBuilder.Append("Не указано имя аккаунта!");
            }

            if (TBoxLogin.Text.Length > 11)
            {
                errorBuilder.Append("Слишком длинный логин! Логин может достигать 11 символов");

            }

            var editUser = App.Context.Users.ToList().FirstOrDefault(p => p.Login.ToLower() == TBoxLogin.Text.ToLower());
            if (editUser != null & App.CurrentUser.Login != TBoxLogin.Text)
            {
                errorBuilder.AppendLine("Такой логин уже занят");

            }
            if (String.IsNullOrEmpty(TBoxPassword.Text))
            {
                errorBuilder.Append("Не указан пароль!");

            }
            if (TBoxPassword.Text.Length < 8)
            {
                errorBuilder.Append("Слишком короткий пароль!");
            }
            if (TBoxLogin.Text.Length < 3 || TBoxLogin.Text == String.Empty)
            {
                errorBuilder.Append("Слишком короткий логин!");

            }
            else if (!(Regex.IsMatch(TBoxPassword.Text, @"\W") && (Regex.IsMatch(TBoxPassword.Text, @"\d") && Regex.IsMatch(TBoxPassword.Text, @"[а-я]")
            && Regex.IsMatch(TBoxPassword.Text, @"[А-Я]") || Regex.IsMatch(TBoxPassword.Text, @"[a-z]") || Regex.IsMatch(TBoxPassword.Text, @"[A-Z]"))))
            {
                errorBuilder.Append("Пароль должен содержать в себе хотя бы один символ, одну цифру, одну строчную букву и одну прописную букву!");
            }

            else if (String.IsNullOrWhiteSpace(TBoxMail.Text) || String.IsNullOrWhiteSpace(TBoxMail.Text))
            {
                errorBuilder.Append("Не указана почта!");
            }
            else if (!(Regex.IsMatch(TBoxMail.Text, @"[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]")))
            {
                errorBuilder.Append("Неверно введена почта!");
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
         }





        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите удалить учетную запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                List<Operation> operationsDelete = App.Context.Operation.Where(e => e.IdUser == App.CurrentUser.IdUser).ToList();
                List<Steam.Inventory> inventoryDelete = App.Context.Inventory.Where(e => e.IdUser == App.CurrentUser.IdUser).ToList();

                App.Context.RemoveRange(operationsDelete);
                App.Context.RemoveRange(inventoryDelete);

                App.Context.Users.Remove(App.CurrentUser);
                App.Context.SaveChanges();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();

                MainWindow mainWindow = (MainWindow)MainWindow.GetWindow(this);
                mainWindow.Close();
            }
            
        }
    }
}
