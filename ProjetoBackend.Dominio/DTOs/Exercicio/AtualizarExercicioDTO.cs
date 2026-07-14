using ProjetoBackend.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Dominio.DTOs.Exercicio
{
    public class AtualizarExercicioDTO
    {
        public int ExercicioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumGrupoMuscular GrupoMuscular { get; set; }
        public string Equipamento { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;
    }
}
