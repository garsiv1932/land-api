using System;
using System.Net;
using System.Text;
using Api.Context;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;        

        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            // services.AddScoped<IdentityDbContext, ApiContext>();
            
            if (_env.IsDevelopment())
            {
                services.AddDbContext<ApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ApiConnection_Dev")));
            }else if (_env.IsProduction())
            {
                services.AddDbContext<ApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ApiConnection_Prod")));
            }
            
            services.AddAutoMapper(typeof(Startup));

            /*
            Agregamos los parametros de validacion del token
            Aqui configuramos el servicio que toma el token que llegue a mi solucion y valide que el JWT que llego
            al sistema fue firmada por nuestra aplicacion
            */
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options => options.TokenValidationParameters = new TokenValidationParameters
                {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                ClockSkew = TimeSpan.Zero
                });

            services.AddTransient<ServiceArticle>();
            services.AddTransient<ServiceWeb>();
            services.AddTransient<ServiceVisit>();
            services.AddTransient<ServiceWebUser>();
            services.AddTransient<DbInitializer>();
            services.AddTransient<ServiceLogs>();
            // services.AddSingleton(Configuration);
            
            services.AddControllers();
            
            // services.Configure<ForwardedHeadersOptions>(options =>
            // {
            //     options.ForwardedHeaders =
            //         ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            // });
            
            // services.Configure<ForwardedHeadersOptions>(options =>
            // {
            //     options.KnownProxies.Add(IPAddress.Parse("202.61.227.108"));
            // });
            
            services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                //Esto tiene que ver con el nombrado de los titulos
                // Referencia: https://rimdev.io/swagger-grouping-with-controller-name-fallback-using-swashbuckle-aspnetcore/
                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }
                
                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }
                
                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true); 
                //
                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                
                // Habilita las anotations en los metodos del controlador
                // Referencia:https://medium.com/c-sharp-progarmming/configure-annotations-in-swagger-documentation-for-asp-net-core-api-8215596907c7
                c.EnableAnnotations();
                
            
                //Configurando Swagger para que soporte JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            // services.Configure<KestrelServerOptions>(
            //     Configuration.GetSection("Kestrel"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // app.UseForwardedHeaders(new ForwardedHeadersOptions
            // {
            //     ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            // });
            // app.UseForwardedHeaders();
            
            app.UseCors(e =>
            {
                e.AllowAnyOrigin();
                e.AllowAnyMethod();
                e.AllowAnyHeader();
            });

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                //Dont show scheemes on Swagger UI
                c.DefaultModelsExpandDepth(-1);
            });
            
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApiContext>();
                var werbService = serviceScope.ServiceProvider.GetRequiredService<ServiceWeb>();
                new DbInitializer(werbService,context).Initialize();
            }
        }
    }
}
