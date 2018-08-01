﻿// <auto-generated />
using System;
using BreadBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BreadBuilder.Migrations
{
    [DbContext(typeof(BreadDbContext))]
    partial class BreadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BreadBuilder.Models.Bread", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Instructions");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Breads");
                });

            modelBuilder.Entity("BreadBuilder.Models.Ingredient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BreadID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("BreadID");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("BreadBuilder.Models.Measurement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeasurementValue");

                    b.HasKey("ID");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("BreadBuilder.Models.Ingredient", b =>
                {
                    b.HasOne("BreadBuilder.Models.Bread")
                        .WithMany("breadIngredients")
                        .HasForeignKey("BreadID");
                });
#pragma warning restore 612, 618
        }
    }
}
