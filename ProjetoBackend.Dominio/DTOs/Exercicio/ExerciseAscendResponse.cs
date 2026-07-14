using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBackend.Dominio.DTOs.Exercicio
{
    public class ExerciseDbResponse
    {
        public string ExerciseId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }

}
