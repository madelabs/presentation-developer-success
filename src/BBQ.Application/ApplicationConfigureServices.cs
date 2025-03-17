using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BBQ.Application.Common.Email;
using BBQ.Application.MappingProfiles;
using BBQ.Application.Services;
using BBQ.Application.Validators;
using BBQ.DataAccess.Services;

namespace BBQ.Application;

public static class ApplicationConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddValidatorsFromAssemblyContaining<IValidationsMarker>();
        services.AddScoped<IBbqSessionService, BbqSessionService>();
        services.AddScoped<ISessionNoteService, SessionNoteService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<ITemplateService, TemplateService>();
     

        if (env.IsDevelopment())
            services.AddScoped<IEmailService, DevEmailService>();
        else
            services.AddScoped<IEmailService, EmailService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }

    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("SmtpSettings").Get<SmtpSettings>());
    }
}
