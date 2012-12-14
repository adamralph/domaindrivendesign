// <copyright file="ValueObject.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An object defined by its attributes.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        // NOTE (Adam): performance - http://geekswithblogs.net/BlackRabbitCoder/archive/2010/06/09/c-4-the-curious-concurrentdictionary.aspx
        private static readonly ConcurrentDictionary<Type, Func<object, object, bool>> EqualsMethods =
            new ConcurrentDictionary<Type, Func<object, object, bool>>();

        private static readonly ConcurrentDictionary<Type, Func<object, int>> GetHashCodeMethods =
            new ConcurrentDictionary<Type, Func<object, int>>();

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified instances are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return (object)left == null ? (object)right == null : left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified instances are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ValueObject" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="ValueObject" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="ValueObject" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ValueObject other)
        {
            return this.Equals((object)other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var type = this.GetType();
            return type == obj.GetType() && EqualsMethods.GetOrAdd(type, key => EqualityExpressionGenerator.Generate(key).Compile())(this, obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var method = GetHashCodeMethods.GetOrAdd(this.GetType(), key => GetHashCodeExpressionGenerator.Generate(key).Compile());
            unchecked
            {
                return method(this);
            }
        }
    }
}
