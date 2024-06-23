using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_DataTransfer.Regiao.Responses;
using TC_Domain.Regioes.Repositorios.Consultas;

namespace TC_Application.Regioes.Profiles
{
    public class RegioesProfile: Profile
    {
        public RegioesProfile()
        {
            CreateMap<RegiaoConsulta, RegiaoResponse>();    
        }
    }
}
