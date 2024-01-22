using System.Threading.Tasks;
using APITubefetch.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace APITubefetch
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Injeção de dependência
            //Adicionando Controllers 
            services.AddControllers();

            //Adicionando Databases
            services.AddDbContext<AppDbContext>();

            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.WithOrigins("http://127.0.0.1:4200",
                    "http://localhost:19006")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TubeFetch API",
                    Version = "v1",
                    Description = "Projeto para mostrar minhas habilidades na programação"
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            // Middleware do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TubeFetch API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                // Rota padrão direciona para o Swagger
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });

                //Mapeando rotas iniciais
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
