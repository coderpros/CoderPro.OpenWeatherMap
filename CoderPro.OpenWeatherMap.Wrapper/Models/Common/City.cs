// --------------------------------------------------------------------------------------------------------------------
// <copyright file="City.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the City type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.Common
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    using Common = CoderPro.OpenWeatherMap.Wrapper.Common;

    #endregion

    /// <summary>
    /// The city.
    /// </summary>
    public class City
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="City"/> class.
        /// </summary>
        /// <param name="cityData">
        /// The city element from the API response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if cityData is null.
        /// </exception>
        public City(JToken cityData)
        {
            if (cityData is null)
            {
                throw new ArgumentNullException(nameof(cityData));
            }

            this.Id = long.Parse(cityData.SelectToken("id")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.Name = cityData.SelectToken("name")?.ToString() ?? string.Empty;
            this.Population = int.Parse(cityData.SelectToken("population")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.Country = cityData.SelectToken("country")?.ToString() ?? string.Empty;
            this.TimeZone = int.Parse(cityData.SelectToken("timezone")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.Sunrise = Common.ConvertUnixToDateTime(long.Parse(cityData.SelectToken("sunrise")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture));
            this.Sunset = Common.ConvertUnixToDateTime(long.Parse(cityData.SelectToken("sunset")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture));
            this.Coordinate = new NetTopologySuite.Geometries.Point(double.Parse(cityData.SelectToken("coord")?.SelectToken("lat")?.ToString() ?? string.Empty, CultureInfo.CurrentCulture), double.Parse(cityData.SelectToken("coord")?.SelectToken("lon")?.ToString() ?? string.Empty, CultureInfo.CurrentCulture));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the population.
        /// </summary>
        public int Population { get; }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        public int TimeZone { get; }

        /// <summary>
        /// Gets the sunrise.
        /// </summary>
        public DateTime Sunrise { get; }

        /// <summary>
        /// Gets the sunset.
        /// </summary>
        public DateTime Sunset { get; }

        /// <summary>
        /// Gets the coordinate.
        /// </summary>
        public NetTopologySuite.Geometries.Point Coordinate { get; }

        #endregion
    }
}
