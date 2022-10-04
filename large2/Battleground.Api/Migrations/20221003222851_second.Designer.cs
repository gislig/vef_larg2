﻿// <auto-generated />
using System;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Battleground.Api.Migrations
{
    [DbContext(typeof(BattlegroundDbContext))]
    [Migration("20221003222851_second")]
    partial class second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Battleground.Repositories.Entities.Attack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BattlePokemonId")
                        .HasColumnType("integer");

                    b.Property<int>("CriticalHit")
                        .HasColumnType("integer");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonIdentifier")
                        .HasColumnType("integer");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Attacks");
                });

            modelBuilder.Entity("Battleground.Repositories.Entities.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int>("WinnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("Battleground.Repositories.Entities.BattlePlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BattlesId")
                        .HasColumnType("integer");

                    b.Property<int>("PlayersInMatchId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BattlePlayers");
                });

            modelBuilder.Entity("Battleground.Repositories.Entities.BattlePokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BattleId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonIdentifier")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BattlePokemons");
                });

            modelBuilder.Entity("Battleground.Repositories.Entities.BattleStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BattleStatuses");
                });

            modelBuilder.Entity("Battleground.Repositories.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Battleground.Repositories.Entities.PlayerInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AcquiredDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<string>("PokemonIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlayerInventories");
                });
#pragma warning restore 612, 618
        }
    }
}
