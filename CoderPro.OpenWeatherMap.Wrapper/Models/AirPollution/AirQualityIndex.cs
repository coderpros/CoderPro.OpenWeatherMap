// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AirQualityIndex.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the AirQualityIndex enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution
{
    /// <summary>
    /// The air quality index.
    /// </summary>
    public enum AirQualityIndex
    {
        // TODO: Quantify the meanings of the different indexes.
        Good = 1,
        Fair = 2,
        Moderate = 3,
        Poor = 4,
        Very_Poor = 5
    }
}
