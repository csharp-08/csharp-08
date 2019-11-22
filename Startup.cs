using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace csharp_08
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // create "CorsPolicy" options in development to accept other origin clients
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            }));

            // Use signalR library for websockets
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
                app.UseDeveloperExceptionPage();
            }

            // Add signalR hub
            app.UseSignalR(routes =>
            {
                routes.MapHub<ConnectionHub>("/ws-server");
            });

            // Add static folder '/wwwroot' to serve the client
            app.UseFileServer();


            app.Run(async (context) =>
            {
            });
        }
    }
}