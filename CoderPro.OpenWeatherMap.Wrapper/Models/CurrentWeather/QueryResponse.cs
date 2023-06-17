// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponse.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the QueryResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Collections.Generic;
    using System.Globalization;

    using Newtonsoft.Json.Linq;

    using Common = CoderPro.OpenWeatherMap.Wrapper.Common;

    #endregion

    /// <summary>
    /// The query response.
    /// </summary>
    public class QueryResponse
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse"/> class.
        /// </summary>
        /// <param name="jsonResponse">
        /// The JSON response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if jsonResponse is null.
        /// </exception>
        public QueryResponse(string jsonResponse)
        {
            this.RawJson = jsonResponse ?? throw new ArgumentNullException(nameof(jsonResponse));

            var jsonData = JObject.Parse(jsonResponse);

            // if (jsonData.SelectToken("cod").ToString() == "200")
            // {
            this.ValidRequest = true;

            if (jsonData.SelectToken("coord") != null)
            {
                this.Coordinate = new NetTopologySuite.Geometries.Point(double.Parse(jsonData.SelectToken("coord").SelectToken("lat").ToString()), double.Parse(jsonData.SelectToken("coord").SelectToken("lon").ToString()));
            }

            foreach (var weather in jsonData.SelectToken("weather"))
            {
                this.WeatherList.Add(new Weather(weather));
            }

            this.Base = jsonData.SelectToken("base")?.ToString() ?? string.Empty;
            this.Name = jsonData.SelectToken("name")?.ToString() ?? string.Empty;
            this.Main = new Main(jsonData.SelectToken("main"));
            this.Date = Common.ConvertUnixToDateTime(long.Parse(jsonData.SelectToken("dt")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture));

            if (jsonData.SelectToken("visibility") != null)
            {
                this.Visibility = double.Parse(jsonData.SelectToken("visibility").ToString(), CultureInfo.InvariantCulture);
            }

            if (jsonData.SelectToken("rain") != null)
            {
                this.Rain = new Rain(jsonData.SelectToken("rain"));
            }

            if (jsonData.SelectToken("snow") != null)
            {
                this.Snow = new Snow(jsonData.SelectToken("snow"));
            }

            if (jsonData.SelectToken("id") != null)
            {
                this.Id = int.Parse(jsonData.SelectToken("id").ToString(), CultureInfo.InvariantCulture);
            }

            if (jsonData.SelectToken("cod") != null)
            {
                this.ResponseCode = int.Parse(jsonData.SelectToken("cod").ToString(), CultureInfo.InvariantCulture);
            }

            if (jsonData.SelectToken("timezone") != null)
            {
                this.TimeZone = int.Parse(jsonData.SelectToken("timezone").ToString(), CultureInfo.InvariantCulture);
            }

            this.Wind = new Wind(jsonData.SelectToken("wind"));
            this.Clouds = new Clouds(jsonData.SelectToken("clouds"));
            this.Sys = new Sys(jsonData.SelectToken("sys"));

            // Update times with given offset.
            if (this.TimeZone != 0)
            {
                this.Date = this.Date.AddSeconds(this.TimeZone);
                this.Sys.Sunrise = this.Sys.Sunrise.AddSeconds(this.TimeZone);
                this.Sys.Sunset = this.Sys.Sunset.AddSeconds(this.TimeZone);
            }

            // }
            // else
            // {
            //    this.ValidRequest = false;
            // }
        }

        #endregion

        #region Properties
        /// <summary>
        /// Code returned from the OpenWeatherMap API
        /// </summary>
        public int ResponseCode { get; }

        /// <summary>
        /// Gets a value indicating whether the request was or was not valid.
        /// </summary>
        public bool ValidRequest { get; }

        /// <summary>
        /// Gets the coordinate.
        /// </summary>
        public NetTopologySuite.Geometries.Point Coordinate { get; }

        /// <summary>
        /// Gets the base.
        /// </summary>
        public string Base { get; }

        /// <summary>
        /// Gets the main.
        /// </summary>
        public Main Main { get; }

        /// <summary>
        /// Gets the visibility.
        /// </summary>
        public double Visibility { get; }

        /// <summary>
        /// Gets the wind.
        /// </summary>
        public Wind Wind { get; }

        /// <summary>
        /// Gets the rain.
        /// </summary>
        public Rain Rain { get; }

        /// <summary>
        /// Gets the snow.
        /// </summary>
        public Snow Snow { get; }

        /// <summary>
        /// Gets the clouds.
        /// </summary>
        public Clouds Clouds { get; }

        /// <summary>
        /// Gets the sys.
        /// </summary>
        public Sys Sys { get; }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        public int TimeZone { get; } = 0;

        /// <summary>
        /// Gets the raw JSON.
        /// </summary>
        public string RawJson { get; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the weather list.
        /// </summary>
        public List<Weather> WeatherList { get; } = new List<Weather>();

        #endregion
    }
}
