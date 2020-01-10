using ImageTextSearch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Data
{

  public class TagService : BaseService<Tag>
  {
    public TagService(ApplicationDbContext context) : base(context)
    {

    }
    public override List<Tag> GetAll()
    {
      var test = Context.Tags.Include(x => x.ImageTags).ToList();
      return test;
    }
  }
}
