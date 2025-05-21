namespace xmap_project.Modules;

public class Process
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public DateTime? lastEdiTime { get; set; }
    public string? userLastEdited { get; set; }
    public string? urlProcess  {get; set;}
    public string xmlDiagrama  {get; set;}
    public List<Atividade> atividades { get; set; }
     
}