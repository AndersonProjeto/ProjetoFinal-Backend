using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjetoBackend.API.Extensoes;
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
    [EnableRateLimiting(RateLimitingExtensoes.PoliticaIa)]
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
            User.GarantirDonoDoRecurso(usuarioId);

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
            User.GarantirDonoDoRecurso(usuarioId);

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
        [HttpGet("ultimas/{usuarioId}/{quantidade}")]
        public async Task<IActionResult> ListarUltimas(int usuarioId, int quantidade)
        {
            User.GarantirDonoDoRecurso(usuarioId);

            try
            {
                var interacoes = await _iaInteracaoAplicacao.ListarUltimasInteracoes(usuarioId, quantidade);
                return Ok(interacoes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("perguntar")]
        public async Task<IActionResult> Perguntar([FromBody] PerguntarParaIADTO dto)
        {
            // A interação é sempre gravada para o dono do token, não para o id enviado.
            var usuarioId = User.ObterUsuarioId();

            if (string.IsNullOrWhiteSpace(dto.Pergunta))
                return BadRequest(new { mensagem = "Pergunta obrigatória." });

            if (dto.Pergunta.Length > LimitesIA.TamanhoMaximoPrompt)
                return BadRequest(new { mensagem = $"Pergunta excede o limite de {LimitesIA.TamanhoMaximoPrompt} caracteres." });

            var respostaIA = await _aiService.GetAiResponseAsync(dto.Pergunta);

            var interacao = new Dominio.Entidade.IAInteracao(
                usuarioId,
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
