using mc.api.register.Controllers.Register;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;
using mc.core.service.Interface;
using mc.core.service.Register;
using mc.provider.sqlserver.Context;
using mc.repository.person;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace mc.api.register
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
                        Title = "Data register service",
                        Version = "1.0.1",
                        Description = "Input data from DataBase",
                        Contact = new Contact
                        {
                            Name = "Rocha, Willimar",
                            Url = "https://github.com/willimar",
                            Email = "willimar in the google",
                        },
                        TermsOfService = "© 2019 Willimar Rocha"
                    });
            });

            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<PersonController>();

            services.AddTransient<IProvider>(s => 
                new DataContext(0, @".\SQLEXPRESS", "MCDATA", "superwell", "sa")
            );

            services.AddCors(options => {
                options.AddPolicy(AllowSpecificOrigins,
                    builder => {
                        //builder.WithOrigins(@"http://localhost:4200");
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                    "Data Register Service");
            });
        }
    }
}
