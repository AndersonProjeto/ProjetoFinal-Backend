using ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao;
using ProjetoBackend.Repositorio.Interfaces;

public class ImportacaoExercicioAplicacao
{
    private readonly IExercicioRepositorio _exercicioRepositorio;
    private readonly ExerciseDbService _exerciseDbService;

    public ImportacaoExercicioAplicacao(
           IExercicioRepositorio exercicioRepositorio,
           ExerciseDbService exerciseDbService)
    {
        _exercicioRepositorio = exercicioRepositorio;
        _exerciseDbService = exerciseDbService;
    }

    public async Task ImportarImagem(int exercicioId)
    {
        var exercicio = await _exercicioRepositorio.ObterPorID(exercicioId);

        if (exercicio == null)
            throw new Exception("Exercício não encontrado");

        if (!string.IsNullOrEmpty(exercicio.ImagemUrl))
            return;

        var imagemUrl = await _exerciseDbService.BuscarImagemAsync(exercicio.GrupoMuscular);

        if (imagemUrl == null)
            return;

        Console.WriteLine("Imagem encontrada: " + imagemUrl);

        // ATUALIZA O OBJETO COM O LINK CORRETO
        exercicio.Atualizar(
            exercicio.Nome,
            exercicio.GrupoMuscular,
            exercicio.Equipamento,
            exercicio.Descricao,
            imagemUrl // <-- AQUI
        );

        Console.WriteLine("Imagem no objeto: " + exercicio.ImagemUrl);

        await _exercicioRepositorio.AtualizarExercicio(exercicio);
    }

}
