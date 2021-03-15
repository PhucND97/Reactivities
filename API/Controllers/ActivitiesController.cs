using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Activities;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAll( )
        {
            return  await Mediator.Send(new List.Query());
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<Activity>> Get( [FromRoute] Guid id )
        {
            return  await Mediator.Send(new Details.Query { Id = id } );
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity( [FromBody] Activity newActivity )
        {
            return Ok( await Mediator.Send( new Create.Command { Activity = newActivity } ) );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateActivity( [FromBody] Activity activity, [FromRoute] Guid id )
        {
            activity.Id = id;
            return Ok( await Mediator.Send( new Edit.Command { Activity = activity } ) );
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteActivity( [FromRoute] Guid id )
        {
            return Ok( await Mediator.Send( new Delete.Command { Id = id } ) );
        }
    }
}
