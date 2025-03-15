using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Infra.Consultas.Consultas;

namespace Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces
{
    public interface IConsultaServico
    {
        Task<PaginacaoConsulta<Consulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct);
    }
}
