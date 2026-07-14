using ProjetoBackend.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.DTOs.Exercicio
{
    public class ExercicioResumoDto
    {
        public int ExercicioId { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public EnumGrupoMuscular GrupoMuscular { get; private set; }
        public string Equipamento { get; private set; } = string.Empty;
        public int TotalTreinos { get; set; }
    }
}
