namespace Backend.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        // Exemplo de uma validação
        public bool ValidarEmail()
        {
            return Email.Contains("@");
        }
    }
}
