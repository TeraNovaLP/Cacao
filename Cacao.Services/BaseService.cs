/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cacao.Services
{
    public abstract class BaseService: BackgroundService
    {
        private readonly IServiceScopeFactory serviceScope;
        private readonly ILogger logger;

        protected BaseService(IServiceScopeFactory serviceScope, ILoggerFactory logger)
        {
            this.serviceScope = serviceScope;
            this.logger = logger.CreateLogger("Cacao.Services");
        }

        protected abstract bool DelayAtStartup { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (DelayAtStartup)
            {
                await Task.Delay(GetDelayUntilNextRun(), stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation($"Executing {GetType().Name} ...");

                using (var scope = serviceScope.CreateScope())
                {
                    await Execute(scope);
                }

                logger.LogInformation("Finished!");

                await Task.Delay(GetDelayUntilNextRun(), stoppingToken);
            }
        }

        protected abstract TimeSpan GetDelayUntilNextRun();

        protected abstract Task Execute(IServiceScope scope);
    }
}
