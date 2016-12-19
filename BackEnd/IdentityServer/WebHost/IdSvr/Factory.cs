using WebHost.AspId;
using WebHost.IdSvr;
/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Caching;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.EntityFramework;

namespace WebHost.IdSvr
{
    class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory
            {
                ScopeStore = new Registration<IScopeStore>(new ScopeStore(new ScopeConfigurationDbContext("AspId"))),
                ClientStore = new Registration<IClientStore>(new ClientStore(new ClientConfigurationDbContext("AspId"))),
                CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService {AllowAll = true}),
                AuthorizationCodeStore =
                    new Registration<IAuthorizationCodeStore>(
                        new AuthorizationCodeStore(new OperationalDbContext("AspId"),
                            new CachingScopeStore(new ScopeStore(new ScopeConfigurationDbContext("AspId")),
                                new DefaultCache<IEnumerable<Scope>>()),
                            new ClientStore(new ClientConfigurationDbContext("AspId"))))
            };

            return factory;
        }
    }
}
