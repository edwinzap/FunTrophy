﻿// <auto-generated />
using System;
using FunTrophy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunTrophy.Infrastructure.Migrations
{
    [DbContext(typeof(FunTrophyContext))]
    partial class FunTrophyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int?>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("RaceId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TimeAdjustment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Seconds")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TeamId");

                    b.ToTable("TimeAdjustments");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TimeAdjustmentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("TimeAdjustmentCategories");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TrackOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackOrders");
                });

            modelBuilder.Entity("FunTrophy.Infrastructure.Model.TrackTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackTimes");
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
