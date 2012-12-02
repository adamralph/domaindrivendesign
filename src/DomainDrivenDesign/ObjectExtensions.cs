// <copyright file="ObjectExtensions.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    internal static class ObjectExtensions
    {
        public static int GetHashCodeOrDefault<T>(this T value)
        {
            return object.Equals(value, default(T)) ? 0 : value.GetHashCode();
        }
    }
}
