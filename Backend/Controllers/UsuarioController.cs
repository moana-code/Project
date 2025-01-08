using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestorDeTarefasAPI.Data;
using GestorDeTarefasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GestorDeTarefasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _chaveSecreta = "supersecreta123456789"; // Alterar isso para mais segurança!

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                return BadRequest("E-mail já cadastrado.");
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            // Verifica se as credenciais são válidas.
            var usuarioEncontrado = _context.Usuarios.FirstOrDefault(u =>
                u.Email == usuario.Email && u.Senha == usuario.Senha);

            if (usuarioEncontrado == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            // Geração do Token JWT
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveSecreta));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credenciais
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
        [HttpPost("Registrar")]
public IActionResult Registrar([FromBody] Usuario usuario)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (_context.Usuarios.Any(u => u.Email == usuario.Email))
    {
        return BadRequest("E-mail já cadastrado.");
    }

    _context.Usuarios.Add(usuario);
    _context.SaveChanges();
    return Ok("Usuário registrado com sucesso.");
}

    }
}
