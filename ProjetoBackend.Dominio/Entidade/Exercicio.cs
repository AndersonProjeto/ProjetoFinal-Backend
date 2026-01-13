namespace ProjetoBackend.Dominio.Entidade
{
    public class Exercicio
    {
        public int ExercicioId { get; private set; }
        public string Nome { get; private set; }
        public string GrupoMuscular { get; private set; }
        public string Equipamento { get; private set; }

        public ICollection<TreinoExercicio> TreinoExercicios { get; private set; }

        protected Exercicio() { }

        public Exercicio(string nome,string grupoMuscular,string equipamento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do exercício é obrigatório");

            Nome = nome;
            GrupoMuscular = grupoMuscular;
            Equipamento = equipamento;

            TreinoExercicios = new List<TreinoExercicio>();
        }
    }
}