namespace ProjetoBackend.Dominio.DTOs.Exercicio
{
    public class ExerciseAscendResponse
    {
        public bool Success { get; set; }
        public List<ExerciseDbResponse> Data { get; set; } = new();
    }
}
