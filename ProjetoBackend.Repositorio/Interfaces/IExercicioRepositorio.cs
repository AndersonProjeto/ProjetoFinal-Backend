using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IExercicioRepositorio
    {
        Task<int> AdicionarExercicio(Exercicio exercicio);
        Task AtualizarExercicio(Exercicio exercicio);
        Task DeletarExercicio(int exercicioId);
        Task<Exercicio> ObterPorID(int exercicioId);
        Task<IEnumerable<Exercicio>> ObterTodosExercicios();
        Task<IEnumerable<Exercicio>> ListarPorGrupoMuscular(string grupoMuscular);
        Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId);
    }
}
