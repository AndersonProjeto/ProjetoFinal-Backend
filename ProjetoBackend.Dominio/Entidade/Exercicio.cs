using ProjetoBackend.Dominio.Enum;

namespace ProjetoBackend.Dominio.Entidade
{
    public class Exercicio
    {
        public int ExercicioId { get; private set; }
        public string Nome { get; private set; }
        public EnumGrupoMuscular GrupoMuscular { get; private set; }
        public string Equipamento { get; private set; }
        public string? Descricao { get; private set; }
        public string? ImagemUrl { get; private set; }
        public string? VideoUrl { get; private set; }

        public ICollection<TreinoExercicio> TreinoExercicios { get; private set; }

        protected Exercicio() { }

        public Exercicio(string nome, EnumGrupoMuscular grupoMuscular, string equipamento, string? descricao = null, string? imagemUrl = null, string? videoUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do exercício é obrigatório");

            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Equipamento = equipamento;
            Descricao = descricao;
            ImagemUrl = imagemUrl;
            VideoUrl = videoUrl;

            TreinoExercicios = new List<TreinoExercicio>();
        }

        public void Atualizar(
            string nome,
            EnumGrupoMuscular grupoMuscular,
            string equipamento,
            string? descricao,
            string? imagemUrl = null,
            string? videoUrl = null)
        {
            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Equipamento = equipamento;
            Descricao = descricao;
            ImagemUrl = imagemUrl;
            VideoUrl = videoUrl;
        }

        public void AtualizarImagem(string imagemUrl)
        {
            ImagemUrl = imagemUrl;
        }

        public void AtualizarVideo(string videoUrl)
        {
            VideoUrl = videoUrl;
        }
    }
}