using Dapr.Client;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NX.Integrations.Dapr.Services;

namespace ServiceA
{
    public static class DaprDataProtectionBuilderExtensions
    {
        public static IDataProtectionBuilder PersistKeysToDaprStore(this IDataProtectionBuilder builder, string storeName)
        {
            builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
            {
                var daprClient = services.GetService<DaprClient>();
                return new ConfigureOptions<KeyManagementOptions>(options =>
                 {
                     options.XmlRepository = new DaprXmlRepository(daprClient, storeName);
                 });
            });
            return builder;
        }
    }
}
