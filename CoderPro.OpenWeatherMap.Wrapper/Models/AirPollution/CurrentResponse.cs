// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentResponse.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the CurrentResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution
{
    #region Usings

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The current response.
    /// </summary>
    public class CurrentResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentResponse"/> class.
        /// </summary>
        /// <param name="jsonResponse">
        /// The json response.
        /// </param>
        public CurrentResponse(string jsonResponse)
        {
            this.RawJson = jsonResponse ?? throw new ArgumentNullException(nameof(jsonResponse));

            var jsonData = JObject.Parse(jsonResponse);

            if (jsonData.SelectToken("coord") != null)
            {
                this.Coordinate = new NetTopologySuite.Geometries.Point(double.Parse(jsonData.SelectToken("coord")?.SelectToken("lat")?.ToString()), double.Parse(jsonData.SelectToken("coord")?.SelectToken("lon")?.ToString()));
            }

            if (jsonData.SelectToken("list") == null)
            {
                return;
            }

            this.PollutionList = new List<Pollution>();

            foreach (var pollution in jsonData.SelectToken("list"))
            {
                this.PollutionList.Add(new Pollution(pollution));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the raw JSON.
        /// </summary>
        public string RawJson { get; }

        /// <summary>
        /// Gets the coordinate.
        /// </summary>
        public NetTopologySuite.Geometries.Point Coordinate { get; }

        /// <summary>
        /// Gets the <see cref="Pollution">pollution list</see>.
        /// </summary>
        public List<Pollution> PollutionList { get; }

        #endregion
    }
}
