// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Main type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The main.
    /// </summary>
    public class Main
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        /// <param name="mainData">
        /// The main element from the API response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when mainData is null.
        /// </exception>
        public Main(JToken mainData)
        {
            if (mainData is null)
            {
                throw new ArgumentNullException(nameof(mainData));
            }

            this.Pressure = double.Parse(mainData.SelectToken("pressure")?.ToString(), CultureInfo.CurrentCulture);
            this.Humidity = double.Parse(mainData.SelectToken("humidity")?.ToString(), CultureInfo.CurrentCulture);
            this.Temperature = new Temperature(
                double.Parse(mainData.SelectToken("temp")?.ToString(), CultureInfo.CurrentCulture),
                double.Parse(mainData.SelectToken("temp_min")?.ToString(), CultureInfo.CurrentCulture),
                double.Parse(mainData.SelectToken("temp_max")?.ToString(), CultureInfo.CurrentCulture),
                double.Parse(mainData.SelectToken("feels_like")?.ToString(), CultureInfo.CurrentCulture));

            if (mainData.SelectToken("sea_level") != null)
            {
                this.SeaLevelAtm = double.Parse(mainData.SelectToken("sea_level")?.ToString() ?? string.Empty, CultureInfo.CurrentCulture);
            }

            if (mainData.SelectToken("grnd_level") != null)
            {
                this.GroundLevelAtm = double.Parse(mainData.SelectToken("grnd_level")?.ToString() ?? string.Empty, CultureInfo.CurrentCulture);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the temperature.
        /// </summary>
        public Temperature Temperature { get; }

        /// <summary>
        /// Gets the atmospheric pressure. Measured in hPa.
        /// </summary>
        public double Pressure { get; }

        /// <summary>
        /// Gets the humidity percentage.
        /// </summary>
        public double Humidity { get; }

        /// <summary>
        /// Gets the sea level atmospheric pressure. Measured in hPa.
        /// </summary>
        public double SeaLevelAtm { get; }

        /// <summary>
        /// Gets the ground level atmospheric pressure. Measured in hPa.
        /// </summary>
        public double GroundLevelAtm { get; }

        #endregion
    }
}
