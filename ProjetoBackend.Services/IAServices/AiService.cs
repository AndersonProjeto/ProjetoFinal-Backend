using Azure;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Services.DtoService;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjetoBackend.Services.IAServices
{
    public class AiService : IAService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        // HttpClient vem do IHttpClientFactory (ver ServicosExtensoes): o factory cuida
        // do pooling e da reciclagem de conexões. Autenticação e User-Agent são
        // configurados no registro, não aqui — mutar DefaultRequestHeaders a cada
        // chamada num client compartilhado é condição de corrida.
        public AiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<string> GetAiResponseAsync(string prompt)
        {
            var url = _config["GitHubModels:ApiUrl"];

            var requestBody = new
            {
                model = "gpt-4o",
                messages = new[]
                {
            new { role = "system", content = @"Você é o AcadIA, um personal trainer digital especializado em musculação e nutrição esportiva.
                    Seja técnico, motivador e focado em saúde.

                    Quando mencionar exercícios, sempre que possível inclua um link de vídeo do YouTube demonstrando o exercício.
                     Videos devem ser de canais confiáveis e respeitados na área de fitness, em Português, tem que ser brasileiros.
                    Formato:
                    Vídeo demonstrativo:
                    https://www.youtube.com/....

                    Nunca invente links. Use apenas links reais do YouTube." },
            new { role = "user", content = prompt }
        },
                max_tokens = 500
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Erro: {response.StatusCode} - {error}";
            }

            var result = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<GitHubResponse>(result, options);

            return data?.Choices?.FirstOrDefault()?.Message?.Content ?? "IA não retornou resposta.";
        }
    }
}
