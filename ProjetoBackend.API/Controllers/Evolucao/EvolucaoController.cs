using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Aplicacao.EvolucaoAplicacao.Interface;
using ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao;
using ProjetoBackend.Dominio.DTOs.Evolucao;

namespace ProjetoBackend.API.Controllers.Evolucao
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EvolucaoController : ControllerBase
    {
        private readonly IEvolucaoAplicacao _evolucaoAplicacao;


        public EvolucaoController(IEvolucaoAplicacao evolucaoAplicacao)
        {
            _evolucaoAplicacao = evolucaoAplicacao;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarEvolucaoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var evolucaoId = await _evolucaoAplicacao.AdicionarEvolucao(dto);

            return CreatedAtAction(
                nameof(ObterUltimaEvolucao),
                new { usuarioId = dto.UsuarioId },
                evolucaoId
            );
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarEvolucaoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _evolucaoAplicacao.AtualizarEvolucao(dto);

            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}/ultima")]
        public async Task<IActionResult> ObterUltimaEvolucao(int usuarioId)
        {
            var evolucao = await _evolucaoAplicacao.ObterUltimaEvolucao(usuarioId);

            if (evolucao == null)
                return NotFound();

            return Ok(evolucao);
        }

        [HttpGet("usuario/{usuarioId}/resumo")]
        public async Task<IActionResult> ResumoEvolucao(int usuarioId)
        {
            var resumo = await _evolucaoAplicacao.ResumoEvolucao(usuarioId);
            return Ok(resumo);
        }

        [HttpGet("usuario/{usuarioId}/historico")]
        public async Task<IActionResult> HistoricoDeEvolucao(int usuarioId)
        {
            var historico = await _evolucaoAplicacao.HistoricoDeEvolucaoDoUsuario(usuarioId);
            return Ok(historico);
        }

        [HttpGet("usuario/{usuarioId}/peso-inicial")]
        public async Task<IActionResult> ObterPesoInicial(int usuarioId)
        {
            var pesoInicial = await _evolucaoAplicacao.ObterPesoInicial(usuarioId);
            return Ok(pesoInicial);
        }

        [HttpGet("usuario/{usuarioId}/diferenca-peso")]
        public async Task<IActionResult> ObterDiferencaPeso(int usuarioId)
        {
            var diferenca = await _evolucaoAplicacao.ObterDiferencaPeso(usuarioId);
            return Ok(diferenca);
        }
        [HttpGet("usuario/{usuarioId}/cintura-inicial")]
        public async Task<IActionResult> ObterCinturaInicial(int usuarioId)
        {
            var cinturaInicial = await _evolucaoAplicacao.ObterCinturaInicial(usuarioId);
            return Ok(cinturaInicial);
        }

        [HttpGet("usuario/{usuarioId}/diferenca-cintura")]
        public async Task<IActionResult> ObterDiferencaCintura(int usuarioId)
        {
            var diferenca = await _evolucaoAplicacao.ObterDiferencaCintura(usuarioId);
            return Ok(diferenca);
        }

        [HttpGet("usuario/{usuarioId}/braco-inicial")]
        public async Task<IActionResult> ObterBracoInicial(int usuarioId)
        {
            var bracoInicial = await _evolucaoAplicacao.ObterBracoInicial(usuarioId);
            return Ok(bracoInicial);
        }

        [HttpGet("usuario/{usuarioId}/diferenca-braco")]
        public async Task<IActionResult> ObterDiferencaBraco(int usuarioId)
        {
            var diferenca = await _evolucaoAplicacao.ObterDiferencaBraco(usuarioId);
            return Ok(diferenca);
        }

        [HttpGet("usuario/{usuarioId}/coxa-inicial")]
        public async Task<IActionResult> ObterCoxaInicial(int usuarioId)
        {
            var coxaInicial = await _evolucaoAplicacao.ObterCoxaInicial(usuarioId);
            return Ok(coxaInicial);
        }

        [HttpGet("usuario/{usuarioId}/diferenca-coxa")]
        public async Task<IActionResult> ObterDiferencaCoxa(int usuarioId)
        {
            var diferenca = await _evolucaoAplicacao.ObterDiferencaCoxa(usuarioId);
            return Ok(diferenca);
        }



    }
}
