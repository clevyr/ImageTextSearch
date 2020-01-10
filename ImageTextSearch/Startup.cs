using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ImageTextSearch.Data;
using ImageTextSearch.Models;
using Microsoft.EntityFrameworkCore;
using Blazor.FileReader;
using ImageTaggerClient;
using Blazored.Modal;
using BlazorModal;
namespace ImageTextSearch
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      DotNetEnv.Env.Load();
      var builder = new ConfigurationBuilder()
               .AddEnvironmentVariables();
      configuration = builder.Build();
      Console.WriteLine(configuration);
      Configuration = configuration;

    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration["CONNECTION_STRING"]));

      services.AddRazorPages();
      services.AddServerSideBlazor();
 
      services.AddSingleton(new ImageTaggerService());
      services.AddScoped<TagService>();
      services.AddScoped<BaseService<ImageTag>>();
      services.AddScoped<ImageService>();
      services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
      services.AddBlazoredModal(); 

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();
      //app.UseStaticFiles(new StaticFileOptions()
      //{
      //  FileProvider = new PhysicalFileProvider(
      //                      Path.Combine(Directory.GetCurrentDirectory(), @"UploadedImages"))
      ////  RequestPath = new PathString("/UploadedImages")
      //});
      //app.UseHttpsRedirection();
      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
