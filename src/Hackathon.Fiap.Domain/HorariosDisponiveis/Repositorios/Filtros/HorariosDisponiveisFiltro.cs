using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;

public class HorariosDisponiveisFiltro : PaginacaoFiltro
{
    public int IdMedico;
    public int? IdPaciente;
    public StatusHorarioDisponivelEnum? Status;

    public HorariosDisponiveisFiltro() : base("IdHorarioDisponivel", TipoOrdernacao.Desc)
    {
        
    }
}