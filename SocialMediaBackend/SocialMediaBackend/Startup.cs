using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaBackend.Identity;
using SocialMediaBackend.Repository;
using SocialMediaBackend.Repository.IRepository;
using SocialMediaBackend.Service;
using SocialMediaBackend.Services_Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaBackend
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Constr"),b=>b.MigrationsAssembly("SocialMediaBackend")));

            services.AddTransient<IRoleStore<ApplicationRoles>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSigninManager>();
            services.AddTransient<RoleManager<ApplicationRoles>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();

            services.AddIdentity<ApplicationUser, ApplicationRoles>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddUserStore<ApplicationUserStore>()
     .AddUserManager<ApplicationUserManager>()
     .AddRoleManager<ApplicationRoleManager>()
     .AddSignInManager<ApplicationSigninManager>()
     .AddRoleStore<ApplicationRoleStore>()
     .AddDefaultTokenProviders();

            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();
            //services.AddScoped<IUserService, UserService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeUnlikeRepository, LikeUnlikeRepository>();
            services.AddScoped<IFollowRepository, FollowRepostory>();


            
            // add cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200/").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // jwt configuration
            var appsettingSection = Configuration.GetSection("AppSettingJWT");
            services.Configure<AppSettingJWT>(appsettingSection);
            var appsetting = appsettingSection.Get<AppSettingJWT>();
            var key = Encoding.ASCII.GetBytes(appsetting.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
         
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialMediaBackend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMediaBackend v1"));
            }
            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            // create by default user using coding
           /* IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using(IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRoles>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if(!await roleManager.RoleExistsAsync(SD.roleAdmin))
                {
                    var role = new ApplicationRoles();
                    role.Name = SD.roleAdmin;
                    await roleManager.CreateAsync(role);
                }
                if(!await roleManager.RoleExistsAsync(SD.roleUser))
                {
                    var role = new ApplicationRoles();
                    role.Name = SD.roleUser;
                    await roleManager.CreateAsync(role);
                }
                // now create the admin user only through code
                if(await userManager.FindByNameAsync(SD.roleAdmin) == null)
                {
                    // create user and assingn the role admin
                    var user = new ApplicationUser();
                    user.UserName = "admin";
                    user.Email = "admin@gmail.com";
                    var password = "Admin@123";
                    var createUser = await userManager.CreateAsync(user, password);
                    if (createUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, SD.roleAdmin);
                    }
                }
            }*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
