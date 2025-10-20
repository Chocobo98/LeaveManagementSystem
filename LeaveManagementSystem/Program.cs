using LeaveManagementSystem.Application;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//EDIT: AddAutoMapper,D.I services and ApplicationDBContext service has moved to Application Layer as ApplicationServicesRegistration
/*builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());*/ //Bypass for profile (If we have a bunch of differents profile, we must add for each one)

//Ahora solo el Program se dedica a agregar el builder hacia la capa de Aplicacion
DataServicesRegistration.AddDataServices(builder.Services, builder.Configuration);
ApplicationServicesRegistration.AddApplicationServices(builder.Services);

// Serilog Integration
builder.Host.UseSerilog((ctx, config) =>
    config.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration)
);

//Authorization Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminSupervisorOnly", policy =>
    {
        policy.RequireRole(Roles.Administrator, Roles.Supervisor);
    });
});

builder.Services.AddHttpContextAccessor(); //HTTPContext Service


//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//Ahora indica que la nueva tabla default de usuario, es la ApplicationUser (Codigo de arriba era el antiguo)
builder.Services.AddDefaultIdentity<ApplicationUser>
(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
})
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
