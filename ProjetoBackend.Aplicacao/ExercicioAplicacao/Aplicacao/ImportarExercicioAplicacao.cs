using Microsoft.Extensions.Logging;
using ProjetoBackend.Aplicacao.Exercicio.Interface;
using ProjetoBackend.Dominio.Excecoes;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao
{
    public class ImportacaoExercicioAplicacao : IImportacaoExercicioAplicacao
    {
        private readonly IExercicioRepositorio _exercicioRepositorio;
        private readonly ExerciseDbService _exerciseDbService;
        private readonly ILogger<ImportacaoExercicioAplicacao> _logger;

        public ImportacaoExercicioAplicacao(
               IExercicioRepositorio exercicioRepositorio,
               ExerciseDbService exerciseDbService,
               ILogger<ImportacaoExercicioAplicacao> logger)
        {
            _exercicioRepositorio = exercicioRepositorio;
            _exerciseDbService = exerciseDbService;
            _logger = logger;
        }

        public async Task ImportarImagem(int exercicioId)
        {
            var exercicio = await _exercicioRepositorio.ObterPorID(exercicioId);

            if (exercicio == null)
                throw new NaoEncontradoException("Exercício não encontrado");

            if (!string.IsNullOrEmpty(exercicio.ImagemUrl))
                return;

            var imagemUrl = await _exerciseDbService.BuscarImagemAsync(exercicio.GrupoMuscular);

            if (imagemUrl == null)
            {
                _logger.LogInformation(
                    "Nenhuma imagem encontrada para o exercício {ExercicioId} (grupo {GrupoMuscular}).",
                    exercicioId, exercicio.GrupoMuscular);
                return;
            }

            exercicio.Atualizar(
                exercicio.Nome,
                exercicio.GrupoMuscular,
                exercicio.Equipamento,
                exercicio.Descricao,
                imagemUrl
            );

            await _exercicioRepositorio.AtualizarExercicio(exercicio);

            _logger.LogInformation(
                "Imagem importada para o exercício {ExercicioId}.", exercicioId);
        }
    }
}
