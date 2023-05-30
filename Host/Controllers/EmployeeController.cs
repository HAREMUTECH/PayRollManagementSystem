using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Application.Common.Dto.EmployeeDto;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Shared;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost, Route("add-employee")]
        [ProducesResponseType(typeof(ResponseModel<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<EmployeeDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(EmployeeRequestModel request)
        {

            var result = await _employeeService.Create(request);
            return Ok(result);
        }


        [HttpGet, Route("Get-all-employees")]
        [ProducesResponseType(typeof(ResponseModel<List<EmployeeDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<EmployeeDto>>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {

            var result = await _employeeService.GetAll();

            return Ok(result);
        }


        [HttpGet, Route("Get-Employee-by-Id")]
        [ProducesResponseType(typeof(ResponseModel<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<EmployeeDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {

            var result = await _employeeService.Get(id);

            return Ok(result);
        }
    }
}
