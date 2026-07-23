using ProjetoBackend.Aplicacao.IARelatorios.Interfaces;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.IARelatorios.Aplicacao
{
    public class IARelatorioAplicacao : IIARelatorioAplicacao
    {
        private readonly IIARelatorioRepositorio _iaRelatorioRepositorio;

        public IARelatorioAplicacao(IIARelatorioRepositorio iaRelatorioRepositorio)
        {
            _iaRelatorioRepositorio = iaRelatorioRepositorio;
        }

        public async Task<int> AdicionarRelatorio(IARelatorio relatorio)
        {
            ValidarRelatorio(relatorio);
            return await _iaRelatorioRepositorio.AdicionarRelatorio(relatorio);
        }

        public async Task<IEnumerable<IARelatorio>> ListarRelatoriosPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            return await _iaRelatorioRepositorio.ListarRelatorios(usuarioId);
        }

        public async Task<IARelatorio?> ObterUltimoRelatorio(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            return await _iaRelatorioRepositorio.ObterUltimoRelatorio(usuarioId);
        }

        private void ValidarRelatorio(IARelatorio relatorio)
        {
            if (relatorio == null)
                throw new ArgumentNullException(nameof(relatorio));

            if (relatorio.UsuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            if (string.IsNullOrWhiteSpace(relatorio.Relatorio))
                throw new ArgumentException("Relatório é obrigatório.");

            if (relatorio.Relatorio.Length < 10)
                throw new ArgumentException("Relatório muito curto.");

            if (relatorio.Relatorio.Length > 8000)
                throw new ArgumentException("Relatório muito longo. Máximo 8000 caracteres.");
        }
    }
}