namespace xmap_project.Modules;



public class CreateAtividadeRequest
{
    public string NomeAtividade { get; set; }
    public MetaDadosRequest MetaDados { get; set; }
}

public class MetaDadosRequest
{
    public string Dados { get; set; }
    public string Ator { get; set; }
    public string Lgpd { get; set; }
}