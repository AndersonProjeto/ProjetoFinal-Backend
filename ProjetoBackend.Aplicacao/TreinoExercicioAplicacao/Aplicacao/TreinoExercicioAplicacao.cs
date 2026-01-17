using ProjetoBackend.Aplicacao.DTOs.TreinoExercicio;
using ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.TreinoExercicioAplicacao.Aplicacao
{
    public class TreinoExercicioAplicacao : ITreinoExercicioAplicacao
    {
        private readonly ITreinoExercicioRepositorio _treinoExercicioRepositorio;
        public TreinoExercicioAplicacao(ITreinoExercicioRepositorio treinoExercicioRepositorio)
        {
            _treinoExercicioRepositorio = treinoExercicioRepositorio;
        }
        public async Task<int> AdicionarTreinoExercicio(AdicionarTreinoExercicioDTO dto)
        {
        
            var treinoExercicio = new TreinoExercicio(
                dto.TreinoId,
                dto.ExercicioId,
                dto.Series,
                dto.Repeticoes,
                dto.DescansoSegundos
            );

            return await _treinoExercicioRepositorio.AdicionarTreinoExercicio(treinoExercicio);
        }

        public async Task AtualizarTreinoExercicio(AtualizarTreinoExercicioDTO dto)
        {
           
            var treinoExercicio = await _treinoExercicioRepositorio.ObterPorID(dto.TreinoExercicioId);

            if (treinoExercicio == null)
                throw new Exception("Vínculo de exercício não encontrado para atualização.");

            treinoExercicio.AtualizarDados(
                dto.Series,
                dto.Repeticoes,
                dto.DescansoSegundos
            );

            await _treinoExercicioRepositorio.AtualizarTreinoExercicio(treinoExercicio);
        }

        public async Task DeletarTreinoExercicio(int treinoExercicioId)
        {
            var treinoExercicio = await _treinoExercicioRepositorio.ObterPorID(treinoExercicioId);

            if (treinoExercicio == null)
                throw new Exception("O registro informado não existe.");

            await _treinoExercicioRepositorio.DeletarTreinoExercicio(treinoExercicioId);
        }

        public async Task<IEnumerable<TreinoExercicioDTO>> ListarTreino(int treinoId)
        {
            if (treinoId <= 0)
                throw new ArgumentException("ID do treino é inválido.");

            return await _treinoExercicioRepositorio.ListarTreino(treinoId);
        }

        public async Task<TreinoExercicio?> ObterPorID(int TreinoExercicioId)
        {
            var treinoExercicio = _treinoExercicioRepositorio.ObterPorID(TreinoExercicioId);
            if (treinoExercicio == null)
            {
                throw new Exception("treinoExercicio não encontrado.");
            }
            return await _treinoExercicioRepositorio.ObterPorID(TreinoExercicioId);
        }
    }
}
