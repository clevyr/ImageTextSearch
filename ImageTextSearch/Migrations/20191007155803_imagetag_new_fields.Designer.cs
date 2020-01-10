﻿// <auto-generated />
using ImageTextSearch.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageTextSearch.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191007155803_imagetag_new_fields")]
    partial class imagetag_new_fields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImageTextSearch.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilePath");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ImageTextSearch.Models.ImageTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId");

                    b.Property<double>("MaxX");

                    b.Property<double>("MaxY");

                    b.Property<double>("MinX");

                    b.Property<double>("MinY");

                    b.Property<double>("Score");

                    b.Property<string>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("TagId");

                    b.ToTable("ImageTag");
                });

            modelBuilder.Entity("ImageTextSearch.Models.Tag", b =>
                {
                    b.Property<string>("Text")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Text");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ImageTextSearch.Models.ImageTag", b =>
                {
                    b.HasOne("ImageTextSearch.Models.Image", "Image")
                        .WithMany("ImageTags")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ImageTextSearch.Models.Tag", "Tag")
                        .WithMany("ImageTags")
                        .HasForeignKey("TagId");
                });
#pragma warning restore 612, 618
        }
    }
}
