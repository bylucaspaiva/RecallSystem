using Microsoft.AspNetCore.Mvc;
using RecallSystem.Application.DTOs;
using RecallSystem.Application.Interfaces;

namespace RecallSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecallController : ControllerBase
    {
        private readonly IRecallService _recallService;

        public RecallController(IRecallService recallService)
        {
            _recallService = recallService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecallDTO>>> GetRecalls()
        {
            var recalls = await _recallService.GetAllRecallsAsync();
            return Ok(recalls.Select(r => new RecallDTO
            {
                Id = r.Id,
                Titulo = r.Titulo,
                Descricao = r.Descricao,
                DataPublicacao = r.DataPublicacao
            }));
        }

        [HttpGet("chassi/{chassi}")]
        public async Task<ActionResult<IEnumerable<ExecucaoRecallDTO>>> GetRecallsPorChassi(string chassi)
        {
            var execucoes = await _recallService.GetRecallsByChassiAsync(chassi);

            if (!execucoes.Any())
            {
                return NotFound("Nenhum recall encontrado para o chassi informado.");
            }

            return Ok(execucoes.Select(e => new ExecucaoRecallDTO
            {
                Id = e.Id,
                RecallId = e.RecallId,
                Chassi = e.Chassi,
                DataExecucao = e.DataExecucao,
                Concessionaria = e.Concessionaria,
                Recall = new RecallDTO
                {
                    Id = e.Recall.Id,
                    Titulo = e.Recall.Titulo,
                    Descricao = e.Recall.Descricao,
                    DataPublicacao = e.Recall.DataPublicacao
                }
            }));
        }
    }
}
