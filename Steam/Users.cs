﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SteamMarket.Steam
{
    public partial class Users
    {
        public Users()
        {
            Inventory = new HashSet<Inventory>();
            Operation = new HashSet<Operation>();
        }

        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal? Balance { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Operation> Operation { get; set; }
    }
}