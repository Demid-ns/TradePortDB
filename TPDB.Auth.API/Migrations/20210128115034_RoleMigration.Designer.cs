﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPDB.Auth.API.Data;

namespace TPDB.Auth.API.Migrations
{
    [DbContext(typeof(TPDBAuthContext))]
    [Migration("20210128115034_RoleMigration")]
    partial class RoleMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.Property<Guid>("AccountsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RolesID")
                        .HasColumnType("int");

                    b.HasKey("AccountsId", "RolesID");

                    b.HasIndex("RolesID");

                    b.ToTable("AccountRole");
                });

            modelBuilder.Entity("TPDB.Auth.API.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TPDB.Auth.API.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.HasOne("TPDB.Auth.API.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TPDB.Auth.API.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
