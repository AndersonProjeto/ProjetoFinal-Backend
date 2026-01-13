namespace ProjetoBackend.Dominio.Entidade
{
    public class Treino
    {
        public int TreinoId { get; private set; }
        public int UsuarioId { get; private set; }
        public string NomeTreino { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Usuario Usuario { get; private set; }
        public ICollection<TreinoExercicio> TreinoExercicios { get; private set; }

        protected Treino() { }

        public Treino(int usuarioId, string nomeTreino)
        {
            if(string.IsNullOrWhiteSpace(nomeTreino))
                throw new ArgumentException("Nome do treino é obrigatório");
            UsuarioId = usuarioId;
            NomeTreino = nomeTreino;
            DataCriacao = DateTime.UtcNow;

            TreinoExercicios = new List<TreinoExercicio>();
        }
        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("O nome do treino não pode ser vazio.");

            NomeTreino = novoNome;
        }

    }
}
