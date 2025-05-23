namespace xmap_project.Modules;

public class Raia
{
    public int id { get; set; }
    
    public string ator { get; set; }
    
    public string laneReference { get; set; }// id da referencia da lane dentro do xml

    public int processoId { get; set; }

    public Processo processo { get; set; }
    
    public ICollection<Atividade> Atividades { get; set; }  // Navegação para muitas Atividades

    
}