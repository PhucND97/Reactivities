using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Activities;
using Application.Core;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Persistence;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices( this IServiceCollection services, IConfiguration config )
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>( options =>
            {
                options.UseSqlServer( connectionString );
            } );
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1", new OpenApiInfo { Title = "API", Version = "v1" } );
            } );
            services.AddCors( options =>
            {
                options.AddPolicy( "Client side", builder => builder.AllowAnyHeader( ).AllowAnyMethod( ).WithOrigins( "http:\\localhost:3000" ) );
            } );

            services.AddMediatR( typeof( List.Handler ).Assembly );
            services.AddAutoMapper( typeof( MappingProfile ).Assembly );
        }
    }
}
