// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pollution.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Pollution type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    using Common = CoderPro.OpenWeatherMap.Wrapper.Common;

    #endregion

    /// <summary>
    /// The pollution model.
    /// </summary>
    public class Pollution
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Pollution"/> class.
        /// </summary>
        /// <param name="pollutionData">
        /// The pollution data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws this exception if no pollution data is present in the response.
        /// </exception>
        public Pollution(JToken pollutionData, double timeZone = 0)
        {
            if (pollutionData is null)
            {
                throw new ArgumentNullException(nameof(pollutionData));
            }

            if (pollutionData.SelectToken("main") != null)
            {
                this.Main = new Main(pollutionData.SelectToken("main"));
            }

            if (pollutionData.SelectToken("components") != null)
            {
                this.Components = new Components(pollutionData.SelectToken("components"));
            }

            this.Date = Common.ConvertUnixToDateTime(long.Parse(pollutionData.SelectToken("dt")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture)).AddSeconds(timeZone);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the main element from the response.
        /// </summary>
        public Main Main { get; }

        /// <summary>
        /// Gets the components element from the response.
        /// </summary>
        public Components Components { get; }

        /// <summary>
        /// Gets the date of the record (UTC).
        /// </summary>
        public DateTime Date { get; }

        #endregion
    }
}
