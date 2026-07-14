namespace ProjetoBackend.Dominio.Excecoes
{
    /// <summary>
    /// Lançada quando um recurso solicitado não existe. Convertida em HTTP 404 pelo middleware.
    /// </summary>
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException(string mensagem) : base(mensagem) { }
    }
}
