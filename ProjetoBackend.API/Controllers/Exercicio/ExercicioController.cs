using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Aplicacao.Exercicio.Interface;
using ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Enum;

namespace ProjetoBackend.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExercicioController : ControllerBase
    {
        private readonly IExercicioAplicacao _exercicioAplicacao;
        private readonly ImportacaoExercicioAplicacao _importacaoExercicioAplicacao;

        public ExercicioController(IExercicioAplicacao exercicioAplicacao, ImportacaoExercicioAplicacao importacaoExercicioAplicacao)
        {
            _exercicioAplicacao = exercicioAplicacao;
            _importacaoExercicioAplicacao = importacaoExercicioAplicacao;
        }


        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarExercicioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exercicioId = await _exercicioAplicacao.AdicionarExercicio(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { exercicioId },
                exercicioId
            );
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarExercicioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _exercicioAplicacao.AtualizarExercicio(dto);

            return NoContent();
        }

        [HttpDelete("{exercicioId}")]
        public async Task<IActionResult> Deletar(int exercicioId)
        {
            await _exercicioAplicacao.DeletarExercicio(exercicioId);
            return NoContent();
        }


        [HttpGet("{exercicioId}")]
        public async Task<IActionResult> ObterPorId(int exercicioId)
        {
            var exercicio = await _exercicioAplicacao.ObterPorID(exercicioId);
            return Ok(exercicio);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var exercicios = await _exercicioAplicacao.ObterTodosExercicios();
            return Ok(exercicios);
        }

        // GET: api/exercicio/grupo/{grupoMuscular}
        [HttpGet("grupo/{grupoMuscular}")]
        public async Task<IActionResult> ListarPorGrupoMuscular(EnumGrupoMuscular grupoMuscular)
        {
            var exercicios = await _exercicioAplicacao.ListarPorGrupoMuscular(grupoMuscular);
            return Ok(exercicios);
        }

        // GET: api/exercicio/{exercicioId}/detalhado
        [HttpGet("{exercicioId}/detalhado")]
        public async Task<IActionResult> ObterDetalhado(int exercicioId)
        {
            var detalhe = await _exercicioAplicacao.ObterExercicioDetalhado(exercicioId);

            if (detalhe == null)
                return NotFound();

            return Ok(detalhe);
        }
        [HttpGet("paginado")]
        public async Task<IActionResult> GetPaginado(int pagina = 1, int tamanhoPagina = 5)
        {
            var result = await _exercicioAplicacao.ObterExerciciosPaginados(pagina, tamanhoPagina);
            return Ok(result);
        }


        
        [HttpGet("{exercicioId:int}/resumo")]
        public async Task<IActionResult> TotalTreinos(int exercicioId)
        {
            var resumo = await _exercicioAplicacao.TotalTreinosPorExercicio(exercicioId);

            if (resumo == null)
                return NotFound();

            return Ok(resumo);
        }
        [HttpPost("{exercicioId}/importar-imagem")]
        public async Task<IActionResult> ImportarImagem(int exercicioId)
        {
            await _importacaoExercicioAplicacao.ImportarImagem(exercicioId);
            return NoContent();
        }


    }
}

