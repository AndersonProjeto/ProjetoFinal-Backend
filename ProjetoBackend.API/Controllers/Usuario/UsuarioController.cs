using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.API.Extensoes;
using ProjetoBackend.API.Validadores;
using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Aplicacao.Login;
using ProjetoBackend.Aplicacao.Login.DTO;
using ProjetoBackend.Aplicacao.Login.Interface;
using ProjetoBackend.Aplicacao.Usuarios.Interfaces;
using ProjetoBackend.Dominio.DTOs.Usuario;

namespace ProjetoBackend.API.Controllers.Usuario
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        private readonly ILoginAutorizacaoAplicacao _loginAplicacao;

        public UsuariosController(IUsuarioAplicacao usuarioAplicacao, ILoginAutorizacaoAplicacao loginAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
            _loginAplicacao = loginAplicacao;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var resposta = await _loginAplicacao.Login(loginDto);
            return Ok(resposta);
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarUsuarioDTO dto)
        {
            var resultado = new UsuarioValidador().Validate(dto);
            if (!resultado.IsValid)
            {
                var erros = resultado.Errors.Select(e => new
                {
                    campo = e.PropertyName,
                    erro = e.ErrorMessage
                });

                return BadRequest(new { erros });
            }

            var id = await _usuarioAplicacao.AdicionarUsuario(dto);
            return CreatedAtAction(nameof(ObterPorId), new { usuarioId = id }, new { usuarioId = id });
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterPorId(int usuarioId)
        {
            User.GarantirDonoDoRecurso(usuarioId);

            var usuario = await _usuarioAplicacao.ObterId(usuarioId);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioDTO dto)
        {
            // A identidade vem do token, não do corpo da requisição.
            dto.UsuarioId = User.ObterUsuarioId();

            await _usuarioAplicacao.AtualizarUsuario(dto);
            return NoContent();
        }

        [HttpPatch("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO dto)
        {
            dto.UsuarioId = User.ObterUsuarioId();

            await _usuarioAplicacao.AlterarSenha(dto);
            return Ok(new { mensagem = "Senha alterada com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            User.GarantirDonoDoRecurso(id);

            await _usuarioAplicacao.DeletarUsuario(id);
            return NoContent();
        }
    }
}
