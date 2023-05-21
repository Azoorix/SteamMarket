﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SteamMarket.Steam
{
    public partial class SteamContext : DbContext
    {
        public SteamContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder e)
        {

            e.UseSqlServer(@"Data Source = .\SQLEXPRESS; Initial Catalog = SteamDB; Integrated Security = True; Trust Server Certificate = True");
        }
        public SteamContext(DbContextOptions<SteamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.IdGame);

                entity.Property(e => e.IdGame)
                    .ValueGeneratedNever()
                    .HasColumnName("id_game");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountItem).HasColumnName("amount_item");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK_Inventory_Item");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Inventory_Users1");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Game).HasColumnName("game");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.GameNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.Game)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Game1");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.IdOperation);

                entity.ToTable("operation");

                entity.Property(e => e.IdOperation).HasColumnName("id_operation");

                entity.Property(e => e.AmountItem).HasColumnName("amount_item");

                entity.Property(e => e.IdItem).HasColumnName("id_item");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Sum)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("sum");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_operation_Item");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_operation_type");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_operation_Users1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.IdType);

                entity.ToTable("type");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}