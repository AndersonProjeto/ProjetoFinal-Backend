using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Dominio.DTOs.TreinoExercicio
{
    public class AdicionarTreinoExercicioDTO
    {
        public int TreinoId { get; set; }
        public int ExercicioId { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public int DescansoSegundos { get; set; }
    }
}
