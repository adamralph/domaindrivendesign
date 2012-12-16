// <copyright file="IRepository.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates the persistence of aggregate root entities.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity of the aggregate root.</typeparam>
    [ComVisible(true)]
    public interface IRepository<TAggregateRoot, in TIdentity> where TAggregateRoot : AggregateRoot<TIdentity>
    {
        /// <summary>
        /// Saves <paramref name="aggregateRoot"/>.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root.</param>
        void Save(TAggregateRoot aggregateRoot);

        /// <summary>
        /// Gets the instance of <typeparamref name="TAggregateRoot"/> with <paramref name="identity"/>.
        /// </summary>
        /// <param name="identity">The identity of an instance of <typeparamref name="TAggregateRoot"/>.</param>
        /// <returns>An instance of <typeparamref name="TAggregateRoot"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "By design.")]
        TAggregateRoot Get(TIdentity identity);
    }
}
