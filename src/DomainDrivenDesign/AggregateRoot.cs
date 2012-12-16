// <copyright file="AggregateRoot.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An entity which is the root of an aggregate.
    /// </summary>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    [Serializable]
    [ComVisible(true)]
    public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>
    {
    }
}
