using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.EmployeeDto;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IAsyncRepository<Employee, Guid> _repository;
        private readonly IAsyncRepository<Cadre, Guid> _cadre;
        private readonly IAsyncRepository<Level, Guid> _level;
        private readonly IAsyncRepository<Position, Guid> _position;
        public EmployeeService(ApplicationDbContext dbContext, ILogger<EmployeeService> logger, IAsyncRepository<Employee, Guid> repository, IAsyncRepository<Cadre, Guid> cadre, IAsyncRepository<Level, Guid> level, IAsyncRepository<Position, Guid> position)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repository = repository;
            _cadre = cadre;
            _level = level;
            _position = position;
        }

        public async Task<ResponseModel> Create(EmployeeRequestModel employeeRequest)
        {
            try
            {



                if (employeeRequest.CadreId == Guid.Empty)
                {

                    return ResponseModel.Failure("Cadre must be selected for a particular employee");

                }
                var checkIfCadreExist = await _cadre.GetByIdAsync(employeeRequest.CadreId);
                if (checkIfCadreExist == null)
                {
                    return ResponseModel.Failure("the selected cadre does not exist in the system !!! Select a valid Cadre for this employee");
                }
                var employee = new Employee()
                {
                    CadreId = employeeRequest.CadreId,
                    FirstName = employeeRequest.FirstName,
                    LastName = employeeRequest.LastName,
                    Email = employeeRequest.Email,
                    DateOfBirth = employeeRequest.DateOfBirth,
                    Sex = employeeRequest.Sex,
                    EmployeeNumber = GenerateEmployeeIdNumber(),
                };
                await _repository.AddAsync(employee);
                await _repository.SaveChangesAsync();

                return ResponseModel.Success("Level successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while creating Level record: {ex.Message}", nameof(Employee));
                return ResponseModel.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<EmployeeDto>> Get(Guid employeeId)
        {
            try
            {
                var data = await _dbContext.Employee.Include(x=>x.Cadre).Where(x => x.Id == employeeId).FirstOrDefaultAsync();
                var getEmployeeCadre = await _cadre.GetByIdAsync(data.CadreId);
                var getLevel = await _level.GetByIdAsync(getEmployeeCadre.LevelId);
                var getEmployeePosition = await _position.GetByIdAsync(getLevel.PositionId);
                if (data != null)
                {
                    var employeeResult = new EmployeeDto()
                    {
                        Id = data.Id,
                        EmployeeNumber = data.EmployeeNumber,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        DateOfBirth = data.DateOfBirth,
                        Sex = data.Sex,
                        Email = data.Email,
                        CadreName = data.Cadre.CaderName,
                        Level = getLevel.Name,
                        Position = getEmployeePosition.PositionName
                    };
                    return ResponseModel<EmployeeDto>.Success(employeeResult);
                }

                return ResponseModel<EmployeeDto>.Failure("No Result Found");

            }


            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting job record list: {ex.Message}", nameof(Employee));
                return ResponseModel<EmployeeDto>.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<List<EmployeeDto>>> GetAll()
        {
            try
            {
                var result = await _dbContext.Employee.Include(x=>x.Cadre).ToListAsync();
                if(result == null)
                {
                    return ResponseModel<List<EmployeeDto>>.Failure("Zero Record");
                }
                var resultList = result.Select(data => new EmployeeDto()
                {
                    Id = data.Id,
                    EmployeeNumber = data.EmployeeNumber,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    DateOfBirth = data.DateOfBirth,
                    Sex = data.Sex,
                    Email = data.Email,
                    CadreName = data.Cadre.CaderName
                }).ToList();

                return ResponseModel<List<EmployeeDto>>.Success(resultList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return ResponseModel<List<EmployeeDto>>.Failure(ex.Message);
            }
        }

        private static string GenerateEmployeeIdNumber()
        {
            Random random = new Random();
            var rand = random.Next(0, 100);
            return "SNWETO" + rand;
        }
    }
}
