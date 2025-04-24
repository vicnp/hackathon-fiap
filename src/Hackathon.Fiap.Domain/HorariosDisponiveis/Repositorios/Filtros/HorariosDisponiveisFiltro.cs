using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

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