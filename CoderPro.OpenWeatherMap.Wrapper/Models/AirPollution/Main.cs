// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Main type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution
{
    #region Usings

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The main model.
    /// </summary>
    public class Main
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        /// <param name="mainData">
        /// The main data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws this exception if the main element is not present in the response.
        /// </exception>
        public Main(JToken mainData)
        {
            if (mainData is null)
            {
                throw new ArgumentNullException(nameof(mainData));
            }

            this.AirQualityIndex = Enum.Parse<AirQualityIndex>(mainData.SelectToken("aqi")?.ToString());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="AirQualityIndex">air quality index</see>.
        /// </summary>
        public AirQualityIndex AirQualityIndex { get; }

        #endregion
    }
}
