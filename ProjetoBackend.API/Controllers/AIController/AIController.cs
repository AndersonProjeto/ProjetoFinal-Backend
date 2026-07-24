using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjetoBackend.API.Extensoes;
using ProjetoBackend.Services.IAServices;

namespace ProjetoBackend.API.Controllers.AIController
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting(RateLimitingExtensoes.PoliticaIa)]
    public class AiController : ControllerBase
    {
        private readonly IAService _aiService;
        public AiController(IAService aiService)
        {
            _aiService = aiService;
        }
        [HttpPost("completar")]
        public async Task<IActionResult> Completar([FromBody] string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return BadRequest(new { mensagem = "Prompt obrigatório." });

            if (prompt.Length > LimitesIA.TamanhoMaximoPrompt)
                return BadRequest(new { mensagem = $"Prompt excede o limite de {LimitesIA.TamanhoMaximoPrompt} caracteres." });

            var resposta = await _aiService.GetAiResponseAsync(prompt);
            return Ok(resposta);
        }
    }
}
