using ProjetoBackend.Dominio.Excecoes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjetoBackend.API.Extensoes
{
    public static class ClaimsPrincipalExtensoes
    {
        /// <summary>
        /// Id do usuário autenticado, lido do token JWT.
        /// Fonte única de identidade: nunca confiar em UsuarioId vindo da rota ou do corpo
        /// da requisição, que são controlados pelo cliente.
        /// </summary>
        public static int ObterUsuarioId(this ClaimsPrincipal usuario)
        {
            // O ASP.NET mapeia 'sub' para NameIdentifier por padrão; aceitamos os dois.
            var valor = usuario.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                        ?? usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(valor, out var usuarioId) || usuarioId <= 0)
                throw new CredenciaisInvalidasException("Token sem identificação de usuário válida.");

            return usuarioId;
        }

        /// <summary>
        /// Garante que o recurso acessado pertence ao usuário autenticado.
        /// </summary>
        public static void GarantirDonoDoRecurso(this ClaimsPrincipal usuario, int usuarioIdDoRecurso)
        {
            if (usuario.ObterUsuarioId() != usuarioIdDoRecurso)
                throw new AcessoNegadoException("Você não tem acesso a este recurso.");
        }
    }
}
