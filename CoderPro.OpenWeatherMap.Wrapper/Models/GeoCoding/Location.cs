// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Location.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Location type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.GeoCoding
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The location.
    /// </summary>
    public class Location
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="locationData">
        /// The location data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if locationData is null.
        /// </exception>
        public Location(JToken locationData)
        {
            if (locationData is null)
            {
                throw new ArgumentNullException(nameof(locationData));
            }

            this.Name = locationData.SelectToken("name")?.ToString() ?? string.Empty;
            this.Coordinates = new NetTopologySuite.Geometries.Point(double.Parse(locationData.SelectToken("lat")?.ToString() ?? "0", CultureInfo.InvariantCulture), double.Parse(locationData.SelectToken("lon")?.ToString() ?? "0", CultureInfo.InvariantCulture));
            this.Country = locationData.SelectToken("country")?.ToString() ?? string.Empty;
            this.LocalNames = new List<LocalName>();

            if (locationData.SelectToken("local_names") != null)
            {
                foreach (var localName in locationData.SelectToken("local_names").Cast<JProperty>())
                {
                    this.LocalNames?.Add(new LocalName(localName));
                }
            }

            if (locationData.SelectToken("state") != null)
            {
                this.State = locationData.SelectToken("state").ToString();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public string State { get; }

        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        public NetTopologySuite.Geometries.Point Coordinates { get; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the local names.
        /// </summary>
        public List<LocalName>? LocalNames { get; }

        #endregion Properties
    }
}
