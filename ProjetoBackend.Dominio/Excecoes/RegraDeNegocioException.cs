namespace ProjetoBackend.Dominio.Excecoes
{
    /// <summary>
    /// Lançada quando uma regra de negócio é violada. Convertida em HTTP 400 pelo middleware.
    /// </summary>
    public class RegraDeNegocioException : Exception
    {
        public RegraDeNegocioException(string mensagem) : base(mensagem) { }
    }
}
