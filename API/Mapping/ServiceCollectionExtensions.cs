using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace API.Mapping
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(m =>
            m.AddProfile(new MappingProfile()));
            services.AddSingleton(mappingConfig.CreateMapper());
            return services;
        }
    }
}
