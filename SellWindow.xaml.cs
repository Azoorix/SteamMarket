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
using SteamMarket.Steam;

namespace SteamMarket
{
    /// <summary>
    /// Страница SellWindow.xaml служит для продажи предмета
    /// </summary>
    public partial class SellWindow : Window
    {
        private Steam.Inventory? _currentItem = null;
        private string amount;

        public SellWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        ///Загрузка данных о продаваемом предмете
        /// </summary>
        public SellWindow(Steam.Inventory inventory)
        {
            InitializeComponent();
            _currentItem = inventory;
            if(inventory.IdItemNavigation.Picture != null)          
                itm.Source = (ImageSource?)new ImageSourceConverter().ConvertFrom(inventory.IdItemNavigation.Picture);
            TBlockName.Text = _currentItem.IdItemNavigation.Name;

            TBlockGame.Text = App.Context.Game.Where(c => c.IdGame == _currentItem.IdItemNavigation.Game).Select(c => c.Name).FirstOrDefault();
            TBlockMoney.Text = _currentItem.IdItemNavigation.Price.ToString();
            errorMessage.Visibility = Visibility.Collapsed;
            MinWidth = this.MinWidth;
            MinHeight = this.MinHeight;

        }

        /// <summary>
        ///Продажа предмета
        /// </summary>
        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(amount))
            {
                MessageBox.Show("Не верно указано количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Convert.ToInt32(amount) > _currentItem.AmountItem)
            {
                MessageBox.Show("У вас нет такого количества предметов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                
                _currentItem.IdItemNavigation.Amount += Convert.ToInt32(amount);
                _currentItem.AmountItem -= Convert.ToInt32(amount);
                if (App.CurrentUser.Balance + (_currentItem.IdItemNavigation.Price * Convert.ToInt32(amount)) >= 99999999)
                {
                   if(MessageBox.Show("Ваш баланс достиг лимита", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error)== MessageBoxResult.OK)
                   {
                        this.Close();

                   }
                }
                else
                {
                    App.CurrentUser.Balance += _currentItem.IdItemNavigation.Price * Convert.ToInt32(amount);
                    /// <summary>
                    ///Запись операции продажи
                    /// </summary>
                    var operationSell = new Steam.Operation
                    {

                        IdItem = _currentItem.IdItemNavigation.IdItem,
                        IdUser = App.CurrentUser.IdUser,
                        AmountItem = Convert.ToInt32(amount),
                        IdType = 2,
                        Sum = _currentItem.IdItemNavigation.Price * Convert.ToInt32(amount)

                    };
                    App.Context.Operation.Add(operationSell);

                    if (_currentItem.AmountItem == 0)
                    {
                        App.Context.Inventory.Remove(_currentItem);
                    }
                    App.Context.SaveChanges();
                    MessageBox.Show("Вы успешно продали " + amount + " " + _currentItem.IdItemNavigation.Name, "Продажа", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }

            }
        }
        /// <summary>
        ///Событие смены текста в текстовом поле количество
        /// </summary>
        private void TBoxAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBoxAmount.Text) & !string.IsNullOrWhiteSpace(TBoxAmount.Text))
            {
                amount = TBoxAmount.Text.Replace(" ", "");
                /// <summary>
                ///Если введённое количество стоит дороже 150000, то появляется предупреждающее сообщение
                /// </summary>
                if ((_currentItem.IdItemNavigation.Price * Convert.ToInt64(amount)) > 150000)
                {
                    errorMessage.Visibility = Visibility.Visible;
                    TBlockMoney.Text = "0";
                }
                else
                {
                    TBlockMoney.Text = (_currentItem.IdItemNavigation.Price * Convert.ToInt32(amount)).ToString();
                    errorMessage.Visibility = Visibility.Collapsed;
                }
               
            }
        }
        /// <summary>
        ///Событие, с помощью которого проверяются введённые символы (разрешено вводить только цифры)
        /// </summary>
        private void TBoxAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
    }
}
