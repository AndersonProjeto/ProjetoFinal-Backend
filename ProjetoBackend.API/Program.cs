using Microsoft.EntityFrameworkCore;
using ProjetoBackend.API.Extensoes;
using ProjetoBackend.API.Middleware;
using ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao;
using ProjetoBackend.Repositorio.Contexto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjetoContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services
    .AdicionarDependencias()
    .AdicionarClientesIA(builder.Configuration)
    .AdicionarAutenticacaoJwt(builder.Configuration)
    .AdicionarCorsFrontend(builder.Configuration)
    .AdicionarSwaggerComJwt();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });

builder.Services.AddHttpClient<ExerciseDbService>(client =>
{
    client.BaseAddress = new Uri("https://exercise-db-with-videos-and-images-by-ascendapi.p.rapidapi.com");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", builder.Configuration["ExerciseDb:ApiKey"]);
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "exercise-db-with-videos-and-images-by-ascendapi.p.rapidapi.com");
});

var app = builder.Build();

app.UseMiddleware<TratamentoExcecoesMiddleware>();
app.UseCors(ServicosExtensoes.PoliticaCorsFrontend);

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
