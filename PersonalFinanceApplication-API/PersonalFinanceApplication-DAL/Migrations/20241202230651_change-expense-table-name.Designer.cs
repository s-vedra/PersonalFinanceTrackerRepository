﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalFinanceApplication_DAL;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241202230651_change-expense-table-name")]
    partial class changeexpensetablename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("PaymentIssue")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserContractId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId");

                    b.HasIndex("UserContractId");

                    b.ToTable("Expense", (string)null);
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.Income", b =>
                {
                    b.Property<int>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeId"));

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

                    b.Property<int>("PaymentIssue")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserContractId")
                        .HasColumnType("int");

                    b.HasKey("IncomeId");

                    b.HasIndex("UserContractId");

                    b.ToTable("Income", (string)null);

                    b.HasData(
                        new
                        {
                            IncomeId = 1,
                            Amount = 20000m,
                            Category = 2,
                            Currency = "MKD",
                            Date = new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            PaymentIssue = 2,
                            UserContractId = 1
                        });
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FirstName = "admin",
                            LastName = "admin",
                            Password = "admin123",
                            UserName = "admin@admin"
                        });
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.UserContract", b =>
                {
                    b.Property<int>("UserContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserContractId"));

                    b.Property<string>("ContractName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContractType")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOpened")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserContractStatus")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserContractId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContract", (string)null);

                    b.HasData(
                        new
                        {
                            UserContractId = 1,
                            ContractName = "PCB CA-4879",
                            ContractType = 1,
                            DateOpened = new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            UserContractStatus = 2,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.Expense", b =>
                {
                    b.HasOne("PersonalFinanceApplication_DomainModels.Models.UserContract", "UserContract")
                        .WithMany("Expenses")
                        .HasForeignKey("UserContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserContract");
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.Income", b =>
                {
                    b.HasOne("PersonalFinanceApplication_DomainModels.Models.UserContract", "UserContract")
                        .WithMany("Incomes")
                        .HasForeignKey("UserContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserContract");
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.UserContract", b =>
                {
                    b.HasOne("PersonalFinanceApplication_DomainModels.Models.User", "User")
                        .WithMany("UserContracts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.User", b =>
                {
                    b.Navigation("UserContracts");
                });

            modelBuilder.Entity("PersonalFinanceApplication_DomainModels.Models.UserContract", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Incomes");
                });
#pragma warning restore 612, 618
        }
    }
}