using Application.Features.Technology.Commands.CreateTechnology;
using Application.Features.Technology.Commands.DeleteTechnology;
using Application.Features.Technology.Commands.UpdateTechnology;
using Application.Features.Technology.Dtos;
using Application.Features.Technology.Models;
using Application.Features.Technology.Queries.GetByIdTechnology;
using Application.Features.Technology.Queries.GetListTechnologyByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] GetByIdTechnologyQuery getByIdTechnologyQueryQuery)
        {
            TechnologyGetByIdDto? result =
                await Mediator.Send(getByIdTechnologyQueryQuery);
            return Ok(result);
        }

        [HttpPost("GetListByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListTechnologyByDynamicQuery getListByDynamicTechnologyQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            TechnologyListModel result = await Mediator.Send(getListByDynamicTechnologyQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            [FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto? createdTechnologyDto =
                await Mediator.Send(createTechnologyCommand);
            return Created("", createdTechnologyDto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeletedTechnologyDto? result =
                await Mediator.Send(deleteTechnologyCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto? updatedTechnologyDto =
                await Mediator.Send(updateTechnologyCommand);
            return Ok(updatedTechnologyDto);
        }
    }
}
