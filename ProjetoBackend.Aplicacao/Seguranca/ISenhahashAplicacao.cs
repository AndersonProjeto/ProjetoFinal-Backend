using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.Seguranca
{
    public interface ISenhahashAplicacao
    {
        string GerarHash(string senha);
        bool VerificarHash(string senha, string hash);
    }
}
