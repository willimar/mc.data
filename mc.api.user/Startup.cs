using mc.api.user.Controllers;
using mc.api.user.Services;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;
using mc.cript;
using mc.provider.mongo.Context;
using mc.repository.user;
using mc.user.service.Interface;
using mc.user.service.Register;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace mc.api.user
{
    public class Startup
    {
        public class DataContextConfig
        {
            public int Port { get; set; }
            public string Ip { get; set; }
            public string DataBasae { get; set; }
            public string Password { get; set; }
            public string User { get; set; }
        }

        public IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "User API Service",
                        Version = "1.0.1",
                        Description = "Input data from DataBase and User validation",
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

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserController>();

            services.Configure<DataContextConfig>(Configuration.GetSection("MongoDb"));
            services.AddTransient<IProvider>(rresult =>
                new DataContext(
                        port: this.GetPort(),
                        ip: this.GetIp(),
                        dataBaseName: this.GetDataBase(),
                        password: this.GetPassword(),
                        userName: this.GetUser()
                        ));

            var signingConfigurations = new SigningConfigurations();
            var tokenConfigurations = new TokenConfigurations() { 
                Seconds = 5*60
            };
            services.AddSingleton(signingConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOption => {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();
        }

        private int GetPort()
        {
            var env = Environment.GetEnvironmentVariable("REGISTER_PORT");

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
            var env = Environment.GetEnvironmentVariable("REGISTER_HOST");
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
            var env = Environment.GetEnvironmentVariable("REGISTER_DATABASE");
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
            var env = Environment.GetEnvironmentVariable("REGISTER_PWS");
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
            var env = Environment.GetEnvironmentVariable("REGISTER_USER");
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
                    "Data Register Service to User");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
        }
    }
}
