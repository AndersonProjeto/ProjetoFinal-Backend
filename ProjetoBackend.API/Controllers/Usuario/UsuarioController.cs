using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Aplicacao.Login;
using ProjetoBackend.Aplicacao.Login.DTO;
using ProjetoBackend.Aplicacao.Usuarios.Interfaces;
using ProjetoBackend.Dominio.DTOs.Usuario;

namespace ProjetoBackend.API.Controllers.Usuario
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        private readonly LoginAutorizacaoAplicacao _loginAplicacao;

        public UsuariosController(IUsuarioAplicacao usuarioAplicacao, LoginAutorizacaoAplicacao loginAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
            _loginAplicacao = loginAplicacao;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var resposta = await _loginAplicacao.Login(loginDto);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { mensagem = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarUsuarioDTO dto)
        {
            try
            {
                var id = await _usuarioAplicacao.AdicionarUsuario(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var usuario = await _usuarioAplicacao.ObterId(id);
                if (usuario == null) return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut]
         [Authorize]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioDTO dto)
        {
            try
            {
                await _usuarioAplicacao.AtualizarUsuario(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPatch("alterar-senha")]
        [Authorize]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO dto)
        {
            try
            {
                await _usuarioAplicacao.AlterarSenha(dto);
                return Ok(new { mensagem = "Senha alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _usuarioAplicacao.DeletarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
