namespace WebAPI.Extensions;

public static class CorsPolicyExtensions
{
    public static void ConfigureCorsPolicy(this IServiceCollection service)
    {
        service.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }
}