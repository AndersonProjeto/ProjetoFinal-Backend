using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBackend.Dominio.DTOs
{
    public class AdicionarIAInteracaoDTO
    {
        public int UsuarioId { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
    }
}
