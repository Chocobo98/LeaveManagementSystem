using LeaveManagementSystem.Application.Services.Email;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LeaveManagementSystem.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Dependency Injection from Services
            services.AddScoped<ILeaveTypeServices, LeaveTypeServices>(); //Usable on other classes to inject it on differents places
            services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
            services.AddScoped<IPeriodsService, PeriodsService>();
            services.AddScoped<ILeaveRequestsService, LeaveRequestsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IEmailSender, EmailSender>(); //New Client, new instance everytime email should be dispase
            return services;
        }
    }
}
