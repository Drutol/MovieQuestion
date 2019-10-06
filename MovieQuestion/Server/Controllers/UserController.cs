using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieQuestion.Shared.Requests;

namespace MovieQuestion.Server.Controllers
{
    [ApiController]
    [Route(("user"))]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string username)
        {
            return Ok(await _mediator.Send(new GetUserQuery
            {
                Username = username.ToLower()
            }));
        }
    }
}
