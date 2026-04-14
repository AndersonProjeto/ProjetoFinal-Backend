using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;
using ProjetoBackend.Services.IAServices;

namespace ProjetoBackend.API.Controllers.IARelatorio
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IARelatorioController : ControllerBase
    {
        private readonly IARelatorioService _relatorioService;
        private readonly IIARelatorioRepositorio _relatorioRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEvolucaoRepositorio _evolucaoRepositorio;
        private readonly ITreinoRepositorio _treinoRepositorio;

        public IARelatorioController(
            IARelatorioService relatorioService,
            IIARelatorioRepositorio relatorioRepositorio,
            IUsuarioRepositorio usuarioRepositorio,
            IEvolucaoRepositorio evolucaoRepositorio,
            ITreinoRepositorio treinoRepositorio)
        {
            _relatorioService = relatorioService;
            _relatorioRepositorio = relatorioRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _evolucaoRepositorio = evolucaoRepositorio;
            _treinoRepositorio = treinoRepositorio;
        }

        // POST api/IARelatorio/gerar/{usuarioId}
        [HttpPost("gerar/{usuarioId}")]
        public async Task<IActionResult> Gerar(int usuarioId)
        {
            try
            {
                // 1. Busca usuário
                var usuario = await _usuarioRepositorio.ObterPorID(usuarioId);
                if (usuario == null)
                    return NotFound(new { mensagem = "Usuário não encontrado." });

                // 2. Busca histórico de evolução — usa o DTO diretamente (sem converter para entidade)
                var evolucoesDtos = (await _evolucaoRepositorio.HistoricoDeEvolucaoDoUsuario(usuarioId))
                    .Where(e => e != null)
                    .Cast<EvolucaoHistoricoDTO>()
                    .ToList();

                // 3. Busca treinos do usuário
                var treinos = await _treinoRepositorio.ListarPorUsuario(usuarioId);

                // 4. Busca resumo dos treinos (com total de exercícios)
                var treinosResumo = await _treinoRepositorio.ObterResumoTreinos();
                var treinosResumoDoUsuario = treinosResumo
                    .Where(r => treinos.Any(t => t.TreinoId == r.TreinoId))
                    .ToList();

                // 5. Gera relatório com IA
                var relatorioJson = await _relatorioService.GerarRelatorioAsync(
                    usuario,
                    usuarioId,
                    evolucoesDtos,
                    treinos,
                    treinosResumoDoUsuario
                );

                // 6. Salva no banco
                var entidade = new Dominio.Entidade.IARelatorio(usuarioId, relatorioJson);
                await _relatorioRepositorio.AdicionarRelatorio(entidade);

                // 7. Retorna
                return Ok(new { relatorio = relatorioJson });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao gerar relatório.", detalhe = ex.Message });
            }
        }

        // GET api/IARelatorio/ultimo/{usuarioId}
        [HttpGet("ultimo/{usuarioId}")]
        public async Task<IActionResult> ObterUltimo(int usuarioId)
        {
            try
            {
                var relatorio = await _relatorioRepositorio.ObterUltimoRelatorio(usuarioId);

                if (relatorio == null)
                    return NotFound(new { mensagem = "Nenhum relatório encontrado." });

                return Ok(new
                {
                    relatorio = relatorio.Relatorio,
                    dataGerado = relatorio.DataGerado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar relatório.", detalhe = ex.Message });
            }
        }

        // GET api/IARelatorio/{usuarioId}
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> Listar(int usuarioId)
        {
            try
            {
                var relatorios = await _relatorioRepositorio.ListarRelatorios(usuarioId);

                var resultado = relatorios.Select(r => new
                {
                    r.IARelatorioId,
                    r.Relatorio,
                    r.DataGerado
                });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar relatórios.", detalhe = ex.Message });
            }
        }
    }
}