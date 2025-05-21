namespace xmap_project.Modules;

public class Mapa
{
    public int id { get; set; }
    
    public string etapaId { get; set; }
    
    public Etapa etapa { get; set; }

    public string xml { get; set; }

    public string nome { get; set; }

    public string? descricao { get; set; }
    
    
}