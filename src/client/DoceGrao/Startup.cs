
using DoceGrao.Api.Infrastructure.DependencyInjection;
using DoceGrao.Api.Services.DependencyInjection;
using DoceGrao.Database.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DoceGrao.Api.Client
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
            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddConnectionProvider();

            services
                .AddDomainRepositories()
                .AddDomainDependencies()
                .AddDataBaseContext((connectionProvider) => connectionProvider.GetConnectionString("DefaultConnection"));

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Doce Gão API", Version = "v1"});
                });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal de Iscas API");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseCors(
                options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
