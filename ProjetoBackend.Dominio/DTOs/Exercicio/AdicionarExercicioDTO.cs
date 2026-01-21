using ProjetoBackend.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Dominio.DTOs.Exercicio
{
    public class  AdicionarExercicioDTO
    {
        public string Nome { get; set; }
        public EnumGrupoMuscular GrupoMuscular { get; set; }
        public string Equipamento { get; set; }
        public string? Descricao { get; set; }
    }
}
