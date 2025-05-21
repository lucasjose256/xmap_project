namespace xmap_project.Modules;

public class Processo
{
    //Pode ter mais de 1 processo no mapeamento
    //Ele seria a piscina dentro do Mapa

    public int id { get; set; }
    
    public string nome { get; set; }
    
    public string processRef { get; set; }// referencia do processo dentro do xml
    
    public int mapaId { get; set; }
    
    public Mapa mapa { get; set; }
    
    
}