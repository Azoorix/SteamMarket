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
using SteamMarket.Steam;

namespace SteamMarket.Pages
{
    /// <summary>
    /// Страница Market.xaml служит для отображения списка, сортировки, удаления и редактирования предметов
    /// </summary>
    public partial class Market : Page
    {

        public Market()
        {
            InitializeComponent();

            /// <summary>
            ///Проверка роли пользователя: 1 - Админ, 2 - Пользователь
            /// </summary>


            if (App.CurrentUser.RoleId == 1)
            {
                BtnAddItem.Visibility = Visibility.Visible;
                InventoryLink.Visibility = Visibility.Collapsed;
                WalletLink.Visibility = Visibility.Collapsed;
                UserGrid.Width = 60;
            }
            else
            {
                BtnAddItem.Visibility= Visibility.Collapsed;
                InventoryLink.Visibility = Visibility.Visible;
                TBlockBalance.Text = TBlockBalance.Text = Math.Round(Convert.ToDecimal(App.CurrentUser.Balance), 2).ToString();
            }

            CBoxGameSort.ItemsSource = App.Context.Game.Select(c => c.Name).ToList();
            userimg.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(App.CurrentUser.Image);
            TBlockLogin.Text = App.CurrentUser.Login.ToUpper();

            UpdateItems();
        }



        /// <summary>
        ///Сортировка предметов
        /// </summary>

        private void UpdateItems()
        {

            var items = App.Context.Item.ToList();

            if (CBoxSortBy.SelectedIndex == 0)
                items = items.OrderBy(p => p.Price).ToList();
            else
                items = items.OrderByDescending(p => p.Price).ToList();
            if (CBoxGameSort.SelectedIndex == 0)
                items = items.Where(p => p.Game == 1).ToList();
            if (CBoxGameSort.SelectedIndex == 1)
                items = items.Where(p => p.Game == 2).ToList();
            items = items.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewItems.ItemsSource = items;

        }

        /// <summary>
        ///Удаление предмета
        /// </summary>

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = (sender as Button).DataContext as Steam.Item;
            if (MessageBox.Show($"Вы уверены, что хотите удалить предмет: " + $"{currentItem.Name}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                List<Steam.Inventory> inventoryDelete = App.Context.Inventory.Where(e => e.IdItem == currentItem.IdItem).ToList();
                List<Item> itemDelete = App.Context.Item.Where(e => e.IdItem == currentItem.IdItem).ToList();
                List<Operation> operationsDelete = App.Context.Operation.Where(c => c.IdItem == currentItem.IdItem).ToList();
                App.Context.RemoveRange(operationsDelete);
                App.Context.RemoveRange(inventoryDelete);
                App.Context.RemoveRange(itemDelete);
                App.Context.SaveChanges();
                UpdateItems();
            }
        }

        /// <summary>
        ///Переход на страницу редактирования предмета, c данными текущего предмета
        /// </summary>

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = (sender as Button).DataContext as Steam.Item;
            NavigationService.Navigate(new AddNewItemPage(currentItem));
        }

        /// <summary>
        ///Переход на страницу редактирования предмета, с созданием нового предмета
        /// </summary>

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
                NavigationService.Navigate(new AddNewItemPage());

        }


        /// <summary>
        ///Переход на страницу предмета
        /// </summary>

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var currentItem = (sender as Grid).DataContext as Steam.Item;
            NavigationService.Navigate(new PageItem(currentItem));
        }

        /// <summary>
        ///Вызов события сортировки
        /// </summary>

        private void CBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }

        /// <summary>
        ///Вызов события сортировки
        /// </summary>


        private void CBoxGameSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateItems();
        }
        /// <summary>
        ///Вызов события сортировки
        /// </summary>

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateItems();

        }
        /// <summary>
        ///Сброс сортировки
        /// </summary>

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            CBoxGameSort.SelectedIndex = -1;
            CBoxSortBy.SelectedIndex = -1;
            TBoxSearch.Text = String.Empty;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateItems();
        }
    }
    
}
