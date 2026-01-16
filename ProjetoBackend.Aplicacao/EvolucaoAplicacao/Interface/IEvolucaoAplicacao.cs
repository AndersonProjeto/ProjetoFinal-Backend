using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Aplicacao.EvolucaoAplicacao.Interface
{
    public interface IEvolucaoAplicacao
    {
        Task<int> AdicionarEvolucao(AdicionarEvolucaoDTO dto);
        Task AtualizarEvolucao(AtualizarEvolucaoDTO dto);

        Task<Evolucao?> ObterUltimaEvolucao(int usuarioId);
        Task<IEnumerable<EvolucaoResumoDTO?>> ResumoEvolucao(int usuarioId);
        Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId);

        Task<decimal> ObterPesoInicial(int usuarioId);
        Task<decimal> ObterDiferencaPeso(int usuarioId);
    }
}