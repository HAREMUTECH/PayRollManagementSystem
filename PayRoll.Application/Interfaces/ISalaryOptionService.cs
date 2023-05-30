using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Common.Dto.SalaryOptionDto;
using PayRoll.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Interfaces
{
    public interface ISalaryOptionService
    {
        Task<ResponseModel> Create(SalaryOptionRequestModel salaryOptionRequest);
        Task<ResponseModel<List<SalaryOptionDto>>> GetAll();
        Task<ResponseModel<SalaryOptionDto>> Get(Guid salaryOptionId);

    }
}
