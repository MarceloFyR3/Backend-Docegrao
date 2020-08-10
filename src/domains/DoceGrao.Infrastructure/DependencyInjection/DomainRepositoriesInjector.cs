using System;
using System.Collections.Generic;
using System.Text;
using DoceGrao.Api.Domain.Repositories.User;
using DoceGrao.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DoceGrao.Api.Infrastructure.DependencyInjection
{
    public static class DomainRepositoriesInjector
    {
        public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
