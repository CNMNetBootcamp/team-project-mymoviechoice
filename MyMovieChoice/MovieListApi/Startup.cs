using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMovieChoice.Data;
using MyMovieChoice.Models;

namespace MovieListApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.C:\Users\Owner\Source\Repos\team-project-mymoviechoice\MyMovieChoice\MovieListApi\Startup.cs
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
      services.AddDbContext<MovieListContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MovieListConnection")));
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }
  }
}
