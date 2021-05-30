/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Cacao.Services
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Initializes all services from the given assembly.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sourceAssembly"></param>
        public static void InitServices(this IServiceCollection services, Assembly sourceAssembly)
        {
            var hostedServices = sourceAssembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(BaseService)))
                .ToList();

            hostedServices.ForEach(x =>
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IHostedService), x));
            });
        }
    }
}
