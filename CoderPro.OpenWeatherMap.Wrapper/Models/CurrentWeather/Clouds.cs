// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Clouds.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Clouds type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The clouds.
    /// </summary>
    public class Clouds
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Clouds"/> class.
        /// </summary>
        /// <param name="cloudsData">
        /// The clouds element from the API response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if cloudsData is null.
        /// </exception>
        public Clouds(JToken cloudsData)
        {
            if (cloudsData is null)
            {
                throw new ArgumentNullException(nameof(cloudsData));
            }

            this.All = double.Parse(cloudsData.SelectToken("all")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the all cloud cover percentage.
        /// </summary>
        public double All { get; }

        #endregion
    }
}
