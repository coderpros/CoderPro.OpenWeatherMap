// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mode.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Modes enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable InconsistentNaming

namespace CoderPro.OpenWeatherMap.Wrapper.Models.Common
{
    /// <summary>
    /// The mode.
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Makes the API return JSON.
        /// </summary>
        JSON,

        /// <summary>
        /// Makes the API return XML.
        /// </summary>
        XML
    }
}
