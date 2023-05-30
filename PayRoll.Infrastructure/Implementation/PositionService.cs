using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Entities;
using PayRoll.Domain.Interfaces;
using PayRoll.Domain.Shared;
using PayRoll.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Infrastructure.Implementation
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PositionService> _logger;
        private readonly IAsyncRepository<Position, Guid> _repository;
        public PositionService(ApplicationDbContext dbContext, ILogger<PositionService> logger, IAsyncRepository<Position, Guid> repository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repository = repository;
        }

        public async Task<ResponseModel> Create(PositionRequestModel positionRequest)
        {
            try
            {
                
                var position = new Position()
                {
                     PositionName = positionRequest.Name

                };
                await _repository.AddAsync(position);
                await _repository.SaveChangesAsync();

                return ResponseModel.Success("Position successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while creating Level record: {ex.Message}", nameof(positionRequest));
                return ResponseModel.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<PositionDto>> Get(Guid positionId)
        {
            try
            {
                var data = await _dbContext.Position.Where(x => x.Id == positionId).FirstOrDefaultAsync();
               
                if (data != null)
                {
                    var positionResult = new PositionDto()
                    {
                        Id = data.Id,
                        Name = data.PositionName,
                    };
                    return ResponseModel<PositionDto>.Success(positionResult);
                }

                return ResponseModel<PositionDto>.Failure("No Result Found");

            }


            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting job record list: {ex.Message}", nameof(positionId));
                return ResponseModel<PositionDto>.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<List<PositionDto>>> GetAll()
        {
            try
            {
                var result = await _dbContext.Position.ToListAsync();
                if(result != null)
                {
                    var resultList = result.Select(x => new PositionDto()
                    {
                        Id = x.Id,
                        Name = x.PositionName
                    }).ToList();

                    return ResponseModel<List<PositionDto>>.Success(resultList);
                }
                return ResponseModel<List<PositionDto>>.Failure("zero records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return ResponseModel<List<PositionDto>>.Failure(ex.Message);
            }
        }
    }
}
