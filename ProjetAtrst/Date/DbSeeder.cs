using Microsoft.AspNetCore.Identity;

namespace ProjetAtrst.Date
{
    public static class DbSeeder
    {
        public static async Task SeedSuperAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // 1️⃣ التأكد من وجود الدور "SuperAdmin"
            if (!await roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            // 2️⃣ التأكد من وجود الدور "Admin" (لأننا نستعمله لاحقًا)
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 3️⃣ إنشاء مستخدم SuperAdmin إذا غير موجود
            var superAdminEmail = "superadmin@atrst.com";
            var existingUser = await userManager.FindByEmailAsync(superAdminEmail);

            if (existingUser == null)
            {
                var superAdmin = new ApplicationUser
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    RoleType = RoleType.SuperAdmin,
                    EmailConfirmed = true
                };

                // كلمة سر افتراضية (تقدر تبدلها)
                var result = await userManager.CreateAsync(superAdmin, "SuperAdmin123@");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }
                else
                {
                    throw new Exception("فشل إنشاء SuperAdmin: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}