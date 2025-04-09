using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

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