// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Forecast.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Forecast model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json.Linq;

namespace CoderPro.OpenWeatherMap.Wrapper.Models.FiveDayForecast
{
    /// <summary>
    /// The forecast object simply inherits from <see cref="CurrentWeather.QueryResponse"/>.
    /// It is included for simplicity and for eventual addition of properties not present in the OpenWeatherMap API.
    /// </summary>
    public class Forecast : CurrentWeather.QueryResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Forecast"/> class.
        /// </summary>
        /// <param name="jsonResponse">
        /// The JSON response.
        /// </param>
        public Forecast(string jsonResponse) : base(jsonResponse)
        {
            var jsonData = JObject.Parse(jsonResponse);
        }

        public Models.AirPollution.Pollution? Pollution { get; set; }

        #endregion
    }
}
