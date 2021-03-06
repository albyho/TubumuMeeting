﻿using System;
using SIPSorcery.SIP;
using Tubumu.GB28181.Settings;
using Tubumu.GB28181.SIP;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GB28181ServiceCollectionExtensions
    {
        public static IServiceCollection AddGB28281(this IServiceCollection services)
        {
            services.AddSingleton<SIPTransport>();
            services.AddSingleton<IRegisterService, RegisterService>();
            return services;
        }
    }
}
