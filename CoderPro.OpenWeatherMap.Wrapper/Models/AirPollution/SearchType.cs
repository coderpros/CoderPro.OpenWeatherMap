// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchType.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the SearchType enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution
{
    /// <summary>
    /// The search type.
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Future air pollution forecasts.
        /// </summary>
        Forecast,

        /// <summary>
        /// Past air pollution history.
        /// </summary>
        History
    }
}
