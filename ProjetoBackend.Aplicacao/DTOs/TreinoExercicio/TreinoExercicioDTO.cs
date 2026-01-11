namespace ProjetoBackend.Aplicacao.DTOs.TreinoExercicio
{
    public class TreinoExercicioDTO
    {
        public int TreinoExercicioId { get; set; }
        public int TreinoId { get; set; }
        public int ExercicioId { get; set; }

        public string NomeExercicio { get; set; }
        public string GrupoMuscular { get; set; }
        public string Equipamento { get; set; }

        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public int DescansoSegundos { get; set; }

    }
}