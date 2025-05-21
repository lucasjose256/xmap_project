using System.Xml;
using Microsoft.AspNetCore.Mvc;
using xmap_project.Modules;
using xmap_project.Services.Interfaces;

namespace xmap_project.Controllers;
[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _processService;

    public ProcessController(IProcessService processService)
    {
        _processService = processService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProcess([FromBody] CreateProcessRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("O nome é obrigatório.");

        var process = await _processService.CreateProcessAsync(request);
        return Ok(new { message = "Processo cadastrado com sucesso!", process.id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var processes = await _processService.GetAllProcessesAsync();
        return Ok(processes);
    }

    [HttpGet("xml/{id}")]
    public async Task<IActionResult> GetXml(int id)
    {
        var xml = await _processService.GetXmlAsync(id);
        if (xml == null)
            return NotFound("Processo não encontrado.");

        return Ok(xml);
    }

    [HttpPost("atividade/{atividadeId}/metadado")]
    public async Task<IActionResult> AddMetaDado(int atividadeId, [FromBody] MetaDadosRequest request)
    {
        var result = await _processService.AddMetaDadoAsync(atividadeId, request);
        if (result == null)
            return NotFound("Atividade não encontrada.");

        return Ok(result);
    }

    [HttpGet("getMetadados")]
    public async Task<IActionResult> GetMetadadosTask()
    {
        var metadados = await _processService.GetMetadadosAsync();
        return Ok(metadados);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcess(int id)
    {
        var success = await _processService.DeleteProcessAsync(id);
        if (!success)
            return NotFound("Processo não encontrado.");

        return Ok(new { message = "Processo deletado com sucesso!" });
    }
}
