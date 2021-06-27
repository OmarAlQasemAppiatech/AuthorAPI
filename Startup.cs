using Author_API.Repositories;
using Author_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BussinessAccessLayer.Managers;
using Author_API.Middlewares;

namespace Author_API
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
            services.AddAutoMapper(typeof(Startup));
            _ = services.AddControllers(options =>
              {
                  options.SuppressAsyncSuffixInActionNames = false;
              });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Author_API", Version = "v1" });
            });
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IPublishersRepository, PublishersRepository>();
            services.AddScoped<AuthorManager, AuthorManager>();
            services.AddScoped<BookManager, BookManager>();
            services.AddScoped<PublisherManager, PublisherManager>();


            services.AddDbContext<AuthorsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthorsDbConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Author_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
