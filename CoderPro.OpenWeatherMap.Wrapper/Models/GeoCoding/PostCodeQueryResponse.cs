// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostCodeQueryResponse.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the PostCodeQueryResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.GeoCoding
{
    #region Usings

    using NetTopologySuite.Geometries;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The post code query response.
    /// </summary>
    public class PostCodeQueryResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCodeQueryResponse"/> class.
        /// </summary>
        /// <param name="jsonResponse">
        /// The json response.
        /// </param>
        public PostCodeQueryResponse(string jsonResponse)
        {
            this.RawJson = jsonResponse ?? throw new ArgumentNullException(nameof(jsonResponse));

            var jsonData = JObject.Parse(jsonResponse);

            this.PostCode = jsonData.SelectToken("zip")?.ToString() ?? string.Empty;
            this.Name = jsonData.SelectToken("name")?.ToString() ?? string.Empty;
            this.Country = jsonData.SelectToken("country")?.ToString() ?? string.Empty;
            this.Coordinate = new Point(double.Parse(jsonData.SelectToken("lat").ToString()), double.Parse(jsonData.SelectToken("lon").ToString()));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the raw JSON.
        /// </summary>
        public string RawJson { get; }

        /// <summary>
        /// Gets the post code.
        /// </summary>
        public string PostCode { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        public Point Coordinate { get; }

        #endregion
    }
}
