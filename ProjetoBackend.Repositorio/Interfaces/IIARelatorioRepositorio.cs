using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IIARelatorioRepositorio
    {
        Task<int> AdicionarRelatorio(IARelatorio relatorio);
        Task<IARelatorio?> ObterUltimoRelatorio(int usuarioId);
        Task<IEnumerable<IARelatorio>> ListarRelatorios(int usuarioId);
    }
}