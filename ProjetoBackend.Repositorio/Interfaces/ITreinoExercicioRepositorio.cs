using ProjetoBackend.Aplicacao.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface ITreinoExercicioRepositorio
    {
        Task<int> AdicionarTreinoExercicio(TreinoExercicio treinoExercicio);
        Task AtualizarTreinoExercicio(TreinoExercicio treinoExercicio);
        Task DeletarTreinoExercicio(int treinoExercicioId);
        Task<IEnumerable<TreinoExercicioDTO>> ListarTreino(int treinoId);
    }
}
