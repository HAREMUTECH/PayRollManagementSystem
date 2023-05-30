using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PayRollDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Entities;
using PayRoll.Domain.Interfaces;
using PayRoll.Domain.Shared;
using PayRoll.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Infrastructure.Implementation
{
    public class PayRollManagementService : IPayRollManagementService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PayRollManagementService> _logger;
        private readonly IAsyncRepository<PayRollManagement, Guid> _repository;
        private readonly IAsyncRepository<Employee, Guid> _employee;
        private readonly IAsyncRepository<CadreService, Guid> _cadre;
        private readonly IAsyncRepository<SalaryOption, Guid> _salaryOption;
        public PayRollManagementService(ApplicationDbContext dbContext, ILogger<PayRollManagementService> logger, IAsyncRepository<PayRollManagement, Guid> repository, IAsyncRepository<Employee, Guid> employee, IAsyncRepository<CadreService, Guid> cadre, IAsyncRepository<SalaryOption, Guid> salaryOption)
        {
            _dbContext = dbContext;
            _logger = logger;
            _repository = repository;
            _employee = employee;
            _cadre = cadre;
            _salaryOption = salaryOption;
        }
        public async Task<ResponseModel> Create(PayRollManagementRequestModel payRollRequest)
        {
            try
            {


                var user = await _employee.GetByIdAsync(payRollRequest.EmployeeID);
                var getSalaryOptionsByCadreId = await _salaryOption.ListAllAsync(x => x.CadreId == user.CadreId);

                if (payRollRequest.EmployeeID == Guid.Empty)
                {

                    return ResponseModel.Failure("Select an employee for a particular payroll");

                }

                var payRoll = new PayRollManagement()
                {
                    EmployeeID = payRollRequest.EmployeeID,
                    CreatedDate = DateTime.Now,
                    //GrossIncome = payRollRequest.GrossIncome,
                    // DeductionAmount = getSalaryOptionsByCadreId.Where(x=>x.SalaryType == Domain.Enums.SalaryType.Deduct),
                    //NetPay = payRollRequest.NetPay,
                    //IncomeAmount= payRollRequest.IncomeAmount,
                };
                await _repository.AddAsync(payRoll);
                await _repository.SaveChangesAsync();

                return ResponseModel.Success("Level successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while creating Level record: {ex.Message}", nameof(PayRollManagement));
                return ResponseModel.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<PayRollManagementDto>> Get(Guid payRolId)
        {
            try
            {
                var data = await _dbContext.PayRollManagement.Where(x => x.Id == payRolId).FirstOrDefaultAsync();
                var payRollResult = new PayRollManagementDto()
                {
                    EmployeeName = $"{data.Employee.FirstName} {data.Employee.LastName}",
                    GrossIncome = data.GrossIncome,
                    DeductionAmount = data.DeductionAmount,
                    NetPay = data.NetPay,
                    IncomeAmount = data.IncomeAmount,
                };
                if (data != null)
                {
                    return ResponseModel<PayRollManagementDto>.Success(payRollResult);
                }

                return ResponseModel<PayRollManagementDto>.Failure("No Result Found");

            }


            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting job record list: {ex.Message}", nameof(PayRollManagement));
                return ResponseModel<PayRollManagementDto>.Failure("Exception error");
            }
        }

        public async Task<ResponseModel<List<PayRollManagementDto>>> GetAll()
        {
            try
            {
                var result = await _dbContext.PayRollManagement.ToListAsync();

                var resultList = result.Select(data => new PayRollManagementDto()
                {
                    EmployeeName = $"{data.Employee.FirstName} {data.Employee.LastName}",
                    GrossIncome = data.GrossIncome,
                    DeductionAmount = data.DeductionAmount,
                    NetPay = data.NetPay,
                    IncomeAmount = data.IncomeAmount,
                }).ToList();




                return ResponseModel<List<PayRollManagementDto>>.Success(resultList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return ResponseModel<List<PayRollManagementDto>>.Failure(ex.Message);
            }
        }
    }
}
