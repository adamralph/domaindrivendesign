// <copyright file="IDeleteRepository.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates the deletion of persistent aggregate root entities.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity of the aggregate root.</typeparam>
    [ComVisible(true)]
    public interface IDeleteRepository<TAggregateRoot, in TIdentity> where TAggregateRoot : AggregateRoot<TIdentity>
    {
        /// <summary>
        /// Deletes the instance of <typeparamref name="TAggregateRoot"/> with <paramref name="identity"/>.
        /// </summary>
        /// <param name="identity">The identity of an instance of <typeparamref name="TAggregateRoot"/>.</param>
        void Delete(TIdentity identity);
    }
}
