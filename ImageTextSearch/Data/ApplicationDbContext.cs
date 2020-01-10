using ImageTextSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext() : base() { }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Image> Images { get; set; }
    public DbSet<Tag> Tags { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<ImageTag>()
      //    .HasKey(x => new { x.ImageId, x.TagId });
      modelBuilder.Entity<ImageTag>()
          .HasOne(x => x.Image)
          .WithMany(b => b.ImageTags)
          .HasForeignKey(x => x.ImageId);
      modelBuilder.Entity<ImageTag>()
          .HasOne(x => x.Tag)
          .WithMany(c => c.ImageTags)
          .HasForeignKey(x => x.TagId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        DotNetEnv.Env.Load();
        var configuration = new ConfigurationBuilder()
           .AddEnvironmentVariables()
           .Build();
        var connectionString = configuration["CONNECTION_STRING"];
        optionsBuilder.UseSqlServer(connectionString);
      }
    }


  }
}

