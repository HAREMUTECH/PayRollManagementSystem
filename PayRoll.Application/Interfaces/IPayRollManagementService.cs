using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.PayRollDto;
using PayRoll.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Interfaces
{
    public interface IPayRollManagementService
    {
        Task<ResponseModel> Create(PayRollManagementRequestModel payRollRequest);
        Task<ResponseModel<List<PayRollManagementDto>>> GetAll();
        Task<ResponseModel<PayRollManagementDto>> Get(Guid payRolId);

    }
}
