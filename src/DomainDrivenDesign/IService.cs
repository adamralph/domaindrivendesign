// <copyright file="IService.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates a stateless operation that is not a natural part of an instance of <see cref="Entity{TIdentity}"/> or <see cref="ValueObject"/>,
    /// defined in terms of other elements of the domain model.
    /// </summary>
    [ComVisible(true)]
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Communicates intent only.")]
    public interface IService
    {
    }
}
