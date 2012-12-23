// <copyright file="PersistenceModelAttribute.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

// TODO (Adam): think about how factories may fit in
namespace DomainDrivenDesign
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a specific implementation of repositories for a model.
    /// E.g. a specific database platform, the file system, in-memory, etc.
    /// Publically exposes only repositories.
    /// </summary>
    [ComVisible(true)]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class PersistenceModelAttribute : Attribute
    {
    }
}
