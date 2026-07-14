namespace ProjetoBackend.Aplicacao.DTOs.TreinoExercicio
{
    public class TreinoExercicioDTO
    {
        public int TreinoExercicioId { get; set; }
        public int TreinoId { get; set; }
        public int ExercicioId { get; set; }

        public string NomeExercicio { get; set; } = string.Empty;
        public string GrupoMuscular { get; set; } = string.Empty;
        public string Equipamento { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public int DescansoSegundos { get; set; }

    }
}