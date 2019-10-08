using Blazor.Extensions.Storage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using MovieQuestion.Client.Infrastructure;

namespace MovieQuestion.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ApiCommunicator>();
            services.AddSingleton<RatingManager>();
            services.AddSingleton<MovieManager>();
            services.AddStorage();
            services.AddBlazoredModal();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
