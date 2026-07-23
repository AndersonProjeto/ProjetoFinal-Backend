using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.API.Extensoes;
using ProjetoBackend.Aplicacao.TreinoAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.Treino;
using ProjetoBackend.Dominio.Excecoes;

namespace ProjetoBackend.API.Controllers.Treino
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TreinoController : ControllerBase
    {
        private readonly ITreinoAplicacao _treinoAplicacao;

        public TreinoController(ITreinoAplicacao treinoAplicacao)
        {
            _treinoAplicacao = treinoAplicacao;
        }

        /// <summary>
        /// Carrega o treino garantindo que ele pertence ao usuário autenticado.
        /// </summary>
        private async Task<Dominio.Entidade.Treino> ObterTreinoDoUsuario(int treinoId)
        {
            var treino = await _treinoAplicacao.ObterPorId(treinoId)
                ?? throw new NaoEncontradoException("Treino não encontrado.");

            User.GarantirDonoDoRecurso(treino.UsuarioId);

            return treino;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarTreinoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // A identidade vem do token, não do corpo da requisição.
            dto.UsuarioId = User.ObterUsuarioId();

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

            await ObterTreinoDoUsuario(dto.TreinoId);

            await _treinoAplicacao.AtualizarTreino(dto);

            return NoContent();
        }

        [HttpDelete("{treinoId}")]
        public async Task<IActionResult> Deletar(int treinoId)
        {
            await ObterTreinoDoUsuario(treinoId);

            await _treinoAplicacao.DeletarTreino(treinoId);
            return NoContent();
        }

        [HttpGet("{treinoId}")]
        public async Task<IActionResult> ObterPorId(int treinoId)
        {
            var treino = await ObterTreinoDoUsuario(treinoId);
            return Ok(treino);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(int usuarioId)
        {
            User.GarantirDonoDoRecurso(usuarioId);

            var treinos = await _treinoAplicacao.ListarPorUsuario(usuarioId);
            return Ok(treinos);
        }

        [HttpGet("resumo")]
        public async Task<IActionResult> ObterResumoTreinos()
        {
            var resumo = await _treinoAplicacao.ObterResumoTreinos(User.ObterUsuarioId());
            return Ok(resumo);
        }

        [HttpGet("{treinoId}/total-exercicios")]
        public async Task<IActionResult> ObterTotalExercicios(int treinoId)
        {
            await ObterTreinoDoUsuario(treinoId);

            var total = await _treinoAplicacao.ObterTotalExercicios(treinoId);
            return Ok(total);
        }
    }
}
