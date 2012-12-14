// <copyright file="EqualityExpressionGenerator.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    // NOTE (Adam): expression building inspired by http://stackoverflow.com/a/986617/49241 and http://www.brad-smith.info/blog/archives/385
    internal static class EqualityExpressionGenerator
    {
        public static Expression<Func<object, object, bool>> Generate(Type type)
        {
            var leftParameter = Expression.Parameter(typeof(object), "left");
            var rightParameter = Expression.Parameter(typeof(object), "right");

            var properties = type.GetProperties();
            if (properties.Length == 0)
            {
                return Expression.Lambda<Func<object, object, bool>>(Expression.Constant(true), leftParameter, rightParameter);
            }

            var left = Expression.Convert(leftParameter, type);
            var right = Expression.Convert(rightParameter, type);

            var body = properties
                .Select(property => Generate(left, right, property))
                .Aggregate(
                    (Expression)Expression.Constant(true), (expression, propertyExpression) => Expression.AndAlso(expression, propertyExpression));

            return Expression.Lambda<Func<object, object, bool>>(body, leftParameter, rightParameter);
        }

        private static BinaryExpression Generate(Expression left, Expression right, PropertyInfo property)
        {
            var leftProperty = Expression.Property(left, property);
            var rightProperty = Expression.Property(right, property);
            for (var type = property.PropertyType; type != null; type = type.BaseType)
            {
                var method = type.GetMethods()
                    .Where(candidate => candidate.Name == "op_Equality")
                    .FirstOrDefault(x =>
                    {
                        var parameters = x.GetParameters();
                        return parameters[0].ParameterType == type && parameters[1].ParameterType == type;
                    });

                if (method != null)
                {
                    return Expression.Equal(leftProperty, rightProperty, false, method);
                }
            }

            return Expression.Equal(leftProperty, rightProperty);
        }
    }
}
