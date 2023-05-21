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

namespace SteamMarket.Pages
{
    /// <summary>
    /// Страница Inventory.xaml служит для отображения иевениаря пользователей, а также для продажи
    /// </summary>
    public partial class Inventory : Page
    {
        
        public Inventory()
        {
            InitializeComponent();
            var inventory = App.Context.Inventory.Where(c => c.IdUser == App.CurrentUser.IdUser).ToList();
            inventory = inventory.ToList();
            LViewInventory.ItemsSource = inventory;
            userimg.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(App.CurrentUser.Image);
            TBlockBalance.Text = App.CurrentUser.Balance.ToString();
            TBlockLogin.Text = App.CurrentUser.Login.ToUpper();
        }



        /// <summary>
        ///Открытие окна продажи предмета
        /// </summary>

        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            var selectInventory = (sender as Button).DataContext as Steam.Inventory;
            SellWindow sellWindow = new SellWindow(selectInventory);
            if(sellWindow.ShowDialog() == false)
            {
                NavigationService.Navigate(new Inventory());
            }
        }
    }
}
