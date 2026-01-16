using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoBackend.Aplicacao.Exercicio.Aplicacao;
using ProjetoBackend.Aplicacao.Exercicio.Interface;
using ProjetoBackend.Aplicacao.Login;
using ProjetoBackend.Aplicacao.Login.Interface;
using ProjetoBackend.Aplicacao.Seguranca;
using ProjetoBackend.Aplicacao.Usuarios.Aplicacao;
using ProjetoBackend.Aplicacao.Usuarios.Interfaces;
using ProjetoBackend.Repositorio;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjetoContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();

builder.Services.AddScoped<IExercicioRepositorio, ExercicioRepositorio>();
builder.Services.AddScoped<IExercicioAplicacao, ExercicioAplicacao>();


builder.Services.AddScoped<ISenhahashAplicacao, SenhaHashAplicacao>();
builder.Services.AddScoped<IJwtAplicacao, JwtAplicacao>();
builder.Services.AddScoped<LoginAutorizacaoAplicacao>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
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
            new string[] {}
        }
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
