using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using MovieQuestion.Server.Mongo;
using MovieQuestion.Shared.Models;
using Newtonsoft.Json;

namespace MovieQuestion.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.EnableEndpointRouting = false; }).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddSingleton(new MongoContext("mongodb://localhost:27017/MovieQuestion"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IRepository<Movie> moviesRepository)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }


#if !DEBUG
            app.UsePathBase("/movies");
            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}"); });
            app.Map("/movies", builder => { builder.UseClientSideBlazorFiles<Client.Startup>(); });
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "",
                FileProvider = new PhysicalFileProvider("/root/movies/MovieQuestion.Client/dist"),
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings =
                    {
                        {".dll", "application/octet-stream" }
                    }
                }
            });
#else
            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}"); });
            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();
            app.UseStaticFiles();
#endif



            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });

            //await moviesRepository.DropAsync();
            //await moviesRepository.AddManyAsync(
            //    JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText("Assets/movies.json")));
        }

    }
}
