using xmap_project.Modules;

namespace xmap_project.Services.Interfaces;

public interface IMapaService
{
    Task<IEnumerable<Mapa>> ListarMapasAsync();
    Task<Mapa?> ObterMapaPorIdAsync(int id); 
    
}