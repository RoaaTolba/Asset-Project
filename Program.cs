using AssetsPro.Filters;
using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Repos;
using AssetsPro.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAutorizationHandler>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IEmpRepo, EmpRepo>();
builder.Services.AddScoped<IEmpService, EmpService>();
builder.Services.AddScoped<IGenderRepo, GenderRepo>();
builder.Services.AddScoped<IAttendanceRepo, AttendanceRepo>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupRepo,GroupRepo>();
builder.Services.AddScoped<IGroupService,GroupService>();
builder.Services.AddScoped<MyDbContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "~/Account/Login";
        option.ExpireTimeSpan = TimeSpan.FromDays(20);
    });
builder.Services.Configure<SecurityStampValidatorOptions>
    (options =>
    {
        options.ValidationInterval = TimeSpan.Zero;
    });

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Account}/{action=Login}/{id?}");


///befor app.Run();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    var logger = loggerFactory.CreateLogger("app");

//    try
//    {
//        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//        // Ensure the role exists
//        if (!await roleManager.RoleExistsAsync("Basic"))
//        {
//            var roleResult = await roleManager.CreateAsync(new IdentityRole("Basic"));
//            if (roleResult.Succeeded)
//            {
//                logger.LogInformation("Role 'Basic' created successfully.");
//            }
//            else
//            {
//                logger.LogError("Failed to create role 'Basic'.");
//                foreach (var error in roleResult.Errors)
//                {
//                    logger.LogError(error.Description);
//                }
//            }
//        }

//        // Find the user by username (e.g., "roaa")
//        var user = await userManager.FindByNameAsync("sherif");
//        if (user == null)
//        {
//            logger.LogWarning("User 'sherif' not found in the database.");
//        }
//        else
//        {
//            // Check if the user already has the role
//            if (!await userManager.IsInRoleAsync(user, "Basic"))
//            {
//                // Assign the role to the user
//                var addToRoleResult = await userManager.AddToRoleAsync(user, "Basic");
//                if (addToRoleResult.Succeeded)
//                {
//                    logger.LogInformation("Role 'Basic' assigned to user 'sherif'.");
//                }
//                else
//                {
//                    logger.LogError("Failed to assign role 'Basic' to user 'sherif'.");
//                    foreach (var error in addToRoleResult.Errors)
//                    {
//                        logger.LogError(error.Description);
//                    }
//                }
//            }
//            else
//            {
//                logger.LogInformation("User 'sherif' already has the 'Basic' role.");
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "An error occurred while setting up roles or users.");
//    }
//}
