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
    /// Страница PageRegEmail1.xaml служит для указания почты новому пользователю, а также подтверждения условий регистрации
    /// </summary>
    public partial class PageRegEmail : Page
    {
        public PageRegEmail()
        {
            InitializeComponent();
        }

        //Продолжение регистрации
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            //Вызов метода проверки ошибок
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var user = new Steam.Users
                {
                    Email = TBoxMail.Text,
                    RoleId = 2, 
                    Balance = 0
                };
                NavigationService.Navigate(new RegistrationPage(user));
            }
        }

        //Проверка ошибок
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (String.IsNullOrWhiteSpace(TBoxMail.Text) || String.IsNullOrWhiteSpace(TBoxMail2.Text))
                errorBuilder.AppendLine("Не указана почта!");

            if (TBoxMail.Text != TBoxMail2.Text)
                errorBuilder.AppendLine("Почты не совпадают!");

            if (!(Regex.IsMatch(TBoxMail.Text, @"[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]")))
                errorBuilder.AppendLine("Неверно введена почта!");

            if (CBoxConfirmation.IsChecked == false)
                errorBuilder.AppendLine("Для продолжения необходимо принять условия регистрации!");
            CBoxConfirmation.Foreground = new SolidColorBrush(Colors.Red);

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}