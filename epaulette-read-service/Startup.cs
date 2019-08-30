using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using epaulette_data.epaulette_interface;
using epaulette_data.epaulette_json;

namespace epaulette_read_service
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Initialize configuration
      services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

      // Initialize dependency injection
      services.AddSingleton<IEpauletteGettor, EpauletteJsonGettor>();

      services.AddControllers();

      services.AddCors(c =>  
      {  
        c.AddPolicy("AllowOrigin", options =>
          options.WithOrigins("http://localhost:7777").AllowAnyHeader().AllowAnyMethod());  
      });  
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(options=>options.WithOrigins("http://localhost:7777").AllowAnyHeader().AllowAnyMethod()); 

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
