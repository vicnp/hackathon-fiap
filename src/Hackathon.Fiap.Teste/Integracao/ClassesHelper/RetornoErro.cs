namespace Hackathon.Fiap.Teste.Integracao.ClassesHelper
{
    public class ErroResponse
    {
        public ErroDetalhe Erro { get; set; }
    }

    public class ErroDetalhe
    {
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public int StatusCode { get; set; }
    }
}
