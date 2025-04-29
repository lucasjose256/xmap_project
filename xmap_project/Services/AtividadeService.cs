using xmap_project.Services.Interfaces;

namespace xmap_project.Services;

using Microsoft.EntityFrameworkCore;
using xmap_project.Data;
using xmap_project.Modules;
using xmap_project.Controllers;



public class AtividadeService : IAtividadeService
{
    private readonly AppDbContext _context;

    public AtividadeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message, int? AtividadeId)> AddAtividadeAsync(int processId, CreateAtividadeRequest request)
    {
        var process = await _context.process
            .Include(p => p.atividades)
            .FirstOrDefaultAsync(p => p.id == processId);

        if (process == null)
            return (false, "Processo não encontrado.", null);

        var atividade = new Atividade
        {
            nomeAtividade = request.NomeAtividade,
            processId = processId,
            metaDados = new MetaDados
            {
                dados = request.MetaDados.Dados,
                ator = request.MetaDados.Ator,
                lgpd = request.MetaDados.Lgpd
            }
        };

        _context.atividade.Add(atividade);
        await _context.SaveChangesAsync();

        return (true, "Atividade adicionada com sucesso!", atividade.id);
    }

    public async Task<List<object>> GetAtividadesAsync()
    {
        var atividades = await _context.atividade
            .Select(p => new
            {
                p.id,
                p.nomeAtividade,
                p.metaDados,
                p.processId
            })
            .ToListAsync<object>();

        return atividades;
    }
}