using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.LevelDto;
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
using static PayRoll.Infrastructure.Authorization.ApplicantPermission;

namespace PayRoll.Infrastructure.Implementation
{
    public class CadreService : ICadreService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CadreService> _logger;
        private readonly IAsyncRepository<Cadre, Guid> _repository;
        private readonly IAsyncRepository<Level, Guid> _level;
        public CadreService(ApplicationDbContext dbContext, ILogger<CadreService> logger, IAsyncRepository<Cadre, Guid> repository, IAsyncRepository<Level, Guid> level) 
        {
            _dbContext = dbContext;
            _logger = logger;
            _repository = repository;
            _level = level;
        }

        public async Task<ResponseModel> Create(CadreRequestModel cadreRequest)
        {
            try
            {


                if (cadreRequest.LevelId == Guid.Empty)
                {

                    return ResponseModel.Failure("You must include Level while creating Cadre");

                }
                var checkIfLevelIDExist = await _level.GetByIdAsync(cadreRequest.LevelId);
                if(checkIfLevelIDExist == null) 
                {
                    return ResponseModel.Failure("The selected Level does not exist");
                }
                     
                var cadre = new Cadre()
                {
                    Description= cadreRequest.Description,
                    CaderName = cadreRequest.CaderName,
                    LevelId = cadreRequest.LevelId,
                };
                await _repository.AddAsync(cadre);
                await _repository.SaveChangesAsync();

                return ResponseModel.Success("Cadre successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while creating Level record: {ex.Message}", nameof(cadreRequest));
                return ResponseModel.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<CadreDto>> Get(Guid cadreId)
        {
            try
            {
                var data = await _dbContext.Cadre.Where(x => x.Id == cadreId).FirstOrDefaultAsync();

               

                if (data != null)
                {
                    var cadreResult = new CadreDto()
                    {
                        Id = data.Id,
                        Description = data.Description,
                        CaderName = data.CaderName,
                        LevelName = data.Level.Name,
                    };
                    return ResponseModel<CadreDto>.Success(cadreResult);
                }

                return ResponseModel<CadreDto>.Failure("No Result Found");

            }


            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting job record list: {ex.Message}", nameof(cadreId));
                return ResponseModel<CadreDto>.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<List<CadreDto>>> GetAll()
        {
            try
            {
                var result = await _dbContext.Cadre.Include(x=>x.Level).ToListAsync();
                if(result == null)
                {
                    return ResponseModel<List<CadreDto>>.Failure("Zero records");
                }
                var resultList = result.Select(x => new CadreDto()
                {
                    Id = x.Id,
                    CaderName = x.CaderName,
                    Description = x.Description,
                    LevelName = x.Level.Name,
                }).ToList();
                return ResponseModel<List<CadreDto>>.Success(resultList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return ResponseModel<List<CadreDto>>.Failure(ex.Message);
            }
        }
    }
}
