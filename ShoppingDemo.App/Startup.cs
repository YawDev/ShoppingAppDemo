using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingDemo.App;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Data.Repositories;
using ShoppingDemo.App.Services;
using ShoppingDemo.EFCore;

namespace ShoppingAppDemo
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
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddMvc(x => x.EnableEndpointRouting = false);
           services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("localhost")));
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            opt.User.RequireUniqueEmail = true
            ).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(
                opt => opt.AccessDeniedPath = new PathString("/Identity/AccessDenied")
                
                ).ConfigureApplicationCookie(opt => opt.LoginPath = new PathString("/Identity/Login"));

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromMinutes(30));

            services.AddControllersWithViews();
            services.AddRazorPages();

            AddRepositories(services);
            AddServices(services);

        }


        public void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }

        public void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserDetailsComposition, UserDetailsComposition>();
            services.AddScoped<ICustomerOrderService, CustomerOrderService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IEmailService, EmailService>();


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

       
    }
    
}