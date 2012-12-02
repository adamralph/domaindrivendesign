// <copyright file="TypeExtensions.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;

    internal static class TypeExtensions
    {
        public static bool IsNullableType(this Type type)
        {
            Guard.AgainstNullArgument("type", type);

            return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
