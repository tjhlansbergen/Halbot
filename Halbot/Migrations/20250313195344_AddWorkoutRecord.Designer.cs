﻿// <auto-generated />
using System;
using Halbot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Halbot.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250313195344_AddWorkoutRecord")]
    partial class AddWorkoutRecord
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("Halbot.Data.Records.ActivityRecord", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DataType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gpx")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRace")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerializedData")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ActivityRecords");
                });

            modelBuilder.Entity("Halbot.Data.Records.LogRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<int>("Severity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("LogRecords");
                });

            modelBuilder.Entity("Halbot.Data.Records.WorkoutRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Minutes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WorkoutRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
