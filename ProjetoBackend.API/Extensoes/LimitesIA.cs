namespace ProjetoBackend.API.Extensoes
{
    /// <summary>
    /// Limites aplicados na borda antes de encaminhar texto para as integracoes de
    /// IA. O rate limit contem a frequencia das chamadas; isto contem o tamanho de
    /// cada uma, para que um payload gigante nao seja repassado ao provedor.
    /// </summary>
    public static class LimitesIA
    {
        /// <summary>
        /// Folgado para uma pergunta de usuario e ainda bem abaixo da janela do modelo.
        /// </summary>
        public const int TamanhoMaximoPrompt = 4000;
    }
}
