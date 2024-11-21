﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PFA_DAL;

#nullable disable

namespace PFA_DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241121230516_add-property")]
    partial class addproperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PFA_DM.Models.AccountBalance", b =>
                {
                    b.Property<int>("AccountBalanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountBalanceId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastDateAddedMoney")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastDateDrawMoney")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserContractId")
                        .HasColumnType("int");

                    b.HasKey("AccountBalanceId");

                    b.ToTable("AccountBalance", (string)null);

                    b.HasData(
                        new
                        {
                            AccountBalanceId = 1,
                            Amount = 10000m,
                            Currency = "MKD",
                            LastDateAddedMoney = new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Local),
                            LastDateDrawMoney = new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Local),
                            UserContractId = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
