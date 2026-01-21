using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.DTOs.Treino
{
    public class TreinoPorUsuarioDTO
    {
        public  int TreinoId { get; set; }
        public string NomeTreino { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
