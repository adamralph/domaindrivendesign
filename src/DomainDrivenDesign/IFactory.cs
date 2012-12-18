// <copyright file="IFactory.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Encapsulates the creation of a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    [ComVisible(true)]
    public interface IFactory<TModel> where TModel : Model
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="TModel"/>.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TModel"/>.</returns>
        TModel Create();
    }

    /// <summary>
    /// Encapsulates the creation of a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    [ComVisible(true)]
    public interface IFactory<TModel, TInput> where TModel : Model
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="TModel" />.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// An instance of <typeparamref name="TModel" />.
        /// </returns>
        TModel Create(TInput input);
    }

    /// <summary>
    /// Encapsulates the creation of a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TInput1">The type of the input at position 1.</typeparam>
    /// <typeparam name="TInput2">The type of the input at position 2.</typeparam>
    [ComVisible(true)]
    public interface IFactory<TModel, TInput1, TInput2> where TModel : Model
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="TModel" />.
        /// </summary>
        /// <param name="input1">The input at position 1.</param>
        /// <param name="input2">The input at position 2.</param>
        /// <returns>
        /// An instance of <typeparamref name="TModel" />.
        /// </returns>
        TModel Create(TInput1 input1, TInput1 input2);
    }
}
