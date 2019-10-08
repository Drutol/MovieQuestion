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
    [Route(("rating"))]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(long userId)
        {
            return Ok(await _mediator.Send(new GetUserRatingsQuery
            {
                UserId = userId
            }));
        }  
        
        [HttpPost]
        public async Task<IActionResult> Post(SetMovieRatingCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
