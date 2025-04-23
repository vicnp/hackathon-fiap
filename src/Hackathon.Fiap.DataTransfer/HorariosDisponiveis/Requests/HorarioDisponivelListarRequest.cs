using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;

public class HorarioDisponivelListarRequest : PaginacaoFiltro
{
    public int IdMedico { get; set; }
    public int? IdPaciente { get; set; }
    public StatusHorarioDisponivelEnum? Status { get; set; }
    
    public HorarioDisponivelListarRequest() : base("IdHorarioDisponivel", TipoOrdernacao.Desc)
    {
        
    }
}