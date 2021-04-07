/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cacao.Framework.Services
{
    public abstract class IntervalService: BaseService
    {
        protected IntervalService(IServiceScopeFactory serviceScope) : base(serviceScope)
        {
        }

        protected abstract TimeSpan ExecuteInterval { get; }

        protected override TimeSpan GetDelayUntilNextRun()
        {
            return ExecuteInterval;
        }
    }
}
