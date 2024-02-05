﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalFinanceApplication_DAL;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<int>("Account")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.Income", b =>
                {
                    b.Property<int>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeId"));

                    b.Property<int>("Account")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncomeId");

                    b.ToTable("Income", (string)null);

                    b.HasData(
                        new
                        {
                            IncomeId = 1,
                            Account = 2,
                            Amount = 20000m,
                            Category = 2,
                            Currency = "MKD",
                            Date = new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
