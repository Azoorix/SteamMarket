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
    /// Страница OperationPage.xaml служит для отображения, печати и сортировки списка операций
    /// </summary>
    public partial class OperationPage : Page
    {

        public OperationPage()
        {
            InitializeComponent();
            var operations = App.Context.Operation.ToList();
            /// <summary>
            ///Проверка роли пользователя: 1 - Админ, 2 - Пользователь
            /// </summary>


            if (App.CurrentUser.RoleId == 1)
            {

                operations = operations.ToList();
                User.Visibility = Visibility.Visible;
                OperationsGrid.ItemsSource = operations;

            }
            else
            {
                operations = operations.Where(c => c.IdUser == App.CurrentUser.IdUser).ToList();
                operations = operations.ToList();
                User.Visibility = Visibility.Collapsed;
                OperationsGrid.ItemsSource = operations;


            }

        }

        /// <summary>
        ///Печать списка операций
        /// </summary>

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                grid.Visibility = Visibility.Hidden;
                double height = this.Height;
                int pageMargin = 5;
                OperationsGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                OperationsGrid.FontSize = 15;
                Size pageSize = new Size(printDialog.PrintableAreaWidth - pageMargin * 2, printDialog.PrintableAreaHeight - 20);
                this.Height = pageSize.Height;

                printDialog.PrintVisual(OperationsGrid, "Печать списка операций");
                OperationsGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                this.Height = height;
                grid.Visibility = Visibility.Visible;
            }


        }

    }


}
