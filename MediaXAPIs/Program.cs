
using MediaXAPIs.Data;
using MediaXAPIs.Services;
using MediaXAPIs.Services.User_;
using Microsoft.EntityFrameworkCore;

namespace MediaXAPIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.
            //builder.Services.AddControllers().AddJsonOptions(options => {
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //});

            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IUserService, UserService>();
            //builder.Services.AddTransient<IProductService, ProductService>();
            //builder.Services.AddDbContext<MediaXDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<MediaXDBContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnString"), new MySqlServerVersion(new Version(8, 0, 2))));

            //builder.Services.AddDistributedMySqlCache(options =>
            //{
            //    options.ConnectionString = builder.Configuration.GetConnectionString("MySqlConnString");
            //    options.TableName = "Sessions";
            //    //options.SchemaName = "dbo";
            //});

            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true; // Required for GDPR
            //});

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //app.UseSession();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
