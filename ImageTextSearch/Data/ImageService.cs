using ImageTaggerClient;
using ImageTextSearch.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Data
{
  public class ImageService : BaseService<Image>
  {
    protected readonly IHostingEnvironment Env;
    protected readonly ImageTaggerService ImageTagger;
    protected readonly BaseService<ImageTag> ImageTagService;
    public ImageService(ApplicationDbContext context,
      ImageTaggerService imageTagger,
      BaseService<ImageTag> imageTagService,
      IHostingEnvironment env) : base(context)
    {
      Env = env;
      ImageTagger = imageTagger;
      ImageTagService = imageTagService;
    }

    public override async Task<List<Image>> GetAllAsync(int? skip = null, int? take = null)
    {
      IQueryable<Image> query = Context.Images
       .Include(x => x.ImageTags).OrderByDescending(x=> x.Id);

      if (skip.HasValue)
        query = query.Skip(skip.Value);
      if (take.HasValue)
        query = query.Take(take.Value);

      return await query.ToListAsync();
 
    }

    public async Task<IEnumerable<Image>> SearchAsync(string text, int? skip = null, int? take = null)
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        return await GetAllAsync(skip, take);
      }
      IQueryable<Image> query = Context.Images
        .Include(x => x.ImageTags)
        .Where(x => x.ImageTags.Any(y => y.Tag.Text == text))
        .OrderByDescending(x => x.Id);

      if (skip.HasValue)
        query = query.Skip(skip.Value);
      if (take.HasValue)
        query = query.Take(take.Value);

      return await query.ToListAsync();
    }


    public async Task<(Image, string[])> CreateFromStreamAsync(Stream stream, string fileName)
    {
      var dbPath = Path.Combine("UploadedImages", Guid.NewGuid() + Path.GetExtension(fileName));
      var filePath = Path.Combine(Env.WebRootPath, dbPath);

      try
      {
        var tags = new List<Tag>();
        using (var uploadFile = File.Create(filePath))
        {
          var bufferSize = 4096;
          var buffer = new byte[bufferSize];
          int count;
          while ((count = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
          {
            uploadFile.Write(buffer, 0, bufferSize);
          }
          uploadFile.Flush();
          uploadFile.Close();
          //var bytes = new byte[uploadFile.Length];
          //uploadFile.Read(bytes, 0, bytes.Length);
          //var byteString = Google.Protobuf.ByteString.CopyFrom(bytes);
          var response = ImageTagger.TagImage(new TagImageRequest { Filepath = filePath });
          var image = await this.CreateAsync(new Image
          {
            FilePath = dbPath,
          });
          foreach (var item in response.Items)
          {
            var tag = Context.Tags.Find(item.Tag);
            if (tag == null)
            {
              tag = new Tag { Text = item.Tag };
              Context.Tags.Add(tag);
            }
            await ImageTagService.CreateAsync(new ImageTag
            {
              Tag = tag,
              Image = image,
              Score = item.Score,
              MinX = item.BoundingBox.MinX,
              MinY = item.BoundingBox.MinY,
              MaxX = item.BoundingBox.MaxX,
              MaxY = item.BoundingBox.MaxY,
            });
          }

          await Context.SaveChangesAsync();
          return (image,response.Items.Select( x => x.Tag).ToArray());
        }

      }
      catch (Exception e)
      {
        return (null,null);
      }

    }

   
  }
}
