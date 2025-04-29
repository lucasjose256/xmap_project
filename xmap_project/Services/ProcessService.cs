
using System.Xml;
using Microsoft.EntityFrameworkCore;
using xmap_project.Data;
using xmap_project.Modules;
using xmap_project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xmap_project.Services
{
    public class ProcessService : IProcessService
    {
        private readonly AppDbContext _context;

        public ProcessService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Process> CreateProcessAsync(CreateProcessRequest request)
        {
            string caminhoArquivo = "pizza-collaboration.bpmn";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(caminhoArquivo);
            string conteudoXml = xmlDoc.OuterXml;

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
            return process;
        }

        public async Task<IEnumerable<object>> GetAllProcessesAsync()
        {
            return await _context.process
                .Select(p => new
                {
                    p.id,
                    p.name,
                    p.description,
                    p.lastEdiTime,
                    p.userLastEdited
                })
                .ToListAsync();
        }

        public async Task<string> GetXmlAsync(int id)
        {
            var process = await _context.process.FindAsync(id);
            return process?.xmlDiagrama;
        }

        public async Task<object> AddMetaDadoAsync(int atividadeId, MetaDadosRequest request)
        {
            var atividade = await _context.atividade
                .Include(a => a.metaDados)
                .FirstOrDefaultAsync(a => a.id == atividadeId);

            if (atividade == null)
                return null;

            var metaDado = new MetaDados
            {
                dados = request.Dados,
                ator = request.Ator,
                lgpd = request.Lgpd,
                atividadeId = atividadeId
            };

            _context.metaDados.Add(metaDado);
            await _context.SaveChangesAsync();

            return new { message = "Metadado adicionado com sucesso!", metaDadoId = metaDado.id };
        }

        public async Task<IEnumerable<object>> GetMetadadosAsync()
        {
            return await _context.metaDados
                .Select(m => new
                {
                    m.id,
                    m.ator,
                    m.dados,
                    m.lgpd
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteProcessAsync(int id)
        {
            var process = await _context.process.FindAsync(id);
            if (process == null)
                return false;

            _context.process.Remove(process);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
