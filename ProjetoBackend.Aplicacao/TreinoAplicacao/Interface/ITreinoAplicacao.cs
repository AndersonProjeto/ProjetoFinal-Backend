using ProjetoBackend.Aplicacao.DTOs.Treino;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.DTOs.Treino;

namespace ProjetoBackend.Aplicacao.TreinoAplicacao.Interface
{
    public interface ITreinoAplicacao
    {

        Task<int> AdicionarTreino(AdicionarTreinoDTO adicionarTreinoDTO);
        Task AtualizarTreino(AtualizarTreinoDTO atualizarTreinoDTO);
        Task DeletarTreino(int treinoId);
        Task<ProjetoBackend.Dominio.Entidade.Treino?> ObterPorId(int treinoId);
        Task<IEnumerable<TreinoPorUsuarioDTO>> ListarPorUsuario(int usuarioId);
        Task<IEnumerable<TreinoResumoDTO>> ObterResumoTreinos();
        Task<int> ObterTotalExercicios(int treinoId);
    }
}