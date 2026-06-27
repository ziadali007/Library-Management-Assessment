
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistence;
using Presistence.Repositories;
using Services;
using Services.Abstractions;
using System.Text;

namespace Library_Management_Assessment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var identityConnectionString = builder.Configuration.GetConnectionString("IdentityConnection");
            builder.Services.AddDbContext<LibraryIdentityDbContext>(options =>
                                          options.UseSqlServer(identityConnectionString));
            builder.Services.AddIdentity<AppUser, AppRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<LibraryIdentityDbContext>()
            .AddDefaultTokenProviders();

            var businessConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                                          options.UseSqlServer(businessConnectionString));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; 
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                };
            });

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<ILibraryUnitOfWork, LibraryUnitOfWork>();
            builder.Services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // Execute the async method safely
                await IdentityDataSeeder.SeedPermanentAdminOnceAsync(services);
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
