using Application.Features.Meetings.Commands;
using Application.Features.Meetings.Dtos;
using Application.Features.Meetings.Queries;
using Application.Wrappers.Results;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeetingsController : BaseApiController
    {

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IEnumerable<MeetingDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<IEnumerable<MeetingDto>>))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return GetResponse(await Mediator.Send(new GetAllMeeting()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<MeetingDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<MeetingDto>))]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetMeetingById getMeetingById)
        {
            return GetResponse(await Mediator.Send(getMeetingById));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IDataResult<Meeting>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDataResult<Meeting>))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateMeeting createMeeting)
        {
            return GetResponseOnlyResultCreated(await Mediator.Send(createMeeting));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResponseResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResponseResult))]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateMeeting updateMeeting)
        {
            return GetResponseOnlyResult(await Mediator.Send(updateMeeting));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResponseResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResponseResult))]
        [HttpPost("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteMeeting deleteMeeting)
        {
            return GetResponseOnlyResult(await Mediator.Send(deleteMeeting));
        }
    }
}
