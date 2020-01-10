using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Models
{
  public class ImageTag
  {
    public int Id { get; set; }
    public int ImageId { get; set; }
    [ForeignKey("ImageId")]
    public Image Image { get; set; }
    public string TagId { get; set; }
    [ForeignKey("TagId")]
    public Tag Tag { get; set; }

    public double Score { get; set; }
    public double MinX { get; set; }
    public double MinY { get; set; }
    public double MaxX { get; set; }
    public double MaxY { get; set; }
  }
}
