using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;

public class HorariosDisponiveisFiltro : PaginacaoFiltro
{
    public int? HorarioDisponivelId;
    public int? MedicoId;
    public int? PacienteId;
    public StatusHorarioDisponivelEnum? Status;

    public HorariosDisponiveisFiltro() : base("HorarioDisponivelId", TipoOrdernacao.Desc)
    {
        
    }
}