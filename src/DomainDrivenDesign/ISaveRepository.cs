// <copyright file="ISaveRepository.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates the saving of aggregate root entities.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity of the aggregate root.</typeparam>
    [ComVisible(true)]
    public interface ISaveRepository<TAggregateRoot, in TIdentity> where TAggregateRoot : AggregateRoot<TIdentity>
    {
        /// <summary>
        /// Saves <paramref name="aggregateRoot"/>.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root.</param>
        void Save(TAggregateRoot aggregateRoot);
    }
}
