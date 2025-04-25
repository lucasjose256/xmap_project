using xmap_project.Data;
using xmap_project.Modules;


using Microsoft.AspNetCore.Mvc;
using xmap_project.Data;
using xmap_project.Modules;
using Microsoft.EntityFrameworkCore;

namespace xmap_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public class LoginRequest
        {
            public string username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.username == request.username);

            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            if (user.password != request.Password)
                return Unauthorized("Senha incorreta.");

            return Ok(new { message = "Login bem-sucedido!" });
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var existingUser = await _context.users.AnyAsync(u => u.username == request.username);
            if (existingUser)
                return BadRequest("Usuário já existe.");

            var newUser = new User
            {   email= "none",
                username = request.username,
                password = request.Password 
            };

            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário criado com sucesso!" });
        }
        
        
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.users
                .Select(u => new { u.id, u.username })
                .ToListAsync();

            return Ok(users);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.email))
                return BadRequest("Username é obrigatório.");

            var user = await _context.users.FirstOrDefaultAsync(u => u.email == request.email);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            // Aqui, vamos redefinir a senha diretamente com a nova fornecida
            // Em produção, você usaria email + token de verificação
            user.password = request.newPassword; // ⚠️ Lembre-se de hashear!
            await _context.SaveChangesAsync();

            return Ok(new { message = "Senha redefinida com sucesso!" });
        }

        public class ForgotPasswordRequest
        {
            public string email { get; set; }
            public string newPassword { get; set; }
        }

    }
}
