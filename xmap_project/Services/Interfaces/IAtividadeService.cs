using xmap_project.Modules;

namespace xmap_project.Services.Interfaces;



public interface IAtividadeService
{
    Task<(bool Success, string Message, int? AtividadeId)> AddAtividadeAsync(int processId, CreateAtividadeRequest request);
    Task<List<object>> GetAtividadesAsync();
}