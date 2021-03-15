using Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id
            {
                get;
                set;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle( Command request, CancellationToken cancellationToken )
            {
                var activityToDelete = await context.Activities.FirstOrDefaultAsync(a => a.Id == request.Id);
                if ( activityToDelete is null )
                    return Unit.Value;
                context.Remove( activityToDelete );
                await context.SaveChangesAsync( );
                return Unit.Value;
            }
        }
    }
}
