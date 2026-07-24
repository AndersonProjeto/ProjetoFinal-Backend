using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using ProjetoBackend.API.Extensoes;
using ProjetoBackend.API.Middleware;
using ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao;
using ProjetoBackend.Repositorio.Contexto;

// O Npgsql 6+ passou a mapear DateTime para 'timestamp with time zone' e recusa
// valores que nao sejam UTC. O projeto veio do SQL Server (datetime2, sem fuso),
// entao mantemos o comportamento legado para nao quebrar as datas existentes.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// O Railway injeta a porta em PORT e so considera o deploy saudavel se o processo
// escutar nela. Localmente a variavel nao existe e vale o launchSettings.
var porta = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(porta))
    builder.WebHost.UseUrls($"http://*:{porta}");

// Atras do proxy do Railway o container recebe HTTP puro. Sem ler os cabecalhos
// encaminhados, o ASP.NET enxerga toda requisicao como insegura e ve o IP do
// proxy no lugar do IP do cliente — o que tambem jogaria todo o trafego anonimo
// numa unica particao do rate limiter.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    // O proxy do Railway nao tem IP fixo; sem limpar as listas o ASP.NET descarta
    // os cabecalhos por vir de origem "desconhecida".
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddDbContext<ProjetoContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services
    .AdicionarDependencias()
    .AdicionarClientesIA(builder.Configuration)
    .AdicionarAutenticacaoJwt(builder.Configuration)
    .AdicionarAutorizacaoAdmin(builder.Configuration)
    .AdicionarCorsFrontend(builder.Configuration)
    .AdicionarSwaggerComJwt()
    .AdicionarRateLimiting();

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

// Aplica as migrations pendentes na subida, para que o deploy leve codigo e
// schema juntos em vez de depender de um passo manual contra o Supabase.
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<ProjetoContexto>();
    contexto.Database.Migrate();
}

app.UseForwardedHeaders();

app.UseMiddleware<TratamentoExcecoesMiddleware>();
app.UseCors(ServicosExtensoes.PoliticaCorsFrontend);

// Swagger fica ligado tambem em producao: a API e vitrine do projeto e as rotas
// seguem protegidas por JWT.
app.UseSwagger();
app.UseSwaggerUI();

// Em producao quem termina o TLS e o proxy do Railway; o container nao tem
// endpoint HTTPS proprio, entao o redirect so faz sentido no ambiente local.
if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();
app.Run();
