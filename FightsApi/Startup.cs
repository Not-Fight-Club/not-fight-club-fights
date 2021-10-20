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
using FightsApi_Data;
using FightsApi_Models.ViewModels;
using FightsApi_Buisiness.Interfaces;
using FightsApi_Buisiness.Repositiories;
using FightsApi_Buisiness.Repositories;
using FightsApi_Logic.Mappers;
using FightsApi_Buisiness.Mappers;
using System.Net.Http;
using Microsoft.Extensions.Options;

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
        options.AddPolicy(name: "FightsApiLocal", builder =>
        {
          builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod();
        });
      });
      //services.AddDbContext<ConfigurationContext>(options =>
      //{
      //    options.UseSqlServer(Configuration.GetConnectionString("local"));
      //});
      services.AddDbContext<P3_NotFightClubContext>(options =>
      {
        //if db options is already configured, done do anything..
        // otherwise use the Connection string I have in secrets.json
        if (!options.IsConfigured)
        {
          options.UseSqlServer(Configuration.GetConnectionString("FightsDB"));
        }
      });
      //services.AddDbContext<P3_NotFightClubContext>();


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
      //services.AddScoped<IRepository<ViewVote, int>, VoteRepository>();
      services.AddScoped<IMapper<Vote, ViewVote>, VoteMapper>();
      services.AddScoped<IVoteRepository, VoteRepository>();

      //services.AddScoped<IRepository<ViewWeather, int>, WeatherRepository>();
      services.AddScoped<IRepository<ViewLocation, string>, LocationRepository>();
      services.AddScoped<IMapper<Weather, ViewWeather>, WeatherMapper>();
      services.AddScoped<IMapper<Location, ViewLocation>, LocationMapper>();

      services.AddScoped<CharacterFightMapper, CharacterFightMapper>();
      services.AddScoped<CharacterRepository, CharacterRepository>();
      services.AddScoped<IWeatherRepository, WeatherRepository>();
      //services.AddHttpClient();
      // Note: The below code will bypass SSL Certificate checking. This is very insecure and I'm
      //    only using it to get my localhost domains to work properly. This CANNOT make it to production
      //      - Jon B
      // (taken from https://stackoverflow.com/questions/62860290/disable-ssl-certificate-verification-in-default-injected-ihttpclientfactory)
      services.AddHttpClient(Options.DefaultName, configure =>
      {
        //configure.BaseAddress = new Uri(Configuration["CharactersApiURL"]);
      }).ConfigurePrimaryHttpMessageHandler(() =>
      {
        return new HttpClientHandler
        {
          ClientCertificateOptions = ClientCertificateOption.Manual,
          ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true
        };
      });



      services.AddControllers();
     // services.AddControllers().AddNewtonsoftJson(options =>
      //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FightApi", Version = "v1" });
      });
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
     // loggerFactory.AddFile("Logs/app-{Date}.txt");

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FightApi v1"));
      }



    


      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("FightsApiLocal");
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
