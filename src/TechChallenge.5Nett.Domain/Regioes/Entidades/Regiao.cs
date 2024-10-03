namespace Regioes.Entidades
{
    public class Regiao
    {
        public Regiao(int dDD, string? estado, string? descricao)
        {
            RegiaoDDD = dDD;
            Estado = estado;
            Descricao = descricao;
        }

        public Regiao()
        {

        }

        public int RegiaoDDD { get; set; }
        public string? Estado { get; set; }
        public string? Descricao { get; set; }
    }
}
