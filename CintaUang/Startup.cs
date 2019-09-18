using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;
using Repository.Repositories.CategoryRepositories;
using Repository.Repositories.ExpenseRepositories;
using Repository.Repositories.SubCategoryRepositories;
using Repository.Repositories.UserRepositories;
using Service.Modules;

namespace CintaUang
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
			// Internal Repo & Service Registration
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<DbContextFactory>();
			services.AddTransient<IStoredProcedureBuilder, PGStoredProcedureBuilder>();
            services.AddTransient<DbUtil>();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<CintaUangDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("CintaUangDbConnection"));
                }, ServiceLifetime.Scoped, ServiceLifetime.Singleton);
            services.AddTransient<CintaUangDbContext>();
            services.AddTransient<UnitOfWork>();

			/**
			 * Repositories
			 */
            services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

			services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
			services.AddTransient<ISubCategoryService, SubCategoryService>();

			services.AddTransient<IExpenseService, ExpenseService>();
			services.AddTransient<IExpenseDatatableRepository, ExpenseDatatableRepository>();

            services.AddMvc();
			services.AddSession(options =>
			{
				options.Cookie.Name = ".CintaUang.Session";
				options.IdleTimeout = TimeSpan.FromMinutes(60);
				options.Cookie.IsEssential = true;
			});
		}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
			app.UseSession();
			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Auth}/{action=Index}/{id?}"
                );
            });

            app.Run(async (context) =>
            {   
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
