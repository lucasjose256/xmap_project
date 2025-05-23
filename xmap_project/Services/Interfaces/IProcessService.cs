using xmap_project.Modules;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace xmap_project.Services.Interfaces
{
    public interface IProcessService
    {
        Task<Processo> CreateProcessAsync(CreateProcessRequest request);
        Task<IEnumerable<object>> GetAllProcessesAsync();
        Task<string> GetXmlAsync(int id);
        Task<object> AddMetaDadoAsync(int atividadeId, MetaDadosRequest request);
        Task<IEnumerable<object>> GetMetadadosAsync();
        Task<bool> DeleteProcessAsync(int id);
    }
}
