using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

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