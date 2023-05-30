using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Entities;
using PayRoll.Domain.Interfaces;
using PayRoll.Domain.Shared;
using PayRoll.Infrastructure.Persistence.Context;
using System.ComponentModel.Design;

namespace PayRoll.Infrastructure.Implementation
{
    public class LevelService : ILevelService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<LevelService> _logger;
        private readonly IAsyncRepository<Level, Guid> _repository;
        private readonly IAsyncRepository<Position, Guid> _position;
        public LevelService(ApplicationDbContext dbContext, ILogger<LevelService> logger, IAsyncRepository<Level, Guid> repository, IAsyncRepository<Position, Guid> position)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repository = repository;
            _position = position;
        }

        public async Task<ResponseModel> Create(LevelRequestModel levelRequest)
        {
            try
            {

                if (levelRequest.PositionId == Guid.Empty)
                {

                    return ResponseModel.Failure("Position must be selected for a Level while creating a new Level");

                }
                var checkPositionIfExist = await _position.GetByIdAsync(levelRequest.PositionId);
                if (checkPositionIfExist == null)
                {
                    return ResponseModel.Failure("Select a valid position");
                }
                var level = new Level()
                {
                    PositionId = levelRequest.PositionId,
                    Description = levelRequest.Description,
                    Name = levelRequest.Name,
                };
                await _repository.AddAsync(level);
                await _repository.SaveChangesAsync();

                return ResponseModel.Success("Level successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while creating Level record: {ex.Message}", nameof(levelRequest));
                return ResponseModel.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<LevelDto>> Get(Guid levelId)
        {
            try
            {
                var data = await _dbContext.Level.Where(x => x.Id == levelId).FirstOrDefaultAsync();
               
                
                if (data != null)
                {
                    var levelResult = new LevelDto()
                    {
                        Id = data.Id,
                        PositionName = data.Position.PositionName,
                        Description = data.Description,
                        Name = data.Name,
                    };
                    return ResponseModel<LevelDto>.Success(levelResult);
                }

                return ResponseModel<LevelDto>.Failure("No Result Found");

            }


            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting job record list: {ex.Message}", nameof(levelId));
                return ResponseModel<LevelDto>.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<List<LevelDto>>> GetAll()
        {
           
            try
            {
                var result = await _dbContext.Level.Include(x=>x.Position).ToListAsync();
                if(result == null)
                {
                    return ResponseModel<List<LevelDto>>.Failure("Zero Record");
                }
                var resultList = result.Select(x => new LevelDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PositionName = x.Position.PositionName,
                }).ToList();

                


                return ResponseModel<List<LevelDto>>.Success(resultList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return ResponseModel<List<LevelDto>>.Failure(ex.Message);
            }
        }
    }
}
