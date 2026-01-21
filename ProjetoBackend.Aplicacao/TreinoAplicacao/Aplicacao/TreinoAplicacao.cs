using ProjetoBackend.Aplicacao.DTOs.Treino;
using ProjetoBackend.Aplicacao.TreinoAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.Treino;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.Treino.Aplicacao
{
    public class TreinoAplicacao : ITreinoAplicacao
    {
        private readonly ITreinoRepositorio _treinoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public TreinoAplicacao(ITreinoRepositorio treinoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _treinoRepositorio = treinoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<int> AdicionarTreino(AdicionarTreinoDTO dto)
        {
 
            var usuario = await _usuarioRepositorio.ObterPorID(dto.UsuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado. Não é possível criar um treino.");
            }

           
            var treino = new Dominio.Entidade.Treino(
                dto.UsuarioId,
                dto.NomeTreino

            );

            return await _treinoRepositorio.AdicionarTreino(treino);
        }

        public async Task AtualizarTreino(AtualizarTreinoDTO dto)
        {
            var treinoExistente = await _treinoRepositorio.ObterPorId(dto.TreinoId);
                 
            if (treinoExistente == null)
            {
                throw new Exception("Treino não encontrado.");
            }
           treinoExistente.AtualizarNome(dto.NomeTreino);
            await _treinoRepositorio.AtualizarTreino(treinoExistente);
        }

        public async Task DeletarTreino(int treinoId)
        {
            var treino = await _treinoRepositorio.ObterPorId(treinoId);
            if (treino == null)
            {
                throw new Exception("Treino não encontrado.");
            }

            await _treinoRepositorio.DeletarTreino(treinoId);
        }

        public async Task<Dominio.Entidade.Treino?> ObterPorId(int treinoId)
        {
            var treino = await _treinoRepositorio.ObterPorId(treinoId);

            if (treino == null)
                throw new Exception("Treino não encontrado.");

            treino.GetType()
                  .GetProperty("DataCriacao")?
                  .SetValue(
                      treino,
                      DateTime.SpecifyKind(treino.DataCriacao, DateTimeKind.Utc)
                  );

            return treino;
        }


        public async Task<IEnumerable<TreinoPorUsuarioDTO>> ListarPorUsuario(int usuarioId)
        {
            var usuario = await _usuarioRepositorio.ObterPorID(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            var treinos = await _treinoRepositorio.ListarPorUsuario(usuarioId);

            return treinos.Select(t => new TreinoPorUsuarioDTO
            {
                TreinoId = t.TreinoId,
                NomeTreino = t.NomeTreino,
                DataCriacao = DateTime.SpecifyKind(
                    t.DataCriacao,
                    DateTimeKind.Utc
                )
            });
        }
        public async Task<IEnumerable<TreinoResumoDTO>> ObterResumoTreinos()
        {
            var treinos = await _treinoRepositorio.ObterResumoTreinos();

            return treinos.Select(t => new TreinoResumoDTO
            {
                TreinoId = t.TreinoId,
                NomeTreino = t.NomeTreino,
                TotalExercicios = t.TotalExercicios,
                DataCriacao = DateTime.SpecifyKind(
                    t.DataCriacao,
                    DateTimeKind.Utc
                )
            });
        }

        public async Task<int> ObterTotalExercicios(int treinoId)
        { 
            var treino = await _treinoRepositorio.ObterPorId(treinoId);
             
            if (treino == null)
            {
                throw new Exception("Treino não encontrado.");
            }
            return await _treinoRepositorio.TotalExerciciosDoTreino(treinoId);
        }
    }
}