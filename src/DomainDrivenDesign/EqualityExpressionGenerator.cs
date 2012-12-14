// <copyright file="EqualityExpressionGenerator.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    // NOTE (Adam): expression building inspired by http://stackoverflow.com/a/986617/49241 and http://www.brad-smith.info/blog/archives/385
    internal static class EqualityExpressionGenerator
    {
        private static readonly MethodInfo CastToObjects = typeof(object).MakeEnumerableCastMethod();
        private static readonly MethodInfo ObjectsEqual = typeof(object).MakeEnumerableSequenceEqualMethod();

        public static Expression<Func<object, object, bool>> Generate(Type type)
        {
            var left = Expression.Parameter(typeof(object), "left");
            var right = Expression.Parameter(typeof(object), "right");

            var body = type.GetProperties()
                .Select(property => Generate(type, property, left, right))
                .Aggregate((Expression)Expression.Constant(true), (current, expression) => Expression.AndAlso(current, expression));

            return Expression.Lambda<Func<object, object, bool>>(body, left, right);
        }

        private static Expression Generate(Type type, PropertyInfo property, Expression left, Expression right)
        {
            var leftProperty = Expression.Property(Expression.Convert(left, type), property);
            var rightProperty = Expression.Property(Expression.Convert(right, type), property);

            // TODO (Adam): optimise for IList - see http://stackoverflow.com/a/486781/49241
            if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                var genericInterfaces = property.PropertyType.GetInterfaces()
                    .Where(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IEnumerable<>)).ToArray();

                if (genericInterfaces.Any())
                {
                    return genericInterfaces
                        .Select(@interface => Expression.Call(
                            @interface.GetGenericArguments()[0].MakeEnumerableSequenceEqualMethod(),
                            Expression.Convert(leftProperty, @interface),
                            Expression.Convert(rightProperty, @interface)))
                        .Aggregate((Expression)Expression.Constant(true), (current, expression) => Expression.AndAlso(current, expression));
                }

                return Expression.Equal(Expression.Call(CastToObjects, leftProperty), Expression.Call(CastToObjects, rightProperty), false, ObjectsEqual);
            }

            return Expression.Equal(leftProperty, rightProperty, false, property.PropertyType.GetEqualityOperator());
        }
    }
}
