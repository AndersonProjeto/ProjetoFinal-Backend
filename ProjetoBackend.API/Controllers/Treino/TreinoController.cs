using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Aplicacao.TreinoAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.Treino;

namespace ProjetoBackend.API.Controllers.Treino
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreinoController : ControllerBase
    {
        private readonly ITreinoAplicacao _treinoAplicacao;

        public TreinoController(ITreinoAplicacao treinoAplicacao)
        {
            _treinoAplicacao = treinoAplicacao;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarTreinoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var treinoId = await _treinoAplicacao.AdicionarTreino(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { treinoId },
                treinoId
            );
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarTreinoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _treinoAplicacao.AtualizarTreino(dto);

            return NoContent();
        }

        [HttpDelete("{treinoId}")]
        public async Task<IActionResult> Deletar(int treinoId)
        {
            await _treinoAplicacao.DeletarTreino(treinoId);
            return NoContent();
        }

        [HttpGet("{treinoId}")]
        public async Task<IActionResult> ObterPorId(int treinoId)
        {
            var treino = await _treinoAplicacao.ObterPorId(treinoId);
            return Ok(treino);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(int usuarioId)
        {
            var treinos = await _treinoAplicacao.ListarPorUsuario(usuarioId);
            return Ok(treinos);
        }

        [HttpGet("resumo")]
        public async Task<IActionResult> ObterResumoTreinos()
        {
            var resumo = await _treinoAplicacao.ObterResumoTreinos();
            return Ok(resumo);
        }

        [HttpGet("{treinoId}/total-exercicios")]
        public async Task<IActionResult> ObterTotalExercicios(int treinoId)
        {
            var total = await _treinoAplicacao.ObterTotalExercicios(treinoId);
            return Ok(total);
        }
    }
}
