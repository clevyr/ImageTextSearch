using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Models
{
  
  public class Image
  {
    public Image()
    {
      //ImageTags = new HashSet<ImageTag>();
    }
    public int Id { get; set; }
    public string FilePath { get; set; }
    public ICollection<ImageTag> ImageTags { get; set; }
  }
}
