using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Dominio.DTOs.TreinoExercicio
{
    public class AtualizarTreinoExercicioDTO
    {
        public int TreinoExercicioId { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public int DescansoSegundos { get; set; }
    }
}
