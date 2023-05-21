using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SteamMarket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Steam.SteamContext Context { get; } = new Steam.SteamContext();
        public static Steam.Users CurrentUser = null;

    }
}
