using Agenda.Application.Features.Activities.Commands.UpdateDueDate;
using Agenda.Application.Features.Activities.Commands.UpdatePriority;
using Agenda.Application.Features.Activities.Commands.Cancel;
using Agenda.Application.Features.Activities.Commands.Complete;
using Agenda.Application.Features.Activities.Commands.Create;
using Agenda.Application.Features.Activities.Commands.Delete;
using Agenda.Application.Features.Activities.Commands.Pause;
using Agenda.Application.Features.Activities.Commands.Start;
using Agenda.Application.Features.Activities.Commands.Update;
using Agenda.Application.Features.Activities.Queries.GetAll;
using Agenda.Application.Features.Activities.Queries.GetByDueDateRange;
using Agenda.Application.Features.Activities.Queries.GetById;
using Agenda.Application.Features.Activities.Queries.GetByStatus;
using Agenda.Application.Features.Activities.Queries.GetByTitle;
using Agenda.Application.ViewModels.DTO.Activity;
using Agenda.Domain.Enuns.ActivityPriority;
using Agenda.Domain.Enuns.ActivityStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TrilhaApiDesafio.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Read
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var activity = await _mediator.Send(new GetByIdActivitiesQuery(id));
        return activity != null ? Ok(activity) : NotFound();
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var listActivity = await _mediator.Send(new GetAllActivitiesQuery());
        return Ok(listActivity);
    }

    [HttpGet("by-title")]
    public async Task<IActionResult> GetByTitle([FromQuery] GetByTitleActivitiesQuery getByTitleActivitiesQuery)
    {
        var listActivity = await _mediator.Send(getByTitleActivitiesQuery);
        return Ok(listActivity);
    }

    [HttpGet("by-due-date-range")]
    public async Task<IActionResult> GetByDueDateRange([FromQuery] GetByDueDateRangeActivitiesQuery getByDueDateRangeActivitiesQuery)
    {
        var listActivity = await _mediator.Send(getByDueDateRangeActivitiesQuery);
        return Ok(listActivity);
    }

    [HttpGet("by-status")]
    public async Task<IActionResult> GetByStatus(EnumActivityStatus status)
    {
        var listActivity = await _mediator.Send(new GetByStatusActivitiesQuery(status));
        return Ok(listActivity);
    }
    #endregion

    #region Create
    [HttpPost()]
    public async Task<IActionResult> Create(InputCreateActivity inputCreateActivity)
    {
        var command = new CreateActivityCommand(inputCreateActivity.Title,
                                                inputCreateActivity.Description,
                                                inputCreateActivity.Priority,
                                                inputCreateActivity.DueDate);

        var activityId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = activityId }, new { id = activityId });
    }
    #endregion

    #region Update
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(long id, [FromBody] InputUpdateActivity inputUpdateActivity)
    {
        var command = new UpdateActivityCommand(
            id,
            inputUpdateActivity.Title,
            inputUpdateActivity.Description,
            inputUpdateActivity.Priority,
            inputUpdateActivity.DueDate);

        await _mediator.Send(command);

        return NoContent();
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        var command = new DeleteActivityCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    #endregion

    #region Actions(Start/Pause/Complete/Cancel)
    [HttpPatch("{id}/change-due-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeDueDate([FromQuery] long id, [FromBody] DateTimeOffset? dueDate)
    {
        var command = new UpdateDueDateActivityCommand(id, dueDate);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch("{id}/change-priority")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePriority(long id, EnumActivityPriority priority)
    {
        var command = new UpdatePriorityActivityCommand(id, priority);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch("{id}/start")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Start(long id)
    {
        var command = new StartActivityCommand(id);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch("{id}/pause")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Pause(long id)
    {
        var command = new PauseActivityCommand(id);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(long id)
    {
        var command = new CompleteActivityCommand(id);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(long id)
    {
        var command = new CancelActivityCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
    #endregion
}