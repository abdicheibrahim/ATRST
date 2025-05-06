using MySqlConnector;

namespace ProjetAtrst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            //    ?? throw new InvalidOperationException("No connection string was found");
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MySqlConnection")
                ?? throw new InvalidOperationException("No connection string was found");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString))
            );

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
