using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace API.Mapping
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mappingCongig = new MapperConfiguration(m =>
            m.AddProfile(new MappingProfile()));
            services.AddSingleton(mappingCongig.CreateMapper());
            return services;
        }
    }
}
