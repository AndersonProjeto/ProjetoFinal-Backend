using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.TreinoExercicio;

namespace ProjetoBackend.API.Controllers.TreinoExercicio
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreinoExercicioController : ControllerBase
    {
        private readonly ITreinoExercicioAplicacao _treinoExercicioAplicacao;

        public TreinoExercicioController(ITreinoExercicioAplicacao treinoExercicioAplicacao)
        {
            _treinoExercicioAplicacao = treinoExercicioAplicacao;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarTreinoExercicioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var treinoExercicioId = await _treinoExercicioAplicacao.AdicionarTreinoExercicio(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { treinoExercicioId },
                treinoExercicioId
            );
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarTreinoExercicioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _treinoExercicioAplicacao.AtualizarTreinoExercicio(dto);

            return NoContent();
        }

        [HttpDelete("{treinoExercicioId}")]
        public async Task<IActionResult> Deletar(int treinoExercicioId)
        {
            await _treinoExercicioAplicacao.DeletarTreinoExercicio(treinoExercicioId);
            return NoContent();
        }

        [HttpGet("{treinoExercicioId}")]
        public async Task<IActionResult> ObterPorId(int treinoExercicioId)
        {
            var treinoExercicio = await _treinoExercicioAplicacao.ObterPorID(treinoExercicioId);

            if (treinoExercicio == null)
                return NotFound();

            return Ok(treinoExercicio);
        }

        [HttpGet("treino/{treinoId}")]
        public async Task<IActionResult> ListarTreino(int treinoId)
        {
            var treinos = await _treinoExercicioAplicacao.ListarTreino(treinoId);
            return Ok(treinos);
        }
    }
}
