using Codetur.Dominio.Repositorios;
using CodeTur.Dominio.Handlers.Pacote;
using CodeTur.Dominio.Handlers.Queries.Pacote;
using CodeTur.Dominio.Handlers.Queries.Usuario;
using CodeTur.Dominio.Handlers.Usuario;
using CodeTur.Infra.Data.Contexts;
using CodeTur.Infra.Data.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CodeTur.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            #region Injeção Dependência Usuário
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<CriarContaCommandHandler, CriarContaCommandHandler>();
            services.AddTransient<AlterarSenhaCommandHandler, AlterarSenhaCommandHandler>();
            services.AddTransient<LogarCommandHandler, LogarCommandHandler>();
            services.AddTransient<ResetarSenhaCommandHandler, ResetarSenhaCommandHandler>();
            services.AddTransient<AlterarUsuarioCommandHandler, AlterarUsuarioCommandHandler>();
            services.AddTransient<ListarUsuarioQueryHandler, ListarUsuarioQueryHandler>();
            services.AddTransient<BuscarUsuarioPorIdQueryHandler, BuscarUsuarioPorIdQueryHandler>();
            #endregion

            #region Injeção Dependência Usuário
            services.AddTransient<IPacoteRepositorio, PacoteRepositorio>();
            services.AddTransient<AdicionarPacoteHandler, AdicionarPacoteHandler>();
            services.AddTransient<AlterarPacoteHandler, AlterarPacoteHandler>();
            services.AddTransient<AlterarImagemHandler, AlterarImagemHandler>();
            services.AddTransient<AlterarStatusHandler, AlterarStatusHandler>();
            services.AddTransient<ListarPacoteQueryHandler, ListarPacoteQueryHandler>();
            services.AddTransient<BuscarPacotePorIdQueryHandler, BuscarPacotePorIdQueryHandler>();
            #endregion

            services.AddDbContext<CodeTurContext>(o => o.UseSqlServer("Data Source=PC-MURILO\\SQLEXPRESS ;Initial Catalog=CodeTur_Dev;user id=sa; password=sa132"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Token:issuer"],
                        ValidAudience = Configuration["Token:audience"],
                        SaveSigninToken = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:secretKey"]))
                    };
                });
            services.AddSwaggerGen(o =>
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "Api CodeTur", Version = "V1" })
            );
            

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "Api CodeTur V1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
