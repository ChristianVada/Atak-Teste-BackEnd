namespace Atak2.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string SenhaHash{ get; set; }
    }
}
