using ProjetoBackend.Aplicacao.DTOs.Treino;
using ProjetoBackend.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface ITreinoRepositorio
    {
        Task<int> AdicionarTreino(Treino treino);
        Task AtualizarTreino(Treino treino);
        Task DeletarTreino(int treinoId);

        Task<Treino?> ObterPorId(int treinoId);
        Task<IEnumerable<TreinoPorUsuarioDTO>> ListarPorUsuario(int usuarioId);

        Task<IEnumerable<TreinoResumoDTO>> ObterResumoTreinos();
        Task<int> TotalExerciciosDoTreino(int treinoId);
        Task<int> TotalTreinosDoUsuario(int usuarioId);
    }
}
