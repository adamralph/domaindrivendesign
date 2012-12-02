// <copyright file="Entity.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An object defined by a thread of continuity and identity.
    /// </summary>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    [Serializable]
    [ComVisible(true)]
    public abstract class Entity<TIdentity> : IEquatable<Entity<TIdentity>>
    {
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified instances are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Entity<TIdentity> left, Entity<TIdentity> right)
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
        public static bool operator !=(Entity<TIdentity> left, Entity<TIdentity> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Entity{TIdentity}" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Entity{TIdentity}" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="Entity{TIdentity}" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Entity<TIdentity> other)
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

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var thisIdentity = this.GetIdentity();
            var otherIdentity = ((Entity<TIdentity>)obj).GetIdentity();

            if (typeof(TIdentity).IsNullableType() && thisIdentity == null)
            {
                return otherIdentity == null;
            }

            return thisIdentity.Equals(otherIdentity);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.GetIdentity().GetHashCodeOrDefault();
        }

        /// <summary>
        /// Gets the identity of this entity.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TIdentity"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "By design.")]
        protected abstract TIdentity GetIdentity();
    }
}
