// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.cs" company="coderPro.net">
//  Copyright 2023 coderPro.net. All rights reserved. 
// </copyright>
// <summary>
//   Defines the Weather type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The weather model.
    /// </summary>
    public class Weather
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Weather"/> class.
        /// </summary>
        /// <param name="weatherData">
        /// The weather element from the API response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when weatherData is null.
        /// </exception>
        public Weather(JToken weatherData)
        {
            if (weatherData is null)
            {
                throw new ArgumentNullException(nameof(weatherData));
            }

            this.Id = int.Parse(weatherData.SelectToken("id")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.Main = weatherData.SelectToken("main")?.ToString() ?? string.Empty;
            this.Description = weatherData.SelectToken("description")?.ToString() ?? string.Empty;
            this.Icon = weatherData.SelectToken("icon")?.ToString() ?? string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        ///  Gets the ID.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the main.
        /// </summary>
        public string Main { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the icon representing the weather. Full url: https://openweathermap.org/img/w/{Icon}.png
        /// </summary>
        public string Icon { get; }

        #endregion
    }
}
