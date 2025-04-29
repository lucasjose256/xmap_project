using xmap_project.Modules;

namespace xmap_project.Controllers;

using Microsoft.AspNetCore.Mvc;
using xmap_project.Services.Interfaces;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AtividadeController : Controller
{
    private readonly IAtividadeService _atividadeService;

    public AtividadeController(IAtividadeService atividadeService)
    {
        _atividadeService = atividadeService;
    }

    [HttpPost("{processId}/atividade")]
    public async Task<IActionResult> AddAtividade(int processId, [FromBody] CreateAtividadeRequest request)
    {
        var (success, message, atividadeId) = await _atividadeService.AddAtividadeAsync(processId, request);

        if (!success)
            return NotFound(message);

        return Ok(new { message, atividadeId });
    }

    [HttpGet("getAtividades")]
    public async Task<IActionResult> GetAtividadesTask()
    {
        var atividades = await _atividadeService.GetAtividadesAsync();
        return Ok(atividades);
    }

 
}