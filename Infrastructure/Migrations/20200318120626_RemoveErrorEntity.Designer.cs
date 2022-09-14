﻿// <auto-generated />
using System;
using FireplaceApi.Core.Models;
using FireplaceApi.Core.ValueObjects;
using FireplaceApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FireplaceApi.Infrastructure.Migrations
{
    [DbContext(typeof(FireplaceApiContext))]
    [Migration("20200318120626_RemoveErrorEntity")]
    partial class RemoveErrorEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.FileEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RealName")
                        .HasColumnType("text");

                    b.Property<string>("RelativePhysicalPath")
                        .HasColumnType("text");

                    b.Property<string>("RelativeUri")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.GlobalEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<Configs>("Values")
                        .HasColumnName("Values")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("GlobalEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.AccessTokenEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("AccessTokenEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.EmailEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ActivationCode")
                        .HasColumnType("bigint");

                    b.Property<string>("ActivationStatus")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("EmailEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.SessionEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("SessionEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.UserEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.AccessTokenEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithMany("AccessTokenEntities")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.EmailEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithOne("EmailEntity")
                        .HasForeignKey("FireplaceApi.Infrastructure.Entities.UserInformationEntities.EmailEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserInformationEntities.SessionEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithMany("SessionEntities")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
