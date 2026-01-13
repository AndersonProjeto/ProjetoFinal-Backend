using ProjetoBackend.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IIAInteracaoRepositorio
    {
        Task<int> AdicionarIAInteracao(IAInteracao iaInteracao);
        Task<IEnumerable<IAInteracao>> ListarIAInteracoesPorUsuario(int usuarioId);
        Task<IAInteracao?> ObterUltimaInteracao(int usuarioId);

    }
}
