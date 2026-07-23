using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.Seguranca
{
    public class SenhaHashAplicacao : ISenhaHashAplicacao
    {
        public string GerarHash(string senha)
        {
           return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarHash(string senha, string hash)
        {
           return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
