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



        public ICollection<TreinoExercicio> TreinoExercicios { get; private set; }

        protected Exercicio() { }

        public Exercicio(string nome, EnumGrupoMuscular grupoMuscular, string equipamento, string? descricao = null, string? imagemUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do exercício é obrigatório");

            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Equipamento = equipamento;
            Descricao = descricao;
            ImagemUrl = imagemUrl;

            TreinoExercicios = new List<TreinoExercicio>();
        }
        public void Atualizar(
       string nome,
       EnumGrupoMuscular grupoMuscular,
       string equipamento,
       string? descricao, string? imagemUrl = null)
        {
            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Equipamento = equipamento;
            Descricao = descricao;
            ImagemUrl = imagemUrl;
        }
        public void AtualizarImagem(string imagemUrl)
        {
            ImagemUrl = imagemUrl;
        }
    }
}