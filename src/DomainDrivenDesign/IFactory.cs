// <copyright file="IFactory.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates the creation of an instance of <see cref="Entity{TIdentity}"/> or <see cref="ValueObject"/>.
    /// </summary>
    [ComVisible(true)]
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Communicates intent only.")]
    public interface IFactory
    {
    }
}
