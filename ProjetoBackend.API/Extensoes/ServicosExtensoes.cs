using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoBackend.Aplicacao.EvolucaoAplicacao.Aplicacao;
using ProjetoBackend.Aplicacao.EvolucaoAplicacao.Interface;
using ProjetoBackend.Aplicacao.Exercicio.Aplicacao;
using ProjetoBackend.Aplicacao.Exercicio.Interface;
using ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao;
using ProjetoBackend.Aplicacao.IAInteracoes.Aplicacao;
using ProjetoBackend.Aplicacao.IAInteracoes.Interfaces;
using ProjetoBackend.Aplicacao.IARelatorios.Aplicacao;
using ProjetoBackend.Aplicacao.IARelatorios.Interfaces;
using ProjetoBackend.Aplicacao.Login;
using ProjetoBackend.Aplicacao.Login.Interface;
using ProjetoBackend.Aplicacao.Seguranca;
using ProjetoBackend.Aplicacao.Treino.Aplicacao;
using ProjetoBackend.Aplicacao.TreinoAplicacao.Interface;
using ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Aplicacao;
using ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Interface;
using ProjetoBackend.Aplicacao.Usuarios.Aplicacao;
using ProjetoBackend.Aplicacao.Usuarios.Interfaces;
using ProjetoBackend.Repositorio;
using ProjetoBackend.Repositorio.Interfaces;
using ProjetoBackend.Repositorio.ProjetoBackend.Repositorio;
using ProjetoBackend.Services.IAServices;
using System.Text;

namespace ProjetoBackend.API.Extensoes
{
    public static class ServicosExtensoes
    {
        public const string PoliticaCorsFrontend = "MinhaPoliticaCors";

        public static IServiceCollection AdicionarDependencias(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();

            services.AddScoped<IExercicioRepositorio, ExercicioRepositorio>();
            services.AddScoped<IExercicioAplicacao, ExercicioAplicacao>();

            services.AddScoped<IEvolucaoRepositorio, EvolucaoRepositorio>();
            services.AddScoped<IEvolucaoAplicacao, EvolucaoAplicacao>();

            services.AddScoped<ITreinoRepositorio, TreinoRepositorio>();
            services.AddScoped<ITreinoAplicacao, TreinoAplicacao>();

            services.AddScoped<ITreinoExercicioRepositorio, TreinoExercicioRepositorio>();
            services.AddScoped<ITreinoExercicioAplicacao, TreinoExercicioAplicacao>();

            services.AddScoped<IIAInteracaoRepositorio, IAInterecaoRepositorio>();
            services.AddScoped<IIAInteracaoAplicacao, IAInteracaoAplicacao>();

            services.AddScoped<IIARelatorioRepositorio, IARelatorioRepositorio>();
            services.AddScoped<IIARelatorioAplicacao, IARelatorioAplicacao>();
            services.AddScoped<IARelatorioService>();

            services.AddScoped<IAService, AiService>();

            services.AddScoped<ISenhahashAplicacao, SenhaHashAplicacao>();
            services.AddScoped<IJwtAplicacao, JwtAplicacao>();
            services.AddScoped<LoginAutorizacaoAplicacao>();

            services.AddScoped<ImportacaoExercicioAplicacao>();

            return services;
        }

        public static IServiceCollection AdicionarAutenticacaoJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
                        )
                    };
                });

            return services;
        }

        public static IServiceCollection AdicionarCorsFrontend(this IServiceCollection services, IConfiguration configuration)
        {
            var origens = configuration.GetSection("Cors:Origens").Get<string[]>()
                          ?? new[] { "http://localhost:5173" };

            services.AddCors(options =>
            {
                options.AddPolicy(PoliticaCorsFrontend, policy =>
                {
                    policy.WithOrigins(origens)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AdicionarSwaggerComJwt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoBackend API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira 'Bearer {token}'"
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
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
