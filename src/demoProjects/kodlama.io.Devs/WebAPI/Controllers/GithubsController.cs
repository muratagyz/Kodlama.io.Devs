using Application.Features.Github.Commands.CreateGithub;
using Application.Features.Github.Commands.DeleteGithub;
using Application.Features.Github.Commands.UpdateGithub;
using Application.Features.Github.Dtos;
using Application.Features.Github.Models;
using Application.Features.Github.Queries.GetByIdGithub;
using Application.Features.Github.Queries.GetListGithub;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubsController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] GetByIdGithubQuery getByIdGithubQuery)
        {
            GithubGetByIdDto? result =
                await Mediator.Send(getByIdGithubQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubQuery getListGithubQuery = new() { PageRequest = pageRequest };
            GithubListModel result = await Mediator.Send(getListGithubQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            [FromBody] CreateGithubCommand createGithubCommand)
        {
            CreatedGithubDto? createdGithubDto =
                await Mediator.Send(createGithubCommand);
            return Created("", createdGithubDto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteGithubCommand deleteGithubCommand)
        {
            DeletedGithubDto? result =
                await Mediator.Send(deleteGithubCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateGithubCommand updateGithubCommand)
        {
            UpdatedGithubDto? updatedGithubDto =
                await Mediator.Send(updateGithubCommand);
            return Ok(updatedGithubDto);
        }
    }
}
