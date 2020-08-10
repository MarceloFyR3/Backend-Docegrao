using System;
using System.Collections.Generic;
using System.Text;
using DoceGrao.Api.Domain.Services.User;
using DoceGrao.Api.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DoceGrao.Api.Services.DependencyInjection
{
    public static class TemplateServicesDependenciesInjector
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {
   
            services.AddScoped<IUserService, UserService>();


            return services;
        }
    }
}
