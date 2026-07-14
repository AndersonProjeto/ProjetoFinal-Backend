using ProjetoBackend.Aplicacao.Login.DTO;
using ProjetoBackend.Aplicacao.Login.Interface;
using ProjetoBackend.Aplicacao.Seguranca;
using ProjetoBackend.Aplicacao.Usuarios.Aplicacao;
using ProjetoBackend.Dominio.Excecoes;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.Login
{
    public class LoginAutorizacaoAplicacao
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISenhahashAplicacao _senhahashAplicacao;
        private readonly IJwtAplicacao _jwtAplicacao;

        public LoginAutorizacaoAplicacao(IUsuarioRepositorio usuarioRepositorio, ISenhahashAplicacao senhahashAplicacao, IJwtAplicacao jwtAplicacao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _senhahashAplicacao = senhahashAplicacao;
            _jwtAplicacao = jwtAplicacao;
        }
        public async Task<LoginRespostaDTO> Login(LoginDTO loginDTO)
        {
            // Mensagem única para email inexistente e senha errada:
            // não revelar a um atacante quais emails estão cadastrados.
            var usuario = await _usuarioRepositorio.ObterPorEmail(loginDTO.Email)
                ?? throw new CredenciaisInvalidasException("Email ou senha inválidos.");

            var senhaValida = _senhahashAplicacao.VerificarHash(loginDTO.Senha, usuario.SenhaHash);
            if (!senhaValida)
                throw new CredenciaisInvalidasException("Email ou senha inválidos.");

            var token = _jwtAplicacao.GerarToken(usuario);
            return new LoginRespostaDTO
            {
                Token = token,
                TempoDeExpirarOToken = DateTime.UtcNow.AddHours(JwtAplicacao.HorasParaExpirar),
                UsuarioId = usuario.UsuarioId
            };
        }
    }
}
