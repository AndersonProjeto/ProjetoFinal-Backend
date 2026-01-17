using ProjetoBackend.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBackend.Aplicacao.IAInteracoes.Interfaces
{
    public interface IIAInteracaoAplicacao
    {
        Task<int> AdicionarIAInteracao(IAInteracao interacao);
        Task<IEnumerable<IAInteracao>> ListarIAInteracoesPorUsuario(int usuarioId);
        Task<IAInteracao?> ObterUltimaInteracao(int usuarioId);
    }
}
