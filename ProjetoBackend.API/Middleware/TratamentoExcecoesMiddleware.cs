using ProjetoBackend.Dominio.Excecoes;
using System.Text.Json;

namespace ProjetoBackend.API.Middleware
{
    /// <summary>
    /// Converte exceções da aplicação em respostas HTTP padronizadas ({ mensagem }),
    /// evitando try/catch repetido nos controllers e vazamento de detalhes internos.
    /// </summary>
    public class TratamentoExcecoesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TratamentoExcecoesMiddleware> _logger;

        public TratamentoExcecoesMiddleware(RequestDelegate next, ILogger<TratamentoExcecoesMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _next(contexto);
            }
            catch (Exception ex)
            {
                var (status, mensagem) = ex switch
                {
                    NaoEncontradoException => (StatusCodes.Status404NotFound, ex.Message),
                    CredenciaisInvalidasException => (StatusCodes.Status401Unauthorized, ex.Message),
                    RegraDeNegocioException => (StatusCodes.Status400BadRequest, ex.Message),
                    ArgumentException => (StatusCodes.Status400BadRequest, ex.Message),
                    _ => (StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado. Tente novamente.")
                };

                if (status == StatusCodes.Status500InternalServerError)
                    _logger.LogError(ex, "Erro não tratado em {Rota}", contexto.Request.Path);

                contexto.Response.StatusCode = status;
                contexto.Response.ContentType = "application/json";
                await contexto.Response.WriteAsync(JsonSerializer.Serialize(new { mensagem }));
            }
        }
    }
}
