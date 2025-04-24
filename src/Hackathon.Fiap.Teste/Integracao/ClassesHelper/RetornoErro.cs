namespace Hackathon.Fiap.Teste.Integracao.ClassesHelper
{
    public class ErroResponse
    {
        public ErroDetalhe Erro { get; set; } = new();
    }

    public class ErroDetalhe
    {
        public string Mensagem { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
