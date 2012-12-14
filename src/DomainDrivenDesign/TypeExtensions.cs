// <copyright file="TypeExtensions.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class TypeExtensions
    {
        public static bool IsNullableType(this Type type)
        {
            Guard.AgainstNullArgument("type", type);

            return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static MethodInfo MakeEnumerableCastMethod(this Type type)
        {
            return typeof(EnumerableProxy).GetMethod("Cast").MakeGenericMethod(type);
        }

        public static MethodInfo MakeEnumerableSequenceEqualMethod(this Type type)
        {
            return typeof(EnumerableProxy).GetMethod("SequenceEqual").MakeGenericMethod(type);
        }

        public static MethodInfo GetEqualityOperator(this Type type)
        {
            Guard.AgainstNullArgument("type", type);

            for (; type != typeof(object); type = type.BaseType)
            {
                var method = GetDeclaredEqualityOperatorOrDefault(type);
                if (method != null)
                {
                    return method;
                }
            }

            return typeof(object).GetDeclaredEqualityOperatorOrDefault();
        }

        private static MethodInfo GetDeclaredEqualityOperatorOrDefault(this Type type)
        {
            return type.GetMethods()
                .Where(candidate => candidate.Name == "op_Equality")
                .FirstOrDefault(candidate =>
                    {
                        var parameters = candidate.GetParameters();
                        return parameters.Length == 2 && parameters[0].ParameterType == type && parameters[1].ParameterType == type;
                    });
        }
    }
}