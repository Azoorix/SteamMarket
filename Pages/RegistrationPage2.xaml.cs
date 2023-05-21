using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
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

namespace SteamMarket
{
    /// <summary>
    /// Страница RegistrationPage2.xaml служит для указания логина и пароля новому пользователю
    /// </summary>
    public partial class RegistrationPage : Page
    {
        Steam.Users _newUser = null;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        public RegistrationPage(Steam.Users users)
        {
            InitializeComponent();
            _newUser = users;
        }
        /// <summary>
        ///Завершение регистрации и добавление нового пользователя
        /// </summary>
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            ///Вызов метода проверки ошибок
            /// </summary>
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                _newUser.Password = TBoxPassword.Text;
                _newUser.Balance = 0;
                _newUser.Login = TBoxLogin.Text;
                MessageBox.Show("Вы успешно зарегистрировались!");
                App.Context.Users.Add(_newUser);
                App.Context.SaveChanges();

                RegistrationWindow registrationWindow = (RegistrationWindow)RegistrationWindow.GetWindow(this);
                registrationWindow.Close();
            }
        }
        /// <summary>
        ///Метод проверки ошибок
        /// </summary>
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (String.IsNullOrWhiteSpace(TBoxLogin.Text))
            {
                errorBuilder.Append("Не указан логин!");
            }

            if (TBoxLogin.Text.Length > 11)
            {
                errorBuilder.Append("Слишком длинный логин! Логин может достигать 11 символов!");

            }

            var userFromDB = App.Context.Users.ToList().FirstOrDefault(p => p.Login.ToLower() == TBoxLogin.Text.ToLower());
            if (userFromDB != null && userFromDB != _newUser)
            {
                errorBuilder.AppendLine("Пользователь с таким логином уже зарегистрирован!");

            }
            if (String.IsNullOrEmpty(TBoxPassword.Text) || String.IsNullOrEmpty(TBoxPassword2.Text))
            {
                errorBuilder.Append("Не указан пароль!");

            }

            if (TBoxPassword.Text != TBoxPassword2.Text)
            {
                errorBuilder.Append("Пароли не совпадают!");

            }
            if (TBoxPassword.Text.Length < 8)
            {
                errorBuilder.Append("Слишком короткий пароль!");
            }
            if (TBoxLogin.Text.Length < 3 & TBoxLogin.Text != String.Empty)
            {
                errorBuilder.Append("Слишком короткий логин!");

            }
            else if (!(Regex.IsMatch(TBoxPassword.Text, @"\W") && (Regex.IsMatch(TBoxPassword.Text, @"\d") && Regex.IsMatch(TBoxPassword.Text, @"[а-я]")
            && Regex.IsMatch(TBoxPassword.Text, @"[А-Я]") || Regex.IsMatch(TBoxPassword.Text, @"[a-z]") || Regex.IsMatch(TBoxPassword.Text, @"[A-Z]"))))
            {
                errorBuilder.Append("Пароль должен содержать в себе хотя бы один символ, одну цифру, одну строчную букву и одну прописную букву!");
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
        
    } 
}