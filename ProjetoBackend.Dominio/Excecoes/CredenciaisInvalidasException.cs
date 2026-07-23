namespace ProjetoBackend.Dominio.Excecoes
{
    /// <summary>
    /// Lançada em falha de autenticação. Convertida em HTTP 401 pelo middleware.
    /// </summary>
    public class CredenciaisInvalidasException : Exception
    {
        public CredenciaisInvalidasException(string mensagem) : base(mensagem) { }
    }
}
