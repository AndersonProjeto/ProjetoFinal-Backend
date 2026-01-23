using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.DTOs.Usuario
{
    public class AtualizarUsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal AlturaCm { get; set; }
        public string AvatarSeed { get; set; }
        public string AvatarEstilo { get; set; }
    }
}
