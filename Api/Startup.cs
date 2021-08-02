using System;
using System.Text;
using Api.Context;
using Api.SRVs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.SRVs;

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
            services.AddControllers();
            // services.AddScoped<IdentityDbContext, ApiContext>();
            
            if (_env.IsDevelopment())
            {
                services.AddDbContext<ApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ApiConnection_Dev")));
            }else if (_env.IsProduction())
            {
                services.AddDbContext<ApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ApiConnection_Prod")));
            }
            
            services.AddCors();

            services.AddDbContext<ApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ApiConnection_Prod")));
            services.AddAutoMapper(typeof(Startup));

            /*
            Agregamos los parametros de validacion del token
            Aqui configuramos el servicio que toma el token que llegue a mi solucion y valide que el JWT que llego
            al sistema fue firmada por nuestra aplicacion
            */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options => options.TokenValidationParameters = new TokenValidationParameters
                {
                ValidateIssuer    = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                ClockSkew = TimeSpan.Zero
                });

            services.AddTransient<Service_Blog_Article>();
            services.AddTransient<Service_Blog>();
            services.AddSingleton<IConfiguration>(provider => Configuration);
            
            // services.AddCors();
            //Configurando Swagger para que soporte JWT 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
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


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {

            }
            
            app.UseCors(
                options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
            );
            
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                c.DefaultModelsExpandDepth(-1);
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
