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
    /// Страница UsersInfoPage.xaml служит для отображения списка пользователей, а также предоставляет возможность их удаления
    /// </summary>
    public partial class UsersInfoPage : Page
    {
        public UsersInfoPage()
        {
            InitializeComponent();
            UpdateUsers();
        }
        /// <summary>
        ///Метод загрузки/обновления данных списка
        /// </summary>
        private void UpdateUsers()
        {
            var users = App.Context.Users.Where(c => c.RoleId == 2).ToList();
            users = users.ToList();
            LViewUsers.ItemsSource = users;

        }
        /// <summary>
        ///Удаление пользователя
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = (sender as Button).DataContext as Steam.Users;
            if (MessageBox.Show($"Вы уверены, что хотите удалить пользователя: " + $"{currentUser.Login}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Users.Remove(currentUser);
                App.Context.SaveChanges();
                UpdateUsers();
            }
        }
    }
}
