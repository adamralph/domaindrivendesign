// <copyright file="ModuleAttribute.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a entire model or part of a model with high internal cohesion and low coupling to other modules.
    /// Publically exposes only entities, value objects, services, factories and repository interfaces.
    /// </summary>
    [ComVisible(true)]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class ModuleAttribute : Attribute
    {
    }
}
