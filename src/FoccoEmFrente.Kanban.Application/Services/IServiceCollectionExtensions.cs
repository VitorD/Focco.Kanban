using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoccoEmFrente.Kanban.Application.Services
{
   public static class IServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IActivityService, ActivityService>();
        
        }
    }
}
