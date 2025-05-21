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


public class CreateProcessRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? UserLastEdited { get; set; }
}
public class MetaDadoInput
{
    public string TextoCompleto { get; set; }
}
public class MetaDadoTransformado
{
    public float[] Features { get; set; }
}
