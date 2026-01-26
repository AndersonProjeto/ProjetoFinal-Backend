using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IEvolucaoRepositorio
    {
        Task<int> AdicionarEvolucao(Evolucao evolucao);
        Task AtualizarEvolucao(Evolucao evolucao);
        Task<Evolucao?> ObterPorId(int evolucaoId);

        Task<Evolucao?> ObterUltimaEvolucao(int usuarioId);
        Task<EvolucaoResumoDTO?> ResumoEvolucao(int usuarioId);
        Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId);
        Task<decimal> ObterPesoInicial(int usuarioId);
        Task<decimal> ObterDiferencaPeso(int usuarioId);
        Task<decimal?> ObterCinturaInicial(int usuarioId);
        Task<decimal?> ObterDiferencaCintura(int usuarioId);

        Task<decimal?> ObterBracoInicial(int usuarioId);
        Task<decimal?> ObterDiferencaBraco(int usuarioId);

        Task<decimal?> ObterCoxaInicial(int usuarioId);
        Task<decimal?> ObterDiferencaCoxa(int usuarioId);

    }
}