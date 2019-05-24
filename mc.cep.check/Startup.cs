using mc.cep.service;
using mc.cep.service.Providers;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;
using mc.cript;
using mc.navigator;
using mc.navigator.domain.Interfaces;
using mc.provider.mongo.Context;
using mc.repository.person;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Net.Http;

namespace mc.cep.check
{
    public class DataContextConfig
    {
        public int Port { get; set; }
        public string Ip { get; set; }
        public string DataBasae { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
    }
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


            services.Configure<DataContextConfig>(Configuration.GetSection("MongoDb"));

            services.AddTransient<INavigator, NavigatorService>();
            services.AddTransient<HttpClient>();
            services.AddTransient<IProviderService<Address>, Viacep>();
            services.AddTransient<CepService>();
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
                var section = this.Configuration.GetSection("MongoDb").GetSection("Port");
                if (string.IsNullOrWhiteSpace(section.Value))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(section.Value);
                }
            }
            else
            {
                return Convert.ToInt32(env);
            }
        }

        private string GetIp()
        {
            var env = Environment.GetEnvironmentVariable("CEP_HOST");
            if (string.IsNullOrWhiteSpace(env))
            {
                var section = this.Configuration.GetSection("MongoDb").GetSection("Ip");
                if (string.IsNullOrWhiteSpace(section.Value))
                {
                    return string.Empty;
                }
                else
                {
                    return section.Value;
                }
            }
            else
            {
                return env;
            }
        }

        private string GetDataBase()
        {
            var env = Environment.GetEnvironmentVariable("CEP_DATABASE");
            if (string.IsNullOrWhiteSpace(env))
            {
                var section = this.Configuration.GetSection("MongoDb").GetSection("DataBase");
                if (string.IsNullOrWhiteSpace(section.Value))
                {
                    return string.Empty;
                }
                else
                {
                    return section.Value;
                }
            }
            else
            {
                return env;
            }
        }

        private string GetPassword()
        {
            var env = Environment.GetEnvironmentVariable("CEP_PWS");
            if (string.IsNullOrWhiteSpace(env))
            {
                var section = this.Configuration.GetSection("MongoDb").GetSection("Password");
                if (string.IsNullOrWhiteSpace(section.Value))
                {
                    return string.Empty;
                }
                else
                {
                    return Cryptographer.Decrypt(section.Value, "YmF0dHV0dGluaGE=");
                }
            }
            else
            {
                return Cryptographer.Decrypt(env, "YmF0dHV0dGluaGE=");
            }
        }

        private string GetUser()
        {
            var env = Environment.GetEnvironmentVariable("CEP_USER");
            if (string.IsNullOrWhiteSpace(env))
            {
                var section = this.Configuration.GetSection("MongoDb").GetSection("User");
                if (string.IsNullOrWhiteSpace(section.Value))
                {
                    return string.Empty;
                }
                else
                {
                    return Cryptographer.Decrypt(section.Value, "YmF0dHV0dGluaGE=");
                }
            }
            else
            {
                return Cryptographer.Decrypt(env, "YmF0dHV0dGluaGE=");
            }
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

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
        }
    }
}
