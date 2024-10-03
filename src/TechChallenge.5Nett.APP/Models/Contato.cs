namespace Models
{
    public class Contato
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? DDD { get; set; }
        public string? Telefone { get; set; }
        public Regiao? Regiao { get; set; }

        public Contato()
        {

        }
    }
}
