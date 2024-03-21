using ADOWrappers;
using ADOWrappers.Contacts;
using ADOWrappers.Implementations;
using Microsoft.Extensions.DependencyInjection;


namespace SADOWrappers
{
    public static class Registration
    {
        public static IServiceCollection UseADOWrappers(this IServiceCollection services)
        {
            services.AddScoped<IGenericSqlDataReader, GenericSqlDataReader>();
            services.AddScoped<IGenericSqlDataTable, GenericSqlDataTable>();
            return services;
        }
    }
}
