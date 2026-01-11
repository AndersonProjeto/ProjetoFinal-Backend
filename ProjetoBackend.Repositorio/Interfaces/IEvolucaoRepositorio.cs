using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IEvolucaoRepositorio
    {
        Task<int> AdicionarEvolucao(Evolucao evolucao);
        Task AtualizarEvolucao(Evolucao evolucao);

        Task<Evolucao?> ObterUltimaEvolucao(int usuarioId);
        Task<IEnumerable<EvolucaoResumoDTO?>> ResumoEvolucao(int usuarioId);
        Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId);
        Task<decimal> ObterPesoInicial(int usuarioId);
        Task<decimal> ObterDiferencaPeso(int usuarioId);
    }
}