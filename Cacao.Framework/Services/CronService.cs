/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cacao.Framework.Services
{
    public abstract class CronService: BaseService
    {
        protected CronService(IServiceScopeFactory serviceScope) : base(serviceScope)
        {
        }

        protected override bool DelayAtStartup => true;

        protected abstract string ExecuteAt { get; }

        protected override TimeSpan GetDelayUntilNextRun()
        {
            var schedule = DateTime.Now.Date.Add(TimeSpan.Parse(ExecuteAt));

            if (DateTime.Now >= schedule)
            {
                schedule = schedule.AddDays(1);
            }

            return schedule - DateTime.Now;
        }
    }
}
