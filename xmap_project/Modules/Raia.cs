namespace xmap_project.Modules;

public class Raia
{
    public int id { get; set; }
    
    public string ator { get; set; }
    
    public string laneReference { get; set; }// id da referencia da lane dentro do xml

    public string processoId { get; set; }

    public Process processo { get; set; }
    
}