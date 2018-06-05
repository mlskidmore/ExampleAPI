using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApiContrib.Core.Formatter.PlainText;
using Microsoft.AspNetCore.Mvc;
using ADXETools.FalconRequests;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System;

namespace ADXETools
{
    public class Startup
    {
        private readonly IEnvironmentConfiguration envConfig;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            envConfig = new EnvironmentConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
        {
            MvcOptions mvcOptions = new MvcOptions();
            services.AddMvc(config =>
            {
                // Add XML Content Negotiation
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter(mvcOptions));
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                config.InputFormatters.Add(new PlainTextInputFormatter());
                config.OutputFormatters.Add(new PlainTextOutputFormatter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ADXE Tools API", Version = "v1" });

                // TO BE ADDED:  Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "adxetools.api.xml");
                //c.IncludeXmlComments(xmlPath);
            });

            services.TryAddSingleton<IEnvironmentConfiguration>(new EnvironmentConfiguration(Configuration));
            services.TryAddSingleton<HttpClient> ();
            services.TryAddSingleton<IFalconPort, FalconPort>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "ADXE Tools API");
                });

                app.UseMvcWithDefaultRoute();
                app.UseMvc();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
}
    }
}
