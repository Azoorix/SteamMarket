using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SteamMarket.Steam
{
    public partial class Operation
    {
        public string GetTypeName
        {
            get
            {
                return App.Context.Type.First(c => c.IdType ==  IdType).TypeName;
            }
        }

        public string GetUserLogin
        {
            get
            {
                return App.Context.Users.First(c => c.IdUser == IdUser).Login;        
            }
        }
    }
}
