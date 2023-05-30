using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Shared;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }


        [HttpPost, Route("add-Position")]
        [ProducesResponseType(typeof(ResponseModel<PositionDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<PositionDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(PositionRequestModel request)
        {

            var result = await _positionService.Create(request);
            return Ok(result);
        }


        [HttpGet, Route("get-all-positions")]
        [ProducesResponseType(typeof(ResponseModel<List<PositionDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<PositionDto>>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {

            var result = await _positionService.GetAll();

            return Ok(result);
        }


        [HttpGet, Route("get-position-by-Id")]
        [ProducesResponseType(typeof(ResponseModel<PositionDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<PositionDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {

            var result = await _positionService.Get(id);

            return Ok(result);
        }

    }
}
