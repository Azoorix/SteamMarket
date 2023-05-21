using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
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

namespace SteamMarket.Pages
{
    /// <summary>
    /// Страница WalletPage.xaml служит для отображения и пополнения баланса пользователя, введя необходимую сумму и номер карты
    /// </summary>
    public partial class WalletPage : Page
    {
    

        public WalletPage()
        {
            InitializeComponent();
            TBlockBalance.Text = Math.Round(Convert.ToDecimal(App.CurrentUser.Balance), 2).ToString();
            errorMessage1.Visibility = Visibility.Collapsed;
            errorMessage2.Visibility = Visibility.Collapsed;

        }
        /// <summary>
        ///Пополнение
        /// </summary>
        private void BtnTopUp_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            //Вызов метода проверки ошибок
            /// </summary>
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                App.CurrentUser.Balance += decimal.Parse(TBoxReplenishment.Text);
                TBlockBalance.Text = Math.Round(Convert.ToDecimal(App.CurrentUser.Balance), 2).ToString();
                App.Context.SaveChanges();
            }
        }
        /// <summary>
        ///Метод проверки ошибок
        /// </summary>
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();


            if (string.IsNullOrWhiteSpace(TBoxReplenishment.Text))
                errorBuilder.AppendLine("Укажите сумму пополнения");
            else
            {
                if (App.CurrentUser.Balance + decimal.Parse(TBoxReplenishment.Text) >= 99999999)
                {
                    errorBuilder.AppendLine("Вы достигли лимита баланса");

                }
            }
            
            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        /// <summary>
        ///Событие, с помощью которого проверяются введённые символы (разрешено вводить только цифры и символ запятой)
        /// </summary>
        private void TBoxReplenishment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",")
               && (!TBoxReplenishment.Text.Contains(",")
               && TBoxReplenishment.Text.Length != 0)))
            {

                e.Handled = true;
            }

        }
        /// <summary>
        ///Событие изменения текста в текстовом поле
        /// </summary>
        private void TBoxReplenishment_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(TBoxReplenishment.Text) & !string.IsNullOrWhiteSpace(TBoxReplenishment.Text))
            {   /// <summary>
                ///Если введённое число больше 150000, то отображается предупреждающее сообщение
                /// </summary>
               
               

                if (App.CurrentUser.Balance + decimal.Parse(TBoxReplenishment.Text) >= 99999999)
                {
                    BtnTopUp.IsEnabled = false;
                    errorMessage2.Visibility = Visibility.Visible;
                }
                else if (decimal.Parse(TBoxReplenishment.Text) > 150000)
                {
                    errorMessage1.Visibility = Visibility.Visible;
                    BtnTopUp.IsEnabled = false;

                }
                else if (decimal.Parse(TBoxReplenishment.Text) < 150000 && App.CurrentUser.Balance + decimal.Parse(TBoxReplenishment.Text) <= 99999999)
                {
                    BtnTopUp.IsEnabled=true;
                    errorMessage2.Visibility = Visibility.Collapsed;
                    errorMessage1.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
