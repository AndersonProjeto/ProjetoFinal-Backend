namespace ProjetoBackend.Aplicacao.DTOs.Treino
{
    public class TreinoResumoDTO
    {
        public int TreinoId { get; set; }
        public string NomeTreino { get; set; }
        public int TotalExercicios { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}