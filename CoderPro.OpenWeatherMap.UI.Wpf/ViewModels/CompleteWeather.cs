// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompleteWeather.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the CompleteWeather type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf.ViewModels
{
    #region Usings

    using System;

    using CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution;
    using CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather;

    #endregion

    /// <summary>
    /// The complete weather view model.
    /// </summary>
    internal class CompleteWeather
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteWeather"/> class.
        /// </summary>
        /// <param name="currentWeather">
        /// The current weather.
        /// </param>
        /// <param name="currentAirPollution">
        /// The current air pollution.
        /// </param>
        /// <param name="forecastAirPollution">
        /// The forecast air pollution.
        /// </param>
        /// <param name="forecast5">
        /// The forecast 5.
        /// </param>
        internal CompleteWeather(
            QueryResponse currentWeather,
            CurrentResponse currentAirPollution,
            ForecastResponse forecastAirPollution,
            OpenWeatherMap.Wrapper.Models.FiveDayForecast.QueryResponse forecast5)
        {
            this.CurrentWeather = currentWeather ?? throw new ArgumentNullException(nameof(currentWeather));
            this.CurrentAirPollution =
                currentAirPollution ?? throw new ArgumentNullException(nameof(currentAirPollution));
            this.ForecastAirPollution =
                forecastAirPollution ?? throw new ArgumentNullException(nameof(forecastAirPollution));
            this.Forecast5 = forecast5 ?? throw new ArgumentNullException(nameof(forecast5));
        }
        #endregion

        /// <summary>
        /// Gets or sets the <see cref="QueryResponse">current weather</see>.
        /// </summary>
        public QueryResponse CurrentWeather { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CurrentResponse">current air pollution</see>.
        /// </summary>
        public CurrentResponse CurrentAirPollution { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ForecastResponse">air pollution forecast</see>.
        /// </summary>
        public ForecastResponse ForecastAirPollution { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CoderPro.OpenWeatherMapWrapper.Models.FiveDayForecast" >forecast 5</see>.(5 day/3 hour)
        /// </summary>
        public OpenWeatherMap.Wrapper.Models.FiveDayForecast.QueryResponse Forecast5 { get; set; }
    }
}
