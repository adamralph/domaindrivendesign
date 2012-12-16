// <copyright file="GetHashCodeGenerator.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    // NOTE (Adam): expression building inspired by http://stackoverflow.com/a/986617/49241 and http://www.brad-smith.info/blog/archives/385
    internal static class GetHashCodeGenerator
    {
        public static Func<object, int> Generate(Type type)
        {
            var obj = Expression.Parameter(typeof(object), "obj");
            var body = type.GetProperties()
                .Select(property => Expression.Call(
                    typeof(ObjectExtensions).GetMethod("GetHashCodeOrDefault").MakeGenericMethod(property.PropertyType),
                    Expression.Property(Expression.TypeAs(obj, type), property)))
                .Aggregate(
                    (Expression)Expression.Constant(17),
                    (expression, propertyExpression) => Expression.Add(Expression.Multiply(expression, Expression.Constant(23)), propertyExpression));

            return Expression.Lambda<Func<object, int>>(body, obj).Compile();
        }
    }
}
