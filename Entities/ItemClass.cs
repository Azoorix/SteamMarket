using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SteamMarket.Steam
{
    public partial class Item
    {
        public string AdminControlsVisibility
        {
            get
            {
                if (App.CurrentUser.RoleId == 1)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }


    }
}
