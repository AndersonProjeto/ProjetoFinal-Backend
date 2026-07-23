namespace ProjetoBackend.Services.DtoService
{
    public class GroqResponse
    {
        public List<GroqChoice>? Choices { get; set; }
    }

    public class GroqChoice
    {
        public GroqMessage? Message { get; set; }
    }

    public class GroqMessage
    {
        public string? Content { get; set; }
    }
}
