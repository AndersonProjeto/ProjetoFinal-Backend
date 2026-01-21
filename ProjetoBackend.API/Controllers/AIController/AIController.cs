using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Services.IAServices;

namespace ProjetoBackend.API.Controllers.AIController
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
            var resposta = await _aiService.GetAiResponseAsync(prompt);
            return Ok(resposta);
        }
    }
}
