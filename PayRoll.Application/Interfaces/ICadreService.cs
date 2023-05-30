using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Interfaces
{
    public interface ICadreService
    {
        Task<ResponseModel> Create(CadreRequestModel cadreRequest);
        Task<ResponseModel<List<CadreDto>>> GetAll();
        Task<ResponseModel<CadreDto>> Get(Guid cadreId);

    }
}
