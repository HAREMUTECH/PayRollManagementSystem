using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Shared;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;
        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }


        [HttpPost, Route("add-level")]
        [ProducesResponseType(typeof(ResponseModel<LevelDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<LevelDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(LevelRequestModel request)
        {

            var result = await _levelService.Create(request);
            return Ok(result);
        }


        [HttpGet, Route("Get-all-levels")]
        [ProducesResponseType(typeof(ResponseModel<List<LevelDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<LevelDto>>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {

            var result = await _levelService.GetAll();

            return Ok(result);
        }


        [HttpGet, Route("get-Level-by-Id")]
        [ProducesResponseType(typeof(ResponseModel<LevelDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<LevelDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {

            var result = await _levelService.Get(id);

            return Ok(result);
        }
    }
}
