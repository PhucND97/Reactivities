using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext context;

        public ActivitiesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAll( )
        {
            return Ok(await context.Activities.ToListAsync());
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<Activity>> Get( [FromRoute] Guid id )
        {
            return Ok( await context.Activities.FirstOrDefaultAsync( a => a.Id == id ) );
        }


    }
}
