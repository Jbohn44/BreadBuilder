﻿// <auto-generated />
using System;
using BreadBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BreadBuilder.Migrations
{
    [DbContext(typeof(BreadDbContext))]
    [Migration("20180808133005_AddRecipeItem")]
    partial class AddRecipeItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name");

                    b.HasKey("ID");

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

            modelBuilder.Entity("BreadBuilder.Models.RecipeItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BreadID");

                    b.Property<int?>("RecipeIngredientID");

                    b.Property<int?>("RecipeMeasurementID");

                    b.HasKey("ID");

                    b.HasIndex("BreadID");

                    b.HasIndex("RecipeIngredientID");

                    b.HasIndex("RecipeMeasurementID");

                    b.ToTable("RecipeItems");
                });

            modelBuilder.Entity("BreadBuilder.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BreadBuilder.Models.RecipeItem", b =>
                {
                    b.HasOne("BreadBuilder.Models.Bread")
                        .WithMany("BreadIngredients")
                        .HasForeignKey("BreadID");

                    b.HasOne("BreadBuilder.Models.Ingredient", "RecipeIngredient")
                        .WithMany()
                        .HasForeignKey("RecipeIngredientID");

                    b.HasOne("BreadBuilder.Models.Measurement", "RecipeMeasurement")
                        .WithMany()
                        .HasForeignKey("RecipeMeasurementID");
                });
#pragma warning restore 612, 618
        }
    }
}
