using ProjetoBackend.Dominio.ProjetoBackend.Dominio;

namespace ProjetoBackend.Dominio
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
            UsuarioId = usuarioId;
            NomeTreino = nomeTreino;
            DataCriacao = DateTime.UtcNow;

            TreinoExercicios = new List<TreinoExercicio>();
        }
        public void AtualizarNome(string Novonome)
        {
            if (!string.IsNullOrWhiteSpace(Novonome))
            {
                throw new ArgumentException("O nome do treino não pode ser vazio ou nulo");
            }
            NomeTreino = Novonome;
        }
    }
}
