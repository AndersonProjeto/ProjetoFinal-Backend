using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBackend.Aplicacao.IAInteracoes.Interfaces;
using ProjetoBackend.Dominio.DTOs;
using ProjetoBackend.Dominio.DTOs.IAInteracao;
using ProjetoBackend.Services.IAServices;

namespace ProjetoBackend.API.Controllers.IAInteracao
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IAInteracaoController : ControllerBase
    {
        private readonly IIAInteracaoAplicacao _iaInteracaoAplicacao;
        private readonly IAService _aiService;


        public IAInteracaoController(IIAInteracaoAplicacao iaInteracaoAplicacao, IAService aiService)
        {
            _iaInteracaoAplicacao = iaInteracaoAplicacao;
            _aiService = aiService;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(int usuarioId)
        {
            try
            {
                var interacoes = await _iaInteracaoAplicacao.ListarIAInteracoesPorUsuario(usuarioId);
                return Ok(interacoes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("ultima/{usuarioId}")]
        public async Task<IActionResult> ObterUltimaInteracao(int usuarioId)
        {
            try
            {
                var interacao = await _iaInteracaoAplicacao.ObterUltimaInteracao(usuarioId);

                if (interacao == null)
                    return NotFound();

                return Ok(interacao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
        [HttpPost("perguntar")]
        public async Task<IActionResult> Perguntar([FromBody] PerguntarParaIADTO dto)
        {
            if (dto.UsuarioId <= 0)
                return BadRequest("Usuário inválido.");

            if (string.IsNullOrWhiteSpace(dto.Pergunta))
                return BadRequest("Pergunta obrigatória.");

            var respostaIA = await _aiService.GetAiResponseAsync(dto.Pergunta);

            var interacao = new Dominio.Entidade.IAInteracao(
                dto.UsuarioId,
                dto.Pergunta,
                respostaIA
            );

            await _iaInteracaoAplicacao.AdicionarIAInteracao(interacao);

            return Ok(new
            {
                pergunta = dto.Pergunta,
                resposta = respostaIA
            });
        }
    }
}
