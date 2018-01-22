using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(opts =>
            {
                opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Поле должно быть заполнено");
                opts.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(value => "Введите число через ,");
                //opts.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(value => "Введите число через .");
            });
            services.AddMemoryCache(); //Добавление хранилища данных в памяти
            services.AddSession(); //Регистрация службы, которая настраивает сеансы
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProductsConnection")));
            //Внедряем зависимость хранилища, теперь можно везде юзать в параметрах IProdcutRepository,
            //а реалзиация будет назначаться на класс FakeProductRepository, который потом можно удобно сменить
            //services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped(sp => SessionCart.GetCart(sp)); //Теперь для получения объекта SessionCart будет использоваться метод GetCart
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            

            //AddScoped() - эти службы создаются один раз за запрос.
            //AddTransient() - эти службы создаются каждый раз, когда они запрашиваются.
            //AddSingleton() - These services are created the first time they are requested and stay the same for subsequent requests.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            SeedData.Initialize(dbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages(); //Добавляет коды ответов в http response
            app.UseSession(); //Позволяет службе сеансов автоматически ассоциировать запросы с сеансами
            //app.UseMvcWithDefaultRoute(); //Стандартный роут в приложении MVC
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: null,
                //    template: "Adminka",
                //    defaults: new { Controller = "Admin", action = "Index" });

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", page = 1 });

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Product", action = "List", page = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");

                //routes.MapRoute(
                //    name: "pagination",
                //    template: "Products/Page{page}",
                //    defaults: new { Controller = "Product", action = "List" });

                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Product}/{action=List}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
