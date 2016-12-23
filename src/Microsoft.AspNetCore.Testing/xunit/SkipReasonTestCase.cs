// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microsoft.AspNetCore.Testing.xunit
{
    internal class SkipReasonTestCase : XunitTestCase
    {
        public SkipReasonTestCase(IMessageSink diagnosticMessageSink,
                                  TestMethodDisplay defaultMethodDisplay,
                                  ITestMethod testMethod,
                                  string skipReason,
                                  object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
        {
            SkipReason = skipReason;
        }
    }
}