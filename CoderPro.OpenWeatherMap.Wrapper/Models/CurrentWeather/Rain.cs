// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rain.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Rain type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The rain.
    /// </summary>
    public class Rain
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rain"/> class.
        /// </summary>
        /// <param name="rainData">
        /// The rain element returned from the API.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when rainData is null.
        /// </exception>
        public Rain(JToken rainData)
        {
            if (rainData is null)
            {
                throw new ArgumentNullException(nameof(rainData));
            }

            if (rainData.SelectToken("3h") != null)
            {
                this.H3 = double.Parse(rainData.SelectToken("3h").ToString(), CultureInfo.InvariantCulture);
            }

            if (rainData.SelectToken("1h") != null)
            {
                this.H1 = double.Parse(rainData.SelectToken("1h").ToString(), CultureInfo.InvariantCulture);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the rain over a 3 hour period.
        /// </summary>
        public double H3 { get; }

        /// <summary>
        /// Gets the rain over a 1 hour period.
        /// </summary>
        public double H1 { get; }

        #endregion
    }
}
