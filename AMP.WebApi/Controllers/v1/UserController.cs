﻿using AMP.Application.Features.Commands;
using AMP.Application.Features.Queries;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Shared.Domain.Models;

namespace AMP.WebApi.Controllers.v1;

public class UserController : BaseControllerv1
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetUserPage.Query(command));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserDto> Get(int id)
        => await Mediator.Send(new GetUser.Query(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(UserCommand command)
    {
        var id = await Mediator.Send(new SaveUser.Command(command));
        return CreatedAtAction(nameof(Get), new {id}, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(int id)
        => await Mediator.Send(new DeleteUser.Command(id));
}