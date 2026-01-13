using ProjetoBackend.Aplicacao.Login.DTO;
using ProjetoBackend.Aplicacao.Login.Interface;
using ProjetoBackend.Aplicacao.Seguranca;
using ProjetoBackend.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
        public async Task<LoginRespostaDTO> Login (LoginDTO loginDTO)
        {
            var usuario = await _usuarioRepositorio.ObterPorEmail(loginDTO.Email);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            var senhaValida = _senhahashAplicacao.VerificarHash(loginDTO.Senha, usuario.SenhaHash);
            if (!senhaValida)
            {
                throw new Exception("Senha inválida.");
            }
            var token = _jwtAplicacao.GerarToken(usuario);
            return new LoginRespostaDTO
            {
                Token = token,
                TempoDeExpirarOToken = DateTime.UtcNow.AddHours(2)

            };
        }
    }
}
