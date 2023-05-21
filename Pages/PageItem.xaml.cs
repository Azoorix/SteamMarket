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
using System.Xaml;
using SteamMarket.Steam;
namespace SteamMarket.Pages
{
    /// <summary>
    /// Страница PageItem.xaml служит для отображения списка, сортировки, удаления и редактирования предметов
    /// </summary>
    public partial class PageItem : Page
    {
        private Steam.Item? _currentItem = null;
        private string amount;
        public PageItem()
        {
            InitializeComponent();
            TBoxAmount.PreviewTextInput += new TextCompositionEventHandler(TBoxAmount_PreviewTextInput);
            TBoxAmount.MaxLength = 5;
        }

        /// <summary>
        ///Загрузка данных об открытом предмете
        /// </summary>
        public PageItem(Steam.Item item)
        {
            InitializeComponent();
            /// <summary>
            ///Проверка роли пользователя: 1 - Админ, 2 - Пользователь
            /// </summary>
            if (App.CurrentUser.RoleId == 1)
            {
                BtnBuy.Content = "Редактировать";
                TBoxAmount.Visibility = Visibility.Collapsed;
                TBlockAmount.Visibility = Visibility.Collapsed;
                BtnDell.Visibility = Visibility.Visible;
                InventoryLink.Visibility = Visibility.Collapsed;
                WalletLink.Visibility = Visibility.Collapsed;
                UserGrid.Width = 60;
            }
            else
            {
                BtnBuy.Content = "Купить";
                TBoxAmount.Visibility = Visibility.Visible;
                TBlockAmount.Visibility = Visibility.Visible;
                BtnDell.Visibility = Visibility.Collapsed;
                TBlockBalance.Text = App.CurrentUser.Balance.ToString();
            }
            _currentItem = item;
            if (_currentItem.Picture != null)
                ImageItem.Source = (ImageSource?)new ImageSourceConverter().ConvertFrom(_currentItem.Picture);
            errorMessage.Visibility = Visibility.Collapsed;
            TBlockDescrItem.Text = _currentItem.Description;
            TBlockPriceItem.Text = _currentItem.Price.ToString() + "₽";
            TBlockLogin.Text = App.CurrentUser.Login.ToUpper();
            TBlockGame.Text = App.Context.Game.Where(c => c.IdGame == _currentItem.Game).Select(c => c.Name).FirstOrDefault(); ;
            userimg.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(App.CurrentUser.Image);


        }

        /// <summary>
        ///Покупка для пользователя и редактирование предмета для админа
        /// </summary>

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            ///Проверка роли пользователя: 1 - Админ, 2 - Пользователь
            /// </summary>
            if (App.CurrentUser.RoleId == 1)
            {
                NavigationService.Navigate(new AddNewItemPage(_currentItem));


            }
            else
            {
                /// <summary>
                ///Вызов метода проверки на ошибки
                /// </summary>

                var errorMessage = CheckErrors();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    var price = App.CurrentUser.Balance - (_currentItem.Price * Int32.Parse(amount));


                    if (price < 0)
                    {
                        MessageBox.Show("Недостаточно средств на балансе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {

                        App.CurrentUser.Balance = price;
                        TBlockBalance.Text = App.CurrentUser.Balance.ToString();
                        _currentItem.Amount = (_currentItem.Amount - Int32.Parse(amount));
                        App.Context.SaveChanges();

                        /// <summary>
                        ///Запись операции покупки
                        /// </summary>
                        var operationBuy = new Steam.Operation
                        {

                            IdItem = _currentItem.IdItem,
                            IdUser = App.CurrentUser.IdUser,
                            AmountItem = Convert.ToInt32(amount),
                            IdType = 1,
                            Sum = _currentItem.Price * Convert.ToInt32(amount)

                        };
                        App.Context.Operation.Add(operationBuy);



                        var invitem = App.CurrentUser.Inventory.Where(c => c.IdItem == _currentItem.IdItem);
                        /// <summary>
                        ///Проверка наличия данного предмета у пользователя
                        ///Если у текущего пользователя уже есть данный предмет, то изменяется только его количество
                        ///Если у текущего пользователя данного предмета нет, то он записывается в инвентарь
                        /// </summary>

                        if (invitem.Any())
                        {


                            invitem.FirstOrDefault().AmountItem += Convert.ToInt32(amount);
                            App.Context.SaveChanges();

                        }
                        else
                        {
                            /// <summary>
                            ///Запись предмета в инвентарь пользователя
                            /// </summary>

                            var InventoryBuy = new Steam.Inventory
                            {
                                IdUser = App.CurrentUser.IdUser,
                                IdItem = _currentItem.IdItem,
                                AmountItem = Convert.ToInt32(amount)
                            };
                            App.Context.Inventory.Add(InventoryBuy);
                            App.Context.SaveChanges();

                        }
                        MessageBox.Show("Вы успешно приобрели " + amount + " " + _currentItem.Name, "Покупка", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
        }

        /// <summary>
        ///Метод проверки на ошибки
        /// </summary>

        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (String.IsNullOrEmpty(amount) || Int32.Parse(amount) == 0)
                errorBuilder.AppendLine("Неверно указано количество");

            if (_currentItem.Amount == 0)
                errorBuilder.AppendLine("В данный момент предмет отсутствует в наличии");

            return errorBuilder.ToString();
        }


        /// <summary>
        ///Удаление предмета (для администратора)
        /// </summary>
        private void BtnDell_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show($"Вы уверены, что хотите удалить предмет: " + $"{_currentItem.Name}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Item.Remove(_currentItem);
                App.Context.SaveChanges();
                NavigationService.Navigate(new Market());
            }
        }
        /// <summary>
        ///Изменение текста в текстовом блоке стоимости, в зависимости от введённого количества
        /// </summary>
        private void TBoxAmount_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (!string.IsNullOrEmpty(TBoxAmount.Text) & !string.IsNullOrWhiteSpace(TBoxAmount.Text))
            {
                amount = TBoxAmount.Text.Replace(" ", "");

                TBlockPriceItem.Text = (_currentItem.Price * Int32.Parse(amount)).ToString() + "₽";


                if (Int32.Parse(amount) > _currentItem.Amount)
                {
                    errorMessage.Visibility = Visibility.Visible;
                    BtnBuy.IsEnabled = false;
                }
                else
                {
                    errorMessage.Visibility = Visibility.Collapsed;
                    BtnBuy.IsEnabled = true;
                }

            }
        }
        /// <summary>
        //Событие, с помощью которого проверяются введённые символы (разрешено вводить только цифры)
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
