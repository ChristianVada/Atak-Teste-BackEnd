using Atak2.Data;
using Atak2.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Atak2.Services
{
    public class AutenticacaoService
    {
        private readonly ApplicationDbContext _contexto;
        private readonly string _chaveSecreta;

        public AutenticacaoService(ApplicationDbContext contexto, IConfiguration configuration)
        {
            _contexto = contexto;
            _chaveSecreta = configuration["Jwt:Secret"];
        }

        public async Task<UsuarioModel> RegistrarAsync(RequisicaoRegistroModel requisicao)
        {
            if (await _contexto.Usuarios.AnyAsync(u => u.Email == requisicao.Email))
                throw new Exception("Email já cadastrado.");

            var usuario = new UsuarioModel
            {
                NomeUsuario = requisicao.NomeUsuario,
                Email = requisicao.Email,
                SenhaHash = GerarHashSenha(requisicao.Senha)
            };

            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioLoginResponseModel> LoginAsync(RequisicaoLoginModel requisicao)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == requisicao.Email);
            if (usuario == null || !VerificarSenha(requisicao.Senha, usuario.SenhaHash))
                throw new Exception("Credenciais inválidas.");

            var token = GerarToken(usuario);

            return new UsuarioLoginResponseModel
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Token = token
            };
        }

        private string GerarHashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerificarSenha(string senha, string senhaArmazenada)
        {
            var senhaHash = GerarHashSenha(senha);
            return senhaHash == senhaArmazenada;
        }

        private string GerarToken(UsuarioModel usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var chaveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveSecreta));
            var creds = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
