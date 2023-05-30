using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Interfaces
{
    public interface IPositionService
    {
        Task<ResponseModel> Create(PositionRequestModel positionRequest);
        Task<ResponseModel<List<PositionDto>>> GetAll();
        Task<ResponseModel<PositionDto>> Get(Guid positionId);
    }
}
