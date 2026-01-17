using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBackend.Dominio.DTOs.IAInteracao
{
    public class PerguntarParaIADTO
    {
        public int UsuarioId { get; set; }
        public string Pergunta { get; set; } = string.Empty;
    }
}
