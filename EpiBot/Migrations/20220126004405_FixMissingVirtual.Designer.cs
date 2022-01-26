﻿// <auto-generated />
using EpiBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Epibot.Migrations
{
    [DbContext(typeof(EpiBotContext))]
    [Migration("20220126004405_FixMissingVirtual")]
    partial class FixMissingVirtual
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("EpiBot.Models.Byline", b =>
                {
                    b.Property<int>("BylineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("BylineId");

                    b.ToTable("Bylines");

                    b.HasData(
                        new
                        {
                            BylineId = 1,
                            Email = "hannah@corgibyte.com",
                            Name = "Hannah Young"
                        },
                        new
                        {
                            BylineId = 2,
                            Email = "abminnick@gmail.com",
                            Name = "Aaron Minnick"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}