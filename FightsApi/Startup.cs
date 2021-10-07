using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FightsApi_DbContext;
using FightsApi_Models.ViewModels;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Buisiness.Repositiories;
using FightsApi_Buisiness.Repositories;
using FightsApi_Logic.Mappers;
using FightsApi_Buisiness.Mappers;

namespace FightsApi
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
      services.AddCors((options) =>
      {
        options.AddPolicy(name: "NotFightClubLocal", builder =>
        {
          builder.WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod();
        });
      });
      //services.AddDbContext<ConfigurationContext>(options =>
      //{
      //    options.UseSqlServer(Configuration.GetConnectionString("local"));
      //});
      //services.AddDbContext<P2_NotFightClubContext>(options =>
      //{
      //    //if db options is already configured, done do anything..
      //    // otherwise use the Connection string I have in secrets.json
      //    if (!options.IsConfigured)
      //    {
      //        options.UseSqlServer(Configuration.GetConnectionString("local"));
      //    }
      //});
      services.AddDbContext<P3_NotFightClubContext>();


      //services.AddSingleton<IRepository<ViewUserInfo, string>, UserRepository>();
      //services.AddSingleton<IMapper<UserInfo, ViewUserInfo>, UserInfoMapper>();
      services.AddScoped<IRepository<ViewCharacter, int>, CharacterRepository>();
      //services.AddSingleton<IMapper<Character, ViewCharacter>, CharacterMapper>();
      //services.AddSingleton<IMapper<Trait, ViewTrait>, TraitMapper>();
      //services.AddSingleton<IRepository<ViewTrait, int>, TraitRepository>();
      //services.AddSingleton<IRepository<ViewWeapon, int>, WeaponRepository>();
      //services.AddSingleton<IMapper<Weapon, ViewWeapon>, WeaponMapper>();
      services.AddScoped<IFightRepository, FightRepository>();
      services.AddScoped<IMapper<Fighter, ViewFighter>, FighterMapper>();
      services.AddScoped<IRepository<ViewFighter, int>, FighterRepository>();
      services.AddScoped<IMapper<Fight, ViewFight>, FightMapper>();



      services.AddControllers();
     // services.AddControllers().AddNewtonsoftJson(options =>
      //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotFightClub_WebAPI", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
     // loggerFactory.AddFile("Logs/app-{Date}.txt");

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotFightClub_WebAPI v1"));
      }



      app.UseCors("NotFightClubLocal");

      app.UseDefaultFiles();
      app.UseStaticFiles();

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
