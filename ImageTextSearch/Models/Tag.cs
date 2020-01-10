using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Models
{
  public class Tag
  {
    public Tag()
    {
      ImageTags = new List<ImageTag>();
    }
    [Key]
    public string Text { get; set; }

    public ICollection<ImageTag> ImageTags { get; set; }
  }
}
