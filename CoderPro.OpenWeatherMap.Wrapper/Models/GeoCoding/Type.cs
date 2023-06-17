// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Types.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Types enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.GeoCoding
{
    /// <summary>
    /// The type.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// The coordinates by location name value will get a location by its name (City, Province, Country).
        /// </summary>
        CoordinatesByLocationName,

        /// <summary>
        /// The coordinates by post code value will get a location by post or zip code.
        /// </summary>
        CoordinatesByPostCode,

        /// <summary>
        /// The reverse value will get a location by coordinate.
        /// </summary>
        Reverse
    }
}
