namespace xmap_project.Modules;

public class Atividade
{
    public int id { get; set; }
    
    public string? nomeAtividade { get; set; }
    
    public MetaDados? metaDados { get; set; }
    
    public int processId { get; set; }
    
    public Process process { get; set; }
    
}