using BBQ.API.Middleware;
using BBQ.Application;
using BBQ.DataAccess;

namespace BBQ.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwagger();

        services.AddDataAccess(_configuration)
            .AddApplication(_env);

        services.AddJwt(_configuration);

        services.AddEmailConfiguration(_configuration);
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        app.UseCors(corsPolicyBuilder =>
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
        );

        app.UseSwagger();

        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BBQ V2"); });

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
