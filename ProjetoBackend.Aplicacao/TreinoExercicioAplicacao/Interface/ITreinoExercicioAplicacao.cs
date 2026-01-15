using ProjetoBackend.Aplicacao.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Interface
{
    public interface ITreinoExercicioAplicacao
    {
        Task<int> AdicionarTreinoExercicio(AdicionarTreinoExercicioDTO dto);
        Task AtualizarTreinoExercicio(AtualizarTreinoExercicioDTO dto);
        Task DeletarTreinoExercicio(int treinoExercicioId);
        Task<IEnumerable<TreinoExercicioDTO>> ListarTreino(int treinoId);

    }
}
