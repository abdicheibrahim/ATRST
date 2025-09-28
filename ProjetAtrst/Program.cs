using Microsoft.AspNetCore.Identity;
using ProjetAtrst.Date;
using ProjetAtrst.Repositories;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Services;
using ProjetAtrst.Helpers;
namespace ProjetAtrst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("No connection string was found");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("MySqlConnection")
            //    ?? throw new InvalidOperationException("No connection string was found");
            // builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            // );

            //var connectionString = builder.Configuration.GetConnectionString("PostgresConnection")
            //    ?? throw new InvalidOperationException("No connection string was found");

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(connectionString)   // ? Postgres
            //);


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                
                //LockOut Settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //user settings
                options.User.RequireUniqueEmail = true;

                // signIn Settin
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IResearcherService, ResearcherService>();
            builder.Services.AddScoped<IResearcherRepository, ResearcherRepository>();
            builder.Services.AddScoped<IPartnerService, PartnerService>();
            builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
            builder.Services.AddScoped<IAssociateRepository, AssociateRepository>();
            builder.Services.AddScoped<IAssociateService, AssociateService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectMembershipRepository, ProjectMembershipRepository>();
            builder.Services.AddScoped<IProjectRequestService, ProjectRequestService>();
            builder.Services.AddScoped<IProjectRequestRepository, ProjectRequestRepository>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ProfileCompletionFilter>();
            builder.Services.AddScoped<AuthorizeProjectLeaderAttribute>();
            builder.Services.AddSingleton<StaticDataLoader>();
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                
                DbSeeder.SeedSuperAdminAsync(userManager, roleManager).GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}");

           
            app.Run();
        }
    }
}
