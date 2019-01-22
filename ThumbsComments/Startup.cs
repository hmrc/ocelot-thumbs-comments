using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using ThumbsComments.Interfaces;
using ThumbsComments.Models;
using ThumbsComments.Repositories;

namespace ThumbsComments
{
    /// <summary>
    /// Project startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup contstructor
        /// </summary>
        /// <param name="configuration">Startup configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// IConfiguration value
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure project services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
               options
                   .UseSqlServer(Configuration.GetConnectionString("DefaultConnection") +
                           Environment.GetEnvironmentVariable("Connection", EnvironmentVariableTarget.Machine)));

            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);
            services.AddMvc();

            services.AddScoped<IThumbsCommentsRepository, ThumbsCommentRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Thumbs Comments",
                    Description = "Thumbs comments for thumbs dashboard",
                    TermsOfService = "to be confirmed",
                    Contact = new Contact
                    {
                        Name = "David Kinghan",
                        Email = "david.kinghan@hmrc.gov.uk",
                        Url = "to be confirmed"
                    },
                    License = new License
                    {
                        Name = "to be confirmed",
                        Url = "to be confirmed"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });


            services.Configure<IISOptions>(c =>
            {
                c.ForwardClientCertificate = true;
                c.AutomaticAuthentication = true;
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application Builder</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Context>();

                context.Database.EnsureCreated();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Thumbs Comments");
            });
        }
    }
}
