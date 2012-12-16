// <copyright file="Guard.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Diagnostics;

    internal static class Guard
    {
        [DebuggerStepThrough]
        public static void AgainstNullArgument<T>(string parameterName, [ValidatedNotNull]T argument) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        // NOTE: when applied to a parameter, this attribute provides an indication to code analysis that the argument has been null checked
        private sealed class ValidatedNotNullAttribute : Attribute
        {
        }
    }
}
