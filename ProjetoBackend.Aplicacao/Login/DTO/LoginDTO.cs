using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.Login.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
