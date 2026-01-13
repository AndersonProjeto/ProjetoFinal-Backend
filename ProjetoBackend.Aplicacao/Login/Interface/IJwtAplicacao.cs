using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Aplicacao.Login.Interface
{
    public interface IJwtAplicacao
    {
        string GerarToken(Usuario usuario);
    }
}