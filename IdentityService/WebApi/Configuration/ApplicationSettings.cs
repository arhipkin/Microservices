using Domain.Settings;
using WebApi.Settings;

namespace WebApi.Configuration
{
    public static class ApplicationSettings
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection(nameof(TokenSettings)));
        }
    }
}
