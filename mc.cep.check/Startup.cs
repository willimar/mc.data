using System;
using mc.cep.service;
using mc.cep.service.Providers;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;
using mc.navigator;
using mc.navigator.domain.Interfaces;
using mc.provider.mongo.Context;
using mc.repository.person;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;

namespace mc.cep.check
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "CEP Find service",
                        Version = "1.0.1",
                        Description = "API to search CEP in web provider.",
                        Contact = new Contact
                        {
                            Name = "Rocha, Willimar",
                            Url = "https://github.com/willimar",
                            Email = "willimar in the google",
                        },
                        TermsOfService = "© 2019 Willimar Rocha"
                    });
            });

            services.AddCors(options => {
                options.AddPolicy(AllowSpecificOrigins, 
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddTransient<INavigator, NavigatorService>();
            services.AddTransient<IProviderService<Address>, Viacep>();
            services.AddTransient<CepService<Address>>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IProvider>(rresult => 
                new DataContext(
                        port: this.GetPort(),
                        ip: this.GetIp(),
                        dataBaseName: this.GetDataBase(),
                        password: this.GetPassword(),
                        userName: this.GetUser()
                        ));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();
        }

        private int GetPort()
        {
            var env = Environment.GetEnvironmentVariable("CEP_PORT");

            if (string.IsNullOrWhiteSpace(env))
            {

            }

            return 0;
        }

        private string GetIp()
        {
            var env = Environment.GetEnvironmentVariable("CEP_HOST");
            return string.Empty;
        }

        private string GetDataBase()
        {
            var env = Environment.GetEnvironmentVariable("CEP_DATABASE");
            return string.Empty;
        }

        private string GetPassword()
        {
            var env = Environment.GetEnvironmentVariable("CEP_PWS");
            return string.Empty;
        }

        private string GetUser()
        {
            var env = Environment.GetEnvironmentVariable("CEP_USER");
            return string.Empty;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowSpecificOrigins);
            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Postal code search service");
            });
        }
    }
}
