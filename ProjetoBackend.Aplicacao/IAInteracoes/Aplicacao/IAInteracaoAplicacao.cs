using ProjetoBackend.Aplicacao.IAInteracoes.Interfaces;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.IAInteracoes.Aplicacao
{
    public class IAInteracaoAplicacao : IIAInteracaoAplicacao
    {
        private readonly IIAInteracaoRepositorio _iaInteracaoRepositorio;

        public IAInteracaoAplicacao(IIAInteracaoRepositorio iaInteracaoRepositorio)
        {
            _iaInteracaoRepositorio = iaInteracaoRepositorio;
        }

        public async Task<int> AdicionarIAInteracao(IAInteracao interacao)
        {
            ValidarInteracao(interacao);
            return await _iaInteracaoRepositorio.AdicionarIAInteracao(interacao);
        }

        public async Task<IEnumerable<IAInteracao>> ListarIAInteracoesPorUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentException("Usuário inválido.");

            }

            return await _iaInteracaoRepositorio.ListarIAInteracoesPorUsuario(usuarioId);
        }
        public async Task<IEnumerable<IAInteracao>> ListarUltimasInteracoes(int usuarioId, int quantidade)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade inválida.");

            return await _iaInteracaoRepositorio.ListarUltimasInteracoes(usuarioId, quantidade);
        }


        public async Task<IAInteracao?> ObterUltimaInteracao(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentException("Usuário inválido.");
            }
              

            return await _iaInteracaoRepositorio.ObterUltimaInteracao(usuarioId);
        }


        private void ValidarInteracao(IAInteracao interacao)
        {
            if (interacao == null)
                throw new ArgumentNullException(nameof(interacao));

            if (interacao.UsuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            if (string.IsNullOrWhiteSpace(interacao.Pergunta))
                throw new ArgumentException("Pergunta é obrigatória.");

            if (interacao.Pergunta.Length > 500)
                throw new ArgumentException("Pergunta muito longa. Deve ter no máximo 500 caracteres.");

            if (string.IsNullOrWhiteSpace(interacao.Resposta))
                throw new ArgumentException("Resposta é obrigatória.");

            if (interacao.Resposta.Length < 5)
                throw new ArgumentException("Resposta muito curta. Deve ter no mínimo 5 caracteres.");

            if (interacao.Resposta.Length > 4000)
                throw new ArgumentException("Resposta muito longa. Deve ter no máximo 4000 caracteres.");
        }
    }
}
