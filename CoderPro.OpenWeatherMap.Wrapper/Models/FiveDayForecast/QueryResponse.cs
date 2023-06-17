// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponse.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Query Response type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CoderPro.OpenWeatherMap.Wrapper.Models.FiveDayForecast
{
    #region Usings

    using System.Globalization;

    using CoderPro.OpenWeatherMap.Wrapper.Models.Common;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The query response.
    /// </summary>
    public class QueryResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse"/> class.
        /// </summary>
        /// <param name="jsonResponse">
        /// The JSON response.
        /// </param>
        public QueryResponse(string jsonResponse)
        {
            this.RawJson = jsonResponse ?? throw new ArgumentNullException(nameof(jsonResponse));

            var jsonData = JObject.Parse(jsonResponse);

            this.ResponseCode = int.Parse(jsonData.SelectToken("cod")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);

            if (this.ResponseCode == 200)
            {
                this.ValidRequest = true;
                this.Count = short.Parse(jsonData.SelectToken("cnt")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
                this.Message = double.Parse(jsonData.SelectToken("message")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
                this.City = new Models.Common.City(jsonData.SelectToken("city"));
                this.ForecastList = new List<Forecast>();

                var list = jsonData.SelectToken("list") as JArray;

                foreach (var jsonForecast in list)
                {
                    var forecast = new Forecast(jsonForecast.ToString());

                    forecast.Date = forecast.Date.AddSeconds(this.City.TimeZone);

                    this.ForecastList.Add(forecast);
                }
            }
            else
            {
                this.ValidRequest = false;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the raw json.
        /// </summary>
        public string RawJson { get; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        public City City { get; }

        /// <summary>
        /// Gets the code returned from the OpenWeatherMap API.
        /// </summary>
        public int ResponseCode { get; }

        /// <summary>
        /// Gets the number of records returned.
        /// </summary>
        public short Count { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public double Message { get; }

        /// <summary>
        /// Gets the valid request.
        /// </summary>
        public bool ValidRequest { get; }

        /// <summary>
        /// Gets the forecast list.
        /// </summary>
        public List<Forecast> ForecastList { get; }

        #endregion
    }
}
