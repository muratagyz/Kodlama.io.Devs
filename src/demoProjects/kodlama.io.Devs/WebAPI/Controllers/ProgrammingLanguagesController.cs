using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };
            ProgrammingLanguageListModel result = await Mediator.Send(getListProgrammingLanguageQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
        {
            ProgrammingLanguageGetByIdDto? result =
                await Mediator.Send(getByIdProgrammingLanguageQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            [FromBody] CreateProgrammingLanguagesCommand createProgrammingLanguagesCommand)
        {
            CreatedProgrammingLanguageDto? createdProgrammingLanguageDto =
                await Mediator.Send(createProgrammingLanguagesCommand);
            return Created("", createdProgrammingLanguageDto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteProgrammingLanguagesCommand deleteProgrammingLanguagesCommand)
        {
            DeletedProgrammingLanguageDto? result =
                await Mediator.Send(deleteProgrammingLanguagesCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateProgrammingLanguagesCommand updateProgrammingLanguagesCommand)
        {
            UpdatedProgrammingLanguageDto? updatedProgrammingLanguageDto =
                await Mediator.Send(updateProgrammingLanguagesCommand);
            return Ok(updatedProgrammingLanguageDto);
        }
    }
}
