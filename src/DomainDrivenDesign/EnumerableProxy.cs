// <copyright file="EnumerableProxy.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal static class EnumerableProxy
    {
        public static IEnumerable<TResult> Cast<TResult>(IEnumerable source)
        {
            return source.Cast<TResult>();
        }

        public static bool SequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return first.SequenceEqual(second);
        }
    }
}