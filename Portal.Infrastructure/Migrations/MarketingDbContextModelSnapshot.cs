﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using QtasMarketing.Infrastructure;
using System;

namespace Portal.Infrastructure.Migrations
{
    [DbContext(typeof(MarketingDbContext))]
    partial class MarketingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QtasMarketing.Core.News.Content", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<long>("GroupId");

                    b.Property<bool>("Hidden");

                    b.Property<int>("Hit");

                    b.Property<bool>("IsAllowedComment");

                    b.Property<string>("Lead");

                    b.Property<decimal>("Rate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("QtasMarketing.Core.News.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsPrivate");

                    b.Property<int>("Priority");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("QtasMarketing.Core.News.Content", b =>
                {
                    b.HasOne("QtasMarketing.Core.News.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
