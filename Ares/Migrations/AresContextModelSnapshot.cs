﻿// <auto-generated />
using Ares.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Ares.Migrations
{
    [DbContext(typeof(AresContext))]
    partial class AresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ares.Models.AuctionItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AuctionEndTime");

                    b.Property<string>("Description");

                    b.Property<int?>("ItemState");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("AuctionItem");
                });

            modelBuilder.Entity("Ares.Models.Offer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuctionID");

                    b.Property<string>("BuyerName");

                    b.Property<DateTime>("OfferTime");

                    b.Property<decimal>("Price");

                    b.HasKey("ID");

                    b.ToTable("Offer");
                });
#pragma warning restore 612, 618
        }
    }
}
