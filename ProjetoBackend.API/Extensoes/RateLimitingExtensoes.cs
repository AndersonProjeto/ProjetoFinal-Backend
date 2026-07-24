using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace ProjetoBackend.API.Extensoes
{
    public static class RateLimitingExtensoes
    {
        public const string PoliticaIa = "Ia";

        private const int LimitePadrao = 100;
        private const int LimiteIa = 5;
        private static readonly TimeSpan Janela = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Rate limiting por usuário (chave = id do usuário no token JWT; requisições
        /// anônimas, como login/registro, usam o IP como chave).
        /// Política padrão vale para toda a API; a política "Ia" é mais restrita e
        /// se aplica aos controllers que chamam serviços de IA pagos (GitHub Models, Groq).
        /// </summary>
        public static IServiceCollection AdicionarRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.OnRejected = async (context, cancellationToken) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                            ((int)retryAfter.TotalSeconds).ToString(CultureInfo.InvariantCulture);
                    }

                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(
                        "{\"mensagem\":\"Muitas requisições. Tente novamente em instantes.\"}",
                        cancellationToken);
                };

                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        ObterChaveParticao(httpContext),
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = LimitePadrao,
                            Window = Janela,
                            QueueLimit = 0
                        }));

                options.AddPolicy(PoliticaIa, httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        ObterChaveParticao(httpContext),
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = LimiteIa,
                            Window = Janela,
                            QueueLimit = 0
                        }));
            });

            return services;
        }

        private static string ObterChaveParticao(HttpContext httpContext)
        {
            var usuarioId = httpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                          ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return usuarioId is not null
                ? $"usuario:{usuarioId}"
                : $"ip:{httpContext.Connection.RemoteIpAddress}";
        }
    }
}
