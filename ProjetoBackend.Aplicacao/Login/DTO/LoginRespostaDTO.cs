using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.Login.DTO
{
    public class LoginRespostaDTO
    {
        public string Token { get; set; }
        public DateTime TempoDeExpirarOToken { get; set; }
    }
}
