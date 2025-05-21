namespace xmap_project.Modules;

public class MetaDados
{
    public int id { get; set; }
    
    public string dados { get; set; }
    
    public string ator { get; set; }
    
    public string lgpd { get; set; }
    public int atividadeId { get; set; }
    public Atividade atividade { get; set; }
}

