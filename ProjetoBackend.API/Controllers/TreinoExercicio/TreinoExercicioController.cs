using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.API.Extensoes;
using ProjetoBackend.Aplicacao.TreinoAplicacao.Interface;
using ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio.Excecoes;

namespace ProjetoBackend.API.Controllers.TreinoExercicio
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TreinoExercicioController : ControllerBase
    {
        private readonly ITreinoExercicioAplicacao _treinoExercicioAplicacao;
        private readonly ITreinoAplicacao _treinoAplicacao;

        public TreinoExercicioController(
            ITreinoExercicioAplicacao treinoExercicioAplicacao,
            ITreinoAplicacao treinoAplicacao)
        {
            _treinoExercicioAplicacao = treinoExercicioAplicacao;
            _treinoAplicacao = treinoAplicacao;
        }

        /// <summary>
        /// A posse de um exercício de treino é herdada do treino a que ele pertence.
        /// </summary>
        private async Task GarantirDonoDoTreino(int treinoId)
        {
            var treino = await _treinoAplicacao.ObterPorId(treinoId)
                ?? throw new NaoEncontradoException("Treino não encontrado.");

            User.GarantirDonoDoRecurso(treino.UsuarioId);
        }

        private async Task<Dominio.Entidade.TreinoExercicio> ObterItemDoUsuario(int treinoExercicioId)
        {
            var item = await _treinoExercicioAplicacao.ObterPorID(treinoExercicioId)
                ?? throw new NaoEncontradoException("Exercício do treino não encontrado.");

            await GarantirDonoDoTreino(item.TreinoId);

            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarTreinoExercicioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await GarantirDonoDoTreino(dto.TreinoId);

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

            await ObterItemDoUsuario(dto.TreinoExercicioId);

            await _treinoExercicioAplicacao.AtualizarTreinoExercicio(dto);

            return NoContent();
        }

        [HttpDelete("{treinoExercicioId}")]
        public async Task<IActionResult> Deletar(int treinoExercicioId)
        {
            await ObterItemDoUsuario(treinoExercicioId);

            await _treinoExercicioAplicacao.DeletarTreinoExercicio(treinoExercicioId);
            return NoContent();
        }

        [HttpGet("{treinoExercicioId}")]
        public async Task<IActionResult> ObterPorId(int treinoExercicioId)
        {
            var treinoExercicio = await ObterItemDoUsuario(treinoExercicioId);
            return Ok(treinoExercicio);
        }

        [HttpGet("treino/{treinoId}")]
        public async Task<IActionResult> ListarTreino(int treinoId)
        {
            await GarantirDonoDoTreino(treinoId);

            var treinos = await _treinoExercicioAplicacao.ListarTreino(treinoId);
            return Ok(treinos);
        }
    }
}
