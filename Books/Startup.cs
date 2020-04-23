using Books.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Books {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddSwaggerGen (options => {
                options.SwaggerDoc ("v1", new Info { Version = "v1", Title = ".Net Core 2.2 and MongoDB" });
            });
            services.AddScoped<BookService> ();
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseSwagger ();
            app.UseSwaggerUI (options => {
                options.SwaggerEndpoint ("/swagger/v1/swagger.json", "API V1");
            });

            var rewrite = new RewriteOptions ();
            rewrite.AddRedirect ("^$", "swagger");

            app.UseCors (
                options => options
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ()
                .AllowCredentials ()
                //.WithOrigins("www.site.com")
            );
            app.UseRewriter (rewrite);
            app.UseHttpsRedirection ();
            app.UseAuthentication ();
            app.UseMvc ();
        }
    }
}