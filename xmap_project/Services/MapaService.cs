using Microsoft.EntityFrameworkCore;
using xmap_project.Data;
using xmap_project.Modules;
using xmap_project.Services.Interfaces;

namespace xmap_project.Services;

public class MapaService : IMapaService
{
    private static readonly List<Mapa> _mapas = new List<Mapa>
    {
        new Mapa { id = 1, nome = "Fluxo de Aprovação de Férias", descricao = "Processo para solicitar e aprovar férias.", etapaId = "etp_001", xml = "<xml>...</xml>" },
        new Mapa { id = 2, nome = "Processo de Onboarding de Novos Funcionários", descricao = "Etapas para integração de novos colaboradores.", etapaId = "etp_002", xml = "<xml>...</xml>" },
        new Mapa { id = 3, nome = "Fluxo de Reembolso de Despesas", descricao = "Procedimento para solicitar reembolso de despesas corporativas.", etapaId = "etp_003", xml = "<xml>...</xml>" }
    };

    public async Task<IEnumerable<Mapa>> ListarMapasAsync()
    {
        await Task.Delay(10);
        return _mapas;
    }

    public async Task<Mapa?> ObterMapaPorIdAsync(int id)
    {
        await Task.Delay(10);
        return _mapas.FirstOrDefault(m => m.id == id);
    }

    // Implemente outros métodos (Criar, Atualizar, Deletar) conforme sua necessidade
    // Exemplo:
    // public async Task<Mapa> CriarMapaAsync(Mapa novoMapa)
    // {
    //     await Task.Delay(10);
    //     novoMapa.id = _mapas.Count > 0 ? _mapas.Max(m => m.id) + 1 : 1;
    //     _mapas.Add(novoMapa);
    //     return novoMapa;
    // }
}