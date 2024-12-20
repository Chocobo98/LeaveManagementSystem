using LeaveManagementSystem.Services.Email;
using LeaveManagementSystem.Services.LeaveAllocations;
using LeaveManagementSystem.Services.LeaveTypes;
using LeaveManagementSystem.Services.Periods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Bypass for profile (If we have a bunch of differents profile, we must add for each one)
builder.Services.AddScoped<ILeaveTypeServices, LeaveTypeServices>(); //Usable on other classes to inject it on differents places
builder.Services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
builder.Services.AddScoped<IPeriodsService, PeriodsService>();
builder.Services.AddTransient<IEmailSender, EmailSender>(); //New Client, new instance everytime email should be dispase


//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//Ahora indica que la nueva tabla default de usuario, es la ApplicationUser (Codigo de arriba era el antiguo)
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() //Identificar los roles referentes al ApplicationUser
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
