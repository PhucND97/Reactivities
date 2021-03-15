using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler( DataContext context, IMapper mapper )
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle( Command request, CancellationToken cancellationToken )
            {
                var activityToEdit = await context.Activities.FirstOrDefaultAsync(a => a.Id == request.Activity.Id);
                if ( activityToEdit is null )
                {
                    return Unit.Value;
                }

                mapper.Map( request.Activity, activityToEdit );

                await context.SaveChangesAsync( );

                return Unit.Value;
            }
        }
    }
}
