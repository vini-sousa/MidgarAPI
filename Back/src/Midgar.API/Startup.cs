using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Midgar.Application.Services;
using Midgar.Application.Interfaces;
using Midgar.Persistence.Repositories;
using Midgar.Persistence.Context;
using Midgar.Persistence.Interfaces;
using Microsoft.Extensions.FileProviders;

namespace Midgar.API
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
            services.AddDbContext<MidgarContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ILoteService, LoteService>();

            services.AddScoped<IGeneralRepository, GeneralRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();      

            services.AddCors();
 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Midgar.API", Version = "v1" });
            });
        }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Midgar.API v1"));
            }
 
            app.UseHttpsRedirection();
 
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(acess => acess.AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin()
            );

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}