using WK.UI.Aplicacao.Interfaces;
using WK.UI.Aplicacao.Services;

namespace WK.UI.Configuracao
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IWkClientService, WkClientService>();

        }
    }
}
