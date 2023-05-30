using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Application.Common.Dto.EmployeeDto;
using PayRoll.Application.Common.Dto.PayRollDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Shared;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayRollManagementController : ControllerBase
    {
        private readonly IPayRollManagementService _payRollManagementService;
        public PayRollManagementController(IPayRollManagementService payRollManagementService)
        {
            _payRollManagementService= payRollManagementService;
        }


        [HttpPost, Route("add-payroll")]
        [ProducesResponseType(typeof(ResponseModel<PayRollManagementDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<PayRollManagementDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(PayRollManagementRequestModel request)
        {

            var result = await _payRollManagementService.Create(request);
            return Ok(result);
        }


        [HttpGet, Route("Get-all-payroll")]
        [ProducesResponseType(typeof(ResponseModel<List<PayRollManagementDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<PayRollManagementDto>>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {

            var result = await _payRollManagementService.GetAll();

            return Ok(result);
        }


        [HttpGet, Route("Get-Payroll-by-Id")]
        [ProducesResponseType(typeof(ResponseModel<PayRollManagementDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<PayRollManagementDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {

            var result = await _payRollManagementService.Get(id);

            return Ok(result);
        }
    }
}
