﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SteamMarket.Steam
{
    public partial class Operation
    {
        public int IdOperation { get; set; }
        public int IdItem { get; set; }
        public int IdUser { get; set; }
        public int AmountItem { get; set; }
        public int IdType { get; set; }
        public decimal? Sum { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Type IdTypeNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}