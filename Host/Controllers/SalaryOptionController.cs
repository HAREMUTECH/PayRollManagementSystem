using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.SalaryOptionDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Shared;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryOptionController : ControllerBase
    {

        private readonly ISalaryOptionService _salaryOptionService;
        public SalaryOptionController(ISalaryOptionService salaryOptionService)
        {
            _salaryOptionService = salaryOptionService;
        }


        [HttpPost, Route("add-salaryOption")]
        [ProducesResponseType(typeof(ResponseModel<SalaryOptionDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<SalaryOptionDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(SalaryOptionRequestModel request)
        {
            var result = await _salaryOptionService.Create(request);
            return Ok(result);
        }


        [HttpGet, Route("get-all-salaryOption")]
        [ProducesResponseType(typeof(ResponseModel<List<SalaryOptionDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<SalaryOptionDto>>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {

            var result = await _salaryOptionService.GetAll();
            return Ok(result);
        }


        [HttpGet, Route("get-salaryOption-by-Id")]
        [ProducesResponseType(typeof(ResponseModel<SalaryOptionDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<SalaryOptionDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _salaryOptionService.Get(id);
            return Ok(result);
        }
    }
}
