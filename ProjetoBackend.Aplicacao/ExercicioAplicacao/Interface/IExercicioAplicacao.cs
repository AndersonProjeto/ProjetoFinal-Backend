using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.Exercicio.Interface
{
    public interface IExercicioAplicacao
    {
        Task<int> AdicionarExercicio(AdicionarExercicioDTO adicionarExercicioDTO );
        Task AtualizarExercicio(AtualizarExercicioDTO atualizarExercicioDTO);
        Task DeletarExercicio(int exercicioId);
        Task<Dominio.Entidade.Exercicio> ObterPorID(int exercicioId);
        Task<IEnumerable<Dominio.Entidade.Exercicio>> ObterTodosExercicios();
        Task<IEnumerable<Dominio.Entidade.Exercicio>> ListarPorGrupoMuscular(string grupoMuscular);
        Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId);
        Task<ExercicioDetalhadoDto?> ObterExercicioDetalhado(int exercicioId);


    }
}
