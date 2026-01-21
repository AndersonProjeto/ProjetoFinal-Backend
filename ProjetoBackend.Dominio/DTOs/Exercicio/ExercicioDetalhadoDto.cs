using ProjetoBackend.Dominio.Enum;

namespace ProjetoBackend.Dominio.DTOs.Exercicio
{
    public class ExercicioDetalhadoDto
    {
        public int ExercicioId { get; set; }
        public string Nome { get; set; } = null!;
        public EnumGrupoMuscular GrupoMuscular { get; set; } 
        public string Equipamento { get; set; } = null!;

        public int TotalTreinos { get; set; }
        public int TotalSeries { get; set; }
        public int TotalRepeticoes { get; set; }
    }
}