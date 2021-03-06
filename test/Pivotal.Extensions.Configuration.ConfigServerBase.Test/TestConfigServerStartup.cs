﻿// Copyright 2017 the original author or authors.
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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Pivotal.Extensions.Configuration.ConfigServer.Test
{
    public class TestConfigServerStartup
    {
        private string _response;
        private int _returnStatus;

        private HttpRequest _request;

        public HttpRequest LastRequest
        {
            get
            {
                return _request;
            }
        }

        public TestConfigServerStartup(string response)
            : this(response, 200)
        {
        }

        public TestConfigServerStartup(string response, int returnStatus)
        {
            _response = response;
            _returnStatus = returnStatus;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                _request = context.Request;
                context.Response.StatusCode = _returnStatus;
                await context.Response.WriteAsync(_response);
            });
        }
    }
}
