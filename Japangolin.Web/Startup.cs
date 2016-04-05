namespace Wacton.Japangolin.Web
{
    using System.Linq;
    using System.Text;

    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using React;
    using React.AspNet;

    using Wacton.Japangolin.Domain.JapanesePhrases;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(this.Configuration);

            // see http://reactjs.net/getting-started/aspnet5.html
            services.AddReact(); 
            services.AddMvc();

            services.AddSingleton<IJapanesePhraseRepository, JapanesePhraseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            // see http://reactjs.net/getting-started/aspnet5.html
            app.UseReact(config =>
            {
                /* see http://reactjs.net/guides/azure.html
                 * ReactJS.NET 2.2 did NOT fall back to ClearScript V8 JavaScript engine out-of-the-box for me
                 * had to copy ClearScript dlls to the correct places via gulp tasks to make things work
                 * see http://www.samulihaverinen.com/web-development/dotnet/2016/01/19/how-to-run-clearscript-v8-javascript-engine-in-azure/
                 * (despite the blog post saying this is not needed in 2.2 onwards...) */
                config.SetAllowMsieEngine(false);

                // If you want to use server-side rendering of React components,
                // add all the necessary JavaScript files here. This includes
                // your components as well as all of their dependencies.
                // See http://reactjs.net/ for more information. Example:
                //config
                //    .AddScript("~/Scripts/First.jsx")
                //    .AddScript("~/Scripts/Second.jsx");

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //    .SetLoadBabel(false)
                //    .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
            });
            app.UseStaticFiles();

            app.UseMvc(
                routes =>
                {
                    // default route
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");

                    // .../react
                    // convention based: needs ReactController, View/React/ReactDemo
                    routes.MapRoute(
                        name: "react route",
                        template: "react",
                        defaults: new { controller = "React", action = "ReactDemo" });

                    // .../comments
                    routes.MapRoute(
                        name: "comments route",
                        template: "comments",
                        defaults: new { controller = "React", action = "Comments" });

                    routes.MapRoute(
                        name: "new comment",
                        template: "comments/new",
                        defaults: new { controller = "React", action = "AddComment" });
                }
            );

            var logger = loggerFactory.CreateLogger("JavaScript engine logger");
            logger.LogInformation(this.GetAvailableEngines());
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        public string GetAvailableEngines()
        {
            var sb = new StringBuilder();
            var registrations = React.TinyIoC.TinyIoCContainer.Current.ResolveAll<JavaScriptEngineFactory.Registration>();

            //foreach (var registration in registrations.OrderBy(r => r.Priority)) // linq extension method not picked up by VS correctly :S ?
            foreach (var registration in Enumerable.OrderBy(registrations, r => r.Priority))
            {
                try
                {
                    var engine = registration.Factory();
                    var result = engine.Evaluate<int>("1 + 1");
                    if (result == 2)
                    {
                        sb.AppendLine($"Engine: {engine.Name}, version: {engine.Version}, priority: {registration.Priority}");
                    }

                }
                catch { }
            }

            return sb.ToString();
        }
    }
}
