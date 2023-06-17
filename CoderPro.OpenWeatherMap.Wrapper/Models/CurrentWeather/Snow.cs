// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Snow.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Snow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The snow.
    /// </summary>
    public class Snow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Snow"/> class.
        /// </summary>
        /// <param name="snowData">
        /// The snow element returned from the API.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when snowData is null.
        /// </exception>
        public Snow(JToken snowData)
        {
            if (snowData is null)
            {
                throw new ArgumentNullException(nameof(snowData));
            }

            if (snowData.SelectToken("1h") != null)
            {
                this.H1 = double.Parse(snowData.SelectToken("1h").ToString(), CultureInfo.InvariantCulture);
            }


            if (snowData.SelectToken("3h") != null)
            {
                this.H3 = double.Parse(snowData.SelectToken("3h").ToString(), CultureInfo.InvariantCulture);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the snow over a one hour period.
        /// </summary>
        public double H1 { get; }

        /// <summary>
        /// Gets the snow over a three hour period.
        /// </summary>
        public double H3 { get; }

        #endregion
    }
}
