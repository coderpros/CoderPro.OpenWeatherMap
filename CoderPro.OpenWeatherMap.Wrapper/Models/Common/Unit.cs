// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Units.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Units enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.Common
{
    /// <summary>
    /// The unit of measure.
    /// </summary>
    public enum Unit
    {
        /// <summary>
        ///  Kelvin, meters, centimeters, etc.
        /// </summary>
        Standard,

        /// <summary>
        /// Returns data in celsius, meters, centimeters, etc.
        /// </summary>
        Metric,

        /// <summary>
        /// Returns data using fahrenheit, feet, inches, etc.
        /// </summary>
        Imperial
    }
}
