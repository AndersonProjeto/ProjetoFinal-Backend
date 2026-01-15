namespace ProjetoBackend.Dominio.Entidade
{

    public class TreinoExercicio
    {

        public int TreinoExercicioId { get; private set; }
        public int TreinoId { get; private set; }
        public int ExercicioId { get; private set; }
        public int Series { get; private set; }
        public int Repeticoes { get; private set; }
        public int DescansoSegundos { get; private set; }

        public Treino Treino { get; private set; }
        public Exercicio Exercicio { get; private set; }

        protected TreinoExercicio() { }

        public TreinoExercicio(int treinoId,int exercicioId,int series,int repeticoes,int descansoSegundos)
        {
            if (treinoId <= 0)
                throw new ArgumentException("Treino inválido");

            if (exercicioId <= 0)
                throw new ArgumentException("Exercício inválido");

            if (series <= 0)
                throw new ArgumentException("Séries inválidas");

            if (repeticoes <= 0)
                throw new ArgumentException("Repetições inválidas");

            if (descansoSegundos < 0)
                throw new ArgumentException("Descanso inválido");

            TreinoId = treinoId;
            ExercicioId = exercicioId;
            Series = series;
            Repeticoes = repeticoes;
            DescansoSegundos = descansoSegundos;
        }
        public void AtualizarDados(int series, int repeticoes, int descansoSegundos)
        {
            if (series <= 0)
                throw new ArgumentException("Séries inválidas");
            if (repeticoes <= 0)
                throw new ArgumentException("Repetições inválidas");
            if (descansoSegundos < 0)
                throw new ArgumentException("Descanso inválido");
            Series = series;
            Repeticoes = repeticoes;
            DescansoSegundos = descansoSegundos;
        }


    }
}


