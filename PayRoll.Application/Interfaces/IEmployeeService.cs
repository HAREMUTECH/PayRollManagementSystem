using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.EmployeeDto;
using PayRoll.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResponseModel> Create(EmployeeRequestModel employeeRequest);
        Task<ResponseModel<List<EmployeeDto>>> GetAll();
        Task<ResponseModel<EmployeeDto>> Get(Guid employeeId);
    }
}
