using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Aplicacao.Exercicio.Interface;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Enum;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.Exercicio.Aplicacao
{
    public class ExercicioAplicacao : IExercicioAplicacao
    {
        private readonly IExercicioRepositorio _exercicioRepositorio;

        public ExercicioAplicacao(IExercicioRepositorio exercicioRepositorio)
        {
            _exercicioRepositorio = exercicioRepositorio;
        }

        public async Task<int> AdicionarExercicio(AdicionarExercicioDTO dto)
        {
            var exercicio = new Dominio.Entidade.Exercicio(
                dto.Nome,
                (EnumGrupoMuscular)dto.GrupoMuscular,
                dto.Equipamento,
                dto.Descricao
            );

            return await _exercicioRepositorio.AdicionarExercicio(exercicio);
        }

        public async Task AtualizarExercicio(AtualizarExercicioDTO dto)
        {
         
            var exercicioExistente = await _exercicioRepositorio.ObterPorID(dto.ExercicioId);

            if (exercicioExistente == null)
                throw new Exception("Exercício não encontrado.");

            exercicioExistente.Atualizar(
                dto.Nome,
               (EnumGrupoMuscular)dto.GrupoMuscular,
                dto.Equipamento,
                dto.Descricao,
                dto.ImagemUrl
                );



            await _exercicioRepositorio.AtualizarExercicio(exercicioExistente);
        }

        public async Task DeletarExercicio(int exercicioId)
        {
            var exercicio = await _exercicioRepositorio.ObterPorID(exercicioId);

            if (exercicio == null)
                throw new Exception("Exercício não encontrado.");

            await _exercicioRepositorio.DeletarExercicio(exercicioId);
        }

        public async Task<ExercicioDetalhadoDto?> ObterExercicioDetalhado(int exercicioId)
        {
            
            if (exercicioId <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }
               
                return await _exercicioRepositorio.ObterExercicioDetalhado(exercicioId);
        }

        public async Task<Dominio.Entidade.Exercicio> ObterPorID(int exercicioId)
        {
            var exercicio = await _exercicioRepositorio.ObterPorID(exercicioId);

            if (exercicio == null)
            {
                throw new Exception("Exercício não encontrado.");
            }

            return await _exercicioRepositorio.ObterPorID(exercicioId);
        }

        public async Task<IEnumerable<Dominio.Entidade.Exercicio>> ObterTodosExercicios()
        {
            
            return await _exercicioRepositorio.ObterTodosExercicios();
        }

        public async Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId)
        {
            return await _exercicioRepositorio.TotalTreinosPorExercicio(exercicioId);
        }

        public async Task<IEnumerable<Dominio.Entidade.Exercicio>> ListarPorGrupoMuscular(EnumGrupoMuscular grupoMuscular)
        {
            return await _exercicioRepositorio.ListarPorGrupoMuscular(grupoMuscular);
        }

        public async Task<PaginaResultado<Dominio.Entidade.Exercicio>> ObterExerciciosPaginados(int pagina, int tamanhoPagina)
        {
            return await _exercicioRepositorio.ObterExerciciosPaginados(pagina, tamanhoPagina);

        }
    }
}