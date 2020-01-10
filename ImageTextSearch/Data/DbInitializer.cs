using ImageTextSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Data
{
  public static class DbInitializer
  {
    public static void Initialize(ApplicationDbContext context)
    {
      context.Database.EnsureCreated();
    }
  }
}
