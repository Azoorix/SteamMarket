﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SteamMarket.Steam
{
    public partial class Inventory
    {
        public int? IdUser { get; set; }
        public int? IdItem { get; set; }
        public int? AmountItem { get; set; }
        public int Id { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}