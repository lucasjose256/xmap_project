using System.Xml;
using Microsoft.AspNetCore.Mvc;
using xmap_project.Data;
using xmap_project.Modules;

namespace xmap_project.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProcessController(AppDbContext context)
    {
        _context = context;
    }

    public class CreateProcessRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? UserLastEdited { get; set; }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProcess([FromBody] CreateProcessRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("O nome é obrigatório.");
        string caminhoArquivo = "pizza-collaboration.bpmn";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(caminhoArquivo); // Carrega o arquivo XML
        string conteudoXml = xmlDoc.OuterXml; // Obtém o XML como string
        Console.WriteLine(conteudoXml);
        var process = new Process
        {
            name = request.Name,
            description = request.Description,
            lastEdiTime = DateTime.UtcNow,
            userLastEdited = request.UserLastEdited,
            urlProcess = "localhost:xxxx/process/matricula",
            xmlDiagrama = conteudoXml,
            atividades = new List<Atividade>()
        };
        _context.process.Add(process);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Processo cadastrado com sucesso!", process.id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var processes = await _context.process
            .Select(p => new
            {
                p.id,
                p.name,
                p.description,
                p.lastEdiTime,
                p.userLastEdited
            })
            .ToListAsync();

        return Ok(processes);
    }
 
 
    [HttpPost("atividade/{atividadeId}/metadado")]
    public async Task<IActionResult> AddMetaDado(int atividadeId, [FromBody] MetaDadosRequest request)
    {
        var atividade = await _context.atividade
            .Include(a => a.metaDados)
            .FirstOrDefaultAsync(a => a.id == atividadeId);

        if (atividade == null)
            return NotFound("Atividade não encontrada.");

        var metaDado = new MetaDados
        {
            dados = request.Dados,
            ator = request.Ator,
            lgpd = request.Lgpd,
            atividadeId = atividadeId
        };

        _context.metaDados.Add(metaDado);
        await _context.SaveChangesAsync();
    
        return Ok(new { message = "Metadado adicionado com sucesso!", metaDadoId = metaDado.id });
    }
    [HttpGet("xml/{id}")]
    public async Task<IActionResult> GetXml(int id)
    {
        var process = await _context.process.FindAsync(id);
        if (process == null)
            return NotFound("Processo não encontrado.");

        return Ok(process.xmlDiagrama);
    }
    
 
    [HttpGet("getMetadados")]
    public async Task<IActionResult> GetMetadadosTask()
    {
        var processes = await _context.metaDados
            .Select(p => new
            { p.id,
           p.ator,
           p.dados,
           p.lgpd,
            })
            .ToListAsync();

        return Ok(processes);
    }
    // Deletar processo
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcess(int id)
    {
        var process = await _context.process.FindAsync(id);
        if (process == null)
            return NotFound("Processo não encontrado.");

        _context.process.Remove(process);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Processo deletado com sucesso!" });
    }
   
    
    
 
    
    
}
