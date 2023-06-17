// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationQueryResponse.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the LocationQueryResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.GeoCoding
{
    #region Usings

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The location query response.
    /// </summary>
    public class LocationQueryResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationQueryResponse"/> class.
        /// </summary>
        /// <param name="jsonResponse">
        /// The JSON response.
        /// </param>
        public LocationQueryResponse(string jsonResponse)
        {
            this.RawJson = jsonResponse ?? throw new ArgumentNullException(nameof(jsonResponse));

            var jsonData = JArray.Parse(jsonResponse);

            this.LocationList = new List<Location>();

            foreach (var location in jsonData)
            {
                this.LocationList?.Add(new Location(location));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the raw JSON.
        /// </summary>
        public string RawJson { get; }

        /// <summary>
        /// Gets the location list.
        /// </summary>
        public List<Location> LocationList { get; }

        #endregion
    }
}
