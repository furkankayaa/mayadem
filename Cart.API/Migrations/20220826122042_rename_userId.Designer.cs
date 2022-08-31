﻿// <auto-generated />
using Cart.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cart.API.Migrations
{
    [DbContext(typeof(CartItemsContext))]
    [Migration("20220826122042_rename_userId")]
    partial class rename_userId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("App.Library.CartItemDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GameName")
                        .HasColumnType("longtext");

                    b.Property<double>("GamePrice")
                        .HasColumnType("double");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Publisher")
                        .HasColumnType("longtext");

                    b.Property<string>("UserID")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("CartItemDetails");
                });
#pragma warning restore 612, 618
        }
    }
}