using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace ImageTaggerClient
{
  public class ImageTaggerService : ImageTagger.ImageTaggerClient
  {
    private readonly ILogger<ImageTaggerService> _logger;
    public ImageTaggerService(ILogger<ImageTaggerService> logger)
    {
      _logger = logger;
    }
    public ImageTaggerService(ChannelBase channel) : base(channel) { }

    public ImageTaggerService() : base(new Channel("127.0.0.1:5001", ChannelCredentials.Insecure))
    {
      Console.WriteLine("OH YEAH!");
    }

    public override TagImageReply TagImage(TagImageRequest request, CallOptions options)
    {
      Console.WriteLine("TAG  IMAGE");
      return base.TagImage(request, options);
    }
  }
}
