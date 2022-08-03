
using PubFinderGeneral.Data.Store.Extensions;

namespace PubFinderGeneral.Data.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            // Add services to the container.
            services.AddMvcCore();
            services.AddControllers();
            services.AddLogging();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.RegisterPubCSVFiles("leedsbeerquest.csv", Path.Combine(System.Environment.CurrentDirectory, @"DataFiles"));
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

        }

    }
    }
