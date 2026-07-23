using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Aplicacao.IARelatorios.Interfaces
{
    public interface IIARelatorioAplicacao
    {
        Task<int> AdicionarRelatorio(IARelatorio relatorio);
        Task<IEnumerable<IARelatorio>> ListarRelatoriosPorUsuario(int usuarioId);
        Task<IARelatorio?> ObterUltimoRelatorio(int usuarioId);
    }
}