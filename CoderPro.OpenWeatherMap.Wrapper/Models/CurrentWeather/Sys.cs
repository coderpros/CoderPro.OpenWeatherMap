// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sys.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Sys type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System;
    using System.Globalization;

    using Newtonsoft.Json.Linq;

    using Common = CoderPro.OpenWeatherMap.Wrapper.Common;

    #endregion

    /// <summary>
    /// The sys.
    /// </summary>
    public class Sys
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Sys"/> class.
        /// </summary>
        /// <param name="sysData">
        /// The sys element returned from the API.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when sysData is null.
        /// </exception>
        public Sys(JToken sysData)
        {
            if (sysData is null)
            {
                throw new ArgumentNullException(nameof(sysData));
            }

            if (sysData.SelectToken("type") != null)
            {
                this.Type = int.Parse(sysData.SelectToken("type")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            }

            if (sysData.SelectToken("id") != null)
            {
                this.Id = int.Parse(sysData.SelectToken("id")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            }

            if (sysData.SelectToken("message") != null)
            {
                this.Message = double.Parse(sysData.SelectToken("message")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            }

            if (sysData.SelectToken("sunrise") != null)
            {
                this.Sunrise = Common.ConvertUnixToDateTime(long.Parse(sysData.SelectToken("sunrise")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture));
            }

            if (sysData.SelectToken("sunset") != null)
            {
                this.Sunset = Common.ConvertUnixToDateTime(long.Parse(sysData.SelectToken("sunset")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture));
            }

            this.PartOfDay = sysData.SelectToken("pod")?.ToString() ?? string.Empty;
            this.Country = sysData.SelectToken("country")?.ToString() ?? string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        public int Type { get; }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///  Gets the message.
        /// </summary>
        public double Message { get; }

        /// <summary>
        ///  Gets the country.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the part of day.
        /// </summary>
        public string PartOfDay { get; }

        /// <summary>
        /// Gets the sunrise time.
        /// </summary>
        public DateTime Sunrise { get; set; }

        /// <summary>
        /// Gets the sunset time.
        /// </summary>
        public DateTime Sunset { get; set; }

        #endregion
    }
}
