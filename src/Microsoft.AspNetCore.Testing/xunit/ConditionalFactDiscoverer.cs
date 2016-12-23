// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.AspNetCore.Testing.xunit
{
    internal class ConditionalFactDiscoverer : FactDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public ConditionalFactDiscoverer(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        public override IEnumerable<IXunitTestCase> Discover(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo factAttribute)
        {
            var testCases = base.Discover(discoveryOptions, testMethod, factAttribute);
            var skipReason = SkipConditionEvaluator.EvaluateSkipConditions(testMethod);

            if (skipReason != null)
            {
                return new[] { new SkipReasonTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod, skipReason) };
            }

            return testCases;
        }
    }
}