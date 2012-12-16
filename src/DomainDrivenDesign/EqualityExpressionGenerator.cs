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
                .Select(property => Generate(
                    property.PropertyType,
                    Expression.Property(Expression.Convert(left, type), property),
                    Expression.Property(Expression.Convert(right, type), property)))
                .Aggregate((Expression)Expression.Constant(true), (current, expression) => Expression.AndAlso(current, expression));

            return Expression.Lambda<Func<object, object, bool>>(body, left, right);
        }

        private static Expression Generate(Type type, Expression left, Expression right)
        {
            var equalityOperator = type.GetEqualityOperatorOrDefault();

            // TODO (Adam): optimise for IList - see http://stackoverflow.com/a/486781/49241
            if (equalityOperator == null && typeof(IEnumerable).IsAssignableFrom(type))
            {
                var genericInterfaces = type.GetInterfaces()
                    .Where(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IEnumerable<>)).ToArray();

                if (genericInterfaces.Any())
                {
                    return genericInterfaces
                        .Select(@interface => Expression.Call(
                            @interface.GetGenericArguments()[0].MakeEnumerableSequenceEqualMethod(),
                            Expression.Convert(left, @interface),
                            Expression.Convert(right, @interface)))
                        .Aggregate((Expression)Expression.Constant(true), (current, expression) => Expression.AndAlso(current, expression));
                }

                return Expression.Equal(Expression.Call(CastToObjects, left), Expression.Call(CastToObjects, right), false, ObjectsEqual);
            }

            return Expression.Equal(left, right, false, equalityOperator);
        }
    }
}
