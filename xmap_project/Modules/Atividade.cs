namespace xmap_project.Modules;

public class Atividade
{

    public int id { get; set; }

    public Raia Raia { get; set; }

    public int raiaId { get; set; }
    
    public string nome { get; set; }
   public MetaDados MetaDados { get; set; }
    
}