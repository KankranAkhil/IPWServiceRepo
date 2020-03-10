using IPWService.IRepository;
using IPWService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IPWService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<Models.IPW_DevContext>(item => item.UseSqlServer(Configuration.GetConnectionString("IPWDBConnection")));
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IFunctionalityRepository, FunctionalityRepository>();
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
