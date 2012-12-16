// <copyright file="IGetRepository.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates the retrieval of persistent aggregate root entities.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity of the aggregate root.</typeparam>
    [ComVisible(true)]
    public interface IGetRepository<TAggregateRoot, in TIdentity> where TAggregateRoot : AggregateRoot<TIdentity>
    {
        /// <summary>
        /// Gets the instance of <typeparamref name="TAggregateRoot"/> with <paramref name="identity"/>.
        /// </summary>
        /// <param name="identity">The identity of an instance of <typeparamref name="TAggregateRoot"/>.</param>
        /// <returns>An instance of <typeparamref name="TAggregateRoot"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "By design.")]
        TAggregateRoot Get(TIdentity identity);
    }
}
