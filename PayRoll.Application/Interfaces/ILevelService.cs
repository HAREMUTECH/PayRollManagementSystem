using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Interfaces
{
    public interface ILevelService
    {
        Task<ResponseModel> Create(LevelRequestModel levelRequest);
        Task<ResponseModel<List<LevelDto>>> GetAll();
        Task<ResponseModel<LevelDto>> Get(Guid levelId);
    }
}
