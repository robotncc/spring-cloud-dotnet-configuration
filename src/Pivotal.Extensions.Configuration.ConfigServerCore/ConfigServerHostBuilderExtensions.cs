﻿//
// Copyright 2017 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace Pivotal.Extensions.Configuration.ConfigServer
{
    public static class ConfigServerHostBuilderExtensions
    {
        public static IWebHostBuilder UseCloudFoundryHosting(this IWebHostBuilder webHostBuilder)
        {
            if (webHostBuilder == null)
            {
                throw new ArgumentNullException(nameof(webHostBuilder));
            }

            List<string> urls = new List<string>();

            string portStr = Environment.GetEnvironmentVariable("PORT");
            if (!string.IsNullOrWhiteSpace(portStr))
            {
                int port;
                if (int.TryParse(portStr, out port))
                {
                    urls.Add($"http://*:{port}");
                }
            }

            if (urls.Count > 0)
            {
                webHostBuilder.UseUrls(urls.ToArray());
            }

            return webHostBuilder;
        }

        public static IWebHostBuilder AddConfigServer(this IWebHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddConfigServer(context.HostingEnvironment);
            });
            return hostBuilder;
        }
    }
}
