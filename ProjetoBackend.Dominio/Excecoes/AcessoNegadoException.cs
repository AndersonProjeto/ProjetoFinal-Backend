namespace ProjetoBackend.Dominio.Excecoes
{
    /// <summary>
    /// Lançada quando o usuário está autenticado mas o recurso pertence a outra pessoa.
    /// Convertida em HTTP 403 pelo middleware.
    /// </summary>
    public class AcessoNegadoException : Exception
    {
        public AcessoNegadoException(string mensagem) : base(mensagem) { }
    }
}
