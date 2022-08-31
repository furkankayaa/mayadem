﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Order.API.Data;

namespace Order.API.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20220825141102_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("App.Library.OrderDetail", b =>
                {
                    b.Property<int>("OrderNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

                    b.HasKey("OrderNum");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("App.Library.OrderedGamesDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GameName")
                        .HasColumnType("longtext");

                    b.Property<double>("GamePrice")
                        .HasColumnType("double");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderedGamesDetails");
                });

            modelBuilder.Entity("App.Library.OrderedGamesDetail", b =>
                {
                    b.HasOne("App.Library.OrderDetail", "Order")
                        .WithMany("OrderedGames")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("App.Library.OrderDetail", b =>
                {
                    b.Navigation("OrderedGames");
                });
#pragma warning restore 612, 618
        }
    }
}
