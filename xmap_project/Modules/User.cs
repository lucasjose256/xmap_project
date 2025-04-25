namespace xmap_project.Modules;


    public class User
    {
        public int id { get; set; }
        
        public string username { get; set; }
        
        public string email { get; set; }
        // Em um cenário real, use um hash para a senha!
        public string password { get; set; }
        
    }

