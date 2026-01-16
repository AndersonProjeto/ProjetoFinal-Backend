using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Aplicacao.EvolucaoAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.EvolucaoAplicacao.Aplicacao
{
    public class EvolucaoAplicacao : IEvolucaoAplicacao
    {
        private readonly IEvolucaoRepositorio _evolucaoRepositorio;

        public EvolucaoAplicacao(IEvolucaoRepositorio evolucaoRepositorio)
        {
            _evolucaoRepositorio = evolucaoRepositorio;
        }

        public async Task<int> AdicionarEvolucao(AdicionarEvolucaoDTO adicionarEvolucaoDTO)
        {
            var evolucao = new Evolucao(
                adicionarEvolucaoDTO.UsuarioId,
                adicionarEvolucaoDTO.PesoKg,
                adicionarEvolucaoDTO.CinturaCm,
                adicionarEvolucaoDTO.BracoCm,
                adicionarEvolucaoDTO.CoxaCm
            );

            return await _evolucaoRepositorio.AdicionarEvolucao(evolucao);
        }

        public async Task AtualizarEvolucao(AtualizarEvolucaoDTO atualizarEvolucaoDTO)
        {
            var evolucaoExistente = await _evolucaoRepositorio.ObterPorId(atualizarEvolucaoDTO.UsuarioId);

            if (evolucaoExistente == null)
                throw new Exception("Evolução não encontrada.");

            var evolucaoAtualizada = new Evolucao(
                atualizarEvolucaoDTO.UsuarioId,
                atualizarEvolucaoDTO.PesoKg,
                atualizarEvolucaoDTO.CinturaCm,
                atualizarEvolucaoDTO.BracoCm,
                atualizarEvolucaoDTO.CoxaCm
            );

  
            await _evolucaoRepositorio.AtualizarEvolucao(evolucaoAtualizada);
        }

        public async Task<Evolucao?> ObterUltimaEvolucao(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            return await _evolucaoRepositorio.ObterUltimaEvolucao(usuarioId);
        }

        public async Task<IEnumerable<EvolucaoResumoDTO?>> ResumoEvolucao(int usuarioId)
        {
            return await _evolucaoRepositorio.ResumoEvolucao(usuarioId);
        }

        public async Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId)
        {
            return await _evolucaoRepositorio.HistoricoDeEvolucaoDoUsuario(usuarioId);
        }

        public async Task<decimal> ObterPesoInicial(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterPesoInicial(usuarioId);
        }

        public async Task<decimal> ObterDiferencaPeso(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterDiferencaPeso(usuarioId);
        }
    }
}