using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Shared;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadreController : ControllerBase
    {
        private readonly ICadreService _cadreService;
        public CadreController(ICadreService cadreService)
        {
            _cadreService = cadreService;
        }


        [HttpPost, Route("add-cadre")]
        [ProducesResponseType(typeof(ResponseModel<CadreDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<CadreDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(CadreRequestModel request)
        {

            var result = await _cadreService.Create(request);
            return Ok(result);
        }


        [HttpGet, Route("get-all-cadre")]
        [ProducesResponseType(typeof(ResponseModel<List<CadreDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<CadreDto>>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {

            var result = await _cadreService.GetAll();

            return Ok(result);
        }


        [HttpGet, Route("get-cadre-by-Id")]
        [ProducesResponseType(typeof(ResponseModel<CadreDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<CadreDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {

            var result = await _cadreService.Get(id);

            return Ok(result);
        }
    }
}
