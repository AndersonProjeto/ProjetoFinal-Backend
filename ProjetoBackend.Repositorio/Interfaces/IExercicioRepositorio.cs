using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Dominio.Enum;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IExercicioRepositorio
    {
        Task<int> AdicionarExercicio(Exercicio exercicio);
        Task AtualizarExercicio(Exercicio exercicio);
        Task DeletarExercicio(int exercicioId);
        Task<Exercicio> ObterPorID(int exercicioId);
        Task<IEnumerable<Exercicio>> ObterTodosExercicios();
        Task<IEnumerable<Exercicio>> ListarPorGrupoMuscular(EnumGrupoMuscular grupoMuscular);
        Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId);
        Task<ExercicioDetalhadoDto?> ObterExercicioDetalhado(int exercicioId);
        Task<PaginaResultado<Exercicio>> ObterExerciciosPaginados(int pagina, int tamanhoPagina);
    }
}
