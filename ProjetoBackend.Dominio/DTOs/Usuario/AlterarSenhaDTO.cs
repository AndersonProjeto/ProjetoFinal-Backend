using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Dominio.DTOs.Usuario
{
    public class AlterarSenhaDTO
    {
        public int UsuarioId { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
    }

}
