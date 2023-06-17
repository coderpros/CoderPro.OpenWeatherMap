// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchTypes.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the SearchType enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    /// <summary>
    /// The search type.
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Use this value to query by location name.
        /// </summary>
        LocationName,

        /// <summary>
        /// Use this value to query by coordinate.
        /// </summary>
        Coordinate
    }
}
