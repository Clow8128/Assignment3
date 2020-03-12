//Cameron Low
//Distributed Applications
//Assignment 2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RESTAPI.Dtos;
using RESTAPI.Entities;
using RESTAPI.Middleware;
using RESTAPI.Repositories;

namespace RESTAPI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddOptions();

            service.AddDbContext<SongContext>(options => options.UseSqlServer
            (
                Configuration.GetConnectionString("DefaultConnection"))
            );

            service.AddScoped<IReviewRepository, ReviewRepository>();
            service.AddScoped<IAccountRepository, AccountRepository>();
            service.AddScoped<ISongRepository, SongRepository>();

            service.AddMvc();
        }

        public void Configure(IApplicationBuilder application, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseDefaultFiles();
            application.UseStaticFiles();
            application.UseCustomMiddleware();

            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Song, SongCreateDto>().ReverseMap();
                mapper.CreateMap<Account, AccountDto>().ReverseMap();
                mapper.CreateMap<Song, SongUpdateDto>().ReverseMap();
                mapper.CreateMap<Song, SongDto>().ReverseMap();
                mapper.CreateMap<Review, ReviewDto>().ReverseMap();
                mapper.CreateMap<Account, AccountCreateDto>().ReverseMap();
                mapper.CreateMap<Review, ReviewCreateDto>().ReverseMap();
                mapper.CreateMap<Review, ReviewUpdateDto>().ReverseMap();
            });

            application.UseMvc();
        }
    }
}
