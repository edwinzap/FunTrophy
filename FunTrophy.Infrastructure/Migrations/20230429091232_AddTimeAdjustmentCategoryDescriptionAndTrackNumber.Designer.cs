﻿// <auto-generated />
using System;
using FunTrophy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunTrophy.Infrastructure.Migrations
{
    [DbContext(typeof(FunTrophyContext))]
    [Migration("20230429091232_AddTimeAdjustmentCategoryDescriptionAndTrackNumber")]
    partial class AddTimeAdjustmentCategoryDescriptionAndTrackNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RaceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ColorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RaceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("RaceId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TimeAdjustment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Seconds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TeamId");

                    b.ToTable("TimeAdjustments");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TimeAdjustmentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RaceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("TimeAdjustmentCategories");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RaceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TrackOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ColorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackOrders");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TrackTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TrackId", "TeamId")
                        .IsUnique();

                    b.ToTable("TrackTimes");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Color", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Team", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.Color", "Color")
                        .WithMany("Teams")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FunTrophy.Infrastructure.Model.Race", null)
                        .WithMany("Teams")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Color");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TimeAdjustment", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.TimeAdjustmentCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FunTrophy.Infrastructure.Model.Team", "Team")
                        .WithMany("TimeAdjustments")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TimeAdjustmentCategory", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Track", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.Race", "Race")
                        .WithMany("Tracks")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TrackOrder", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.Color", "Color")
                        .WithMany("TrackOrders")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FunTrophy.Infrastructure.Model.Track", "Track")
                        .WithMany("TrackOrders")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TrackTime", b =>
                {
                    b.HasOne("FunTrophy.Infrastructure.Model.Team", "Team")
                        .WithMany("TrackTimes")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FunTrophy.Infrastructure.Model.Track", "Track")
                        .WithMany("TrackTimes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Color", b =>
                {
                    b.Navigation("Teams");

                    b.Navigation("TrackOrders");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Race", b =>
                {
                    b.Navigation("Teams");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Team", b =>
                {
                    b.Navigation("TimeAdjustments");

                    b.Navigation("TrackTimes");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Track", b =>
                {
                    b.Navigation("TrackOrders");

                    b.Navigation("TrackTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
