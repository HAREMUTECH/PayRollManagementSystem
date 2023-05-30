using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Common.Dto.SalaryOptionDto;
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
    public class SalaryOptionService : ISalaryOptionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SalaryOptionService> _logger;
        private readonly IAsyncRepository<SalaryOption, Guid> _repository;
        private readonly IAsyncRepository<Cadre, Guid> _cadre;
        public SalaryOptionService(ApplicationDbContext dbContext, ILogger<SalaryOptionService> logger, IAsyncRepository<SalaryOption, Guid> repository, IAsyncRepository<Cadre, Guid> cadre)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repository = repository;
            _cadre = cadre;
        }

        public async Task<ResponseModel> Create(SalaryOptionRequestModel salaryOptionRequest)
        {
            try
            {



                if (salaryOptionRequest.CadreId == Guid.Empty)
                {

                    return ResponseModel.Failure("Provide a cadre for salary option");

                }
                var checkIfCadreExist = await _cadre.GetByIdAsync(salaryOptionRequest.CadreId);
                if(checkIfCadreExist == null)
                {
                    return ResponseModel.Failure("Provide a valid cadre");
                }
                var salaryOption = new SalaryOption()
                {
                    SalaryOptionName = salaryOptionRequest.SalaryOptionName,
                    Amount = salaryOptionRequest.Amount,
                    SalaryType = salaryOptionRequest.SalaryType,
                    CadreId = salaryOptionRequest.CadreId,
                };
                await _repository.AddAsync(salaryOption);
                await _repository.SaveChangesAsync();

                return ResponseModel.Success("Salary Option successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while creating Level record: {ex.Message}", nameof(SalaryOption));
                return ResponseModel.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<SalaryOptionDto>> Get(Guid salaryOptionId)
        {
            try
            {
                var data = await _dbContext.SalaryOption.Where(x => x.Id == salaryOptionId).FirstOrDefaultAsync();
                
                if (data != null)
                {
                    var salaryOptionResult = new SalaryOptionDto()
                    {
                        Id = data.Id,
                        SalaryOptionName = data.SalaryOptionName,
                        Amount = data.Amount,
                        SalaryType = data.SalaryType,
                        CadreName = data.Cadre.CaderName
                    };
                    return ResponseModel<SalaryOptionDto>.Success(salaryOptionResult);
                }

                return ResponseModel<SalaryOptionDto>.Failure("No Result Found");

            }


            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting job record list: {ex.Message}", nameof(SalaryOption));
                return ResponseModel<SalaryOptionDto>.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<List<SalaryOptionDto>>> GetAll()
        {
            try
            {
                var result = await _dbContext.SalaryOption.Include(x=>x.Cadre).ToListAsync();
                if(result != null)
                {
                    var resultList = result.Select(data => new SalaryOptionDto()
                    {
                        Id = data.Id,
                        SalaryOptionName = data.SalaryOptionName,
                        Amount = data.Amount,
                        SalaryType = data.SalaryType,
                        CadreName = data.Cadre.CaderName
                    }).ToList();

                    return ResponseModel<List<SalaryOptionDto>>.Success(resultList);

                }
                return ResponseModel<List<SalaryOptionDto>>.Failure("Zero Record in the system");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return ResponseModel<List<SalaryOptionDto>>.Failure(ex.Message);
            }
        }
    }
}
