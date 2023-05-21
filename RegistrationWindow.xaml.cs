using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SteamMarket.Pages;

namespace SteamMarket
{
    /// <summary>
    /// Страница RegistrationWindow.xaml служит для отображения страниц регистрации
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        
        public RegistrationWindow()
        {
            InitializeComponent();

            FrameReg.Navigate(new PageRegEmail());
        }

      
    }
}
