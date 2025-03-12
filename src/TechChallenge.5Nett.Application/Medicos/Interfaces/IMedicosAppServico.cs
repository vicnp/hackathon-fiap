using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contatos.Requests;
using Medicos.Requests;
using Medicos.Responses;
using Utils;

namespace Medicos.Interfaces
{
    public interface IMedicosAppServico
    {
        Task<PaginacaoConsulta<MedicoResponse>> ListarContatosComPaginacaoAsync(MedicosPaginacaoRequest request);
    }
}