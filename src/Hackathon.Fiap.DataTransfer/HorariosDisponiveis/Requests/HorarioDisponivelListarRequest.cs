using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;

public class HorarioDisponivelListarRequest : PaginacaoFiltro
{
    public int MedicoId { get; set; }
    public int? PacienteId { get; set; }
    public StatusHorarioDisponivelEnum? Status { get; set; }
    
    public HorarioDisponivelListarRequest() : base("HorarioDisponivelId", TipoOrdernacao.Desc)
    {
        
    }
}