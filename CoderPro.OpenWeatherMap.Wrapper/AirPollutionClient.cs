// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AirPollutionClient.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the AirPollutionClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper
{
    /// <summary>
    /// The air pollution client.
    /// </summary>
    public sealed class AirPollutionClient : IDisposable
    {
        #region Private Variables

        /// <summary>
        /// The API key.
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// The HTTP client used to access the API.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Flag specifying whether or not to use https.
        /// </summary>
        private readonly bool _useHttps;

        /// <summary>
        /// Specifies if the object has been disposed of.
        /// </summary>
        private bool _disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AirPollutionClient"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The API key.
        /// </param>
        /// <param name="useHttps">
        /// Flag whether or not to use https.
        /// </param>
        public AirPollutionClient(string apiKey, bool useHttps = false)
        {
            this._apiKey = apiKey;
            this._httpClient = new HttpClient();
            this._useHttps = useHttps;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously gets the current pollution level.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate.
        /// </param>
        /// <returns>
        /// Returns current pollution level or null if the query is invalid.
        /// </returns>
        public async Task<Models.AirPollution.CurrentResponse> CurrentPollutionAsync(NetTopologySuite.Geometries.Point coordinate)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(Models.AirPollution.SearchType.Forecast, coordinate: coordinate)).ConfigureAwait(false);
            var query = new Models.AirPollution.CurrentResponse(jsonResponse);

            return query;
        }

        /// <summary>
        /// Gets the current pollution level. Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate.
        /// </param>
        /// <returns>
        /// Returns current pollution level or null if the query is invalid.
        /// </returns>
        [Obsolete("Use async version wherever possible.")]
        public Models.AirPollution.CurrentResponse CurrentPollution(NetTopologySuite.Geometries.Point coordinate)
        {
            return Task.Run(async () => await this.CurrentPollutionAsync(coordinate).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously gets historical pollution levels.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate.
        /// </param>
        /// <param name="startDate">
        /// The start Date.
        /// </param>
        /// <param name="endDate">
        /// The end Date.
        /// </param>
        /// <param name="timeZone">
        /// The time zone offset.
        /// </param>
        /// <returns>
        /// Returns null if the query is invalid.
        /// </returns>
        public async Task<Models.AirPollution.ForecastResponse> HistoryPollutionAsync(NetTopologySuite.Geometries.Point coordinate, DateTime startDate, DateTime endDate, double timeZone)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(Models.AirPollution.SearchType.History, coordinate: coordinate, startDate, endDate)).ConfigureAwait(false);
            var query = new Models.AirPollution.ForecastResponse(jsonResponse, timeZone);

            return query;
        }

        /// <summary>
        /// Gets the historical pollution levels. Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate.
        /// </param>
        /// <param name="startDate">
        /// The start Date.
        /// </param>
        /// <param name="endDate">
        /// The end Date.
        /// </param>
        /// <param name="timeZone">
        /// The time zone offset.
        /// </param>
        /// <returns>
        /// Returns null if the query is invalid.
        /// </returns>
        [Obsolete("Use Async version wherever possible.")]
        public Models.AirPollution.ForecastResponse HistoryPollution(NetTopologySuite.Geometries.Point coordinate, DateTime startDate, DateTime endDate, double timeZone)
        {
            return Task.Run(async () => await this.HistoryPollutionAsync(coordinate, startDate, endDate, timeZone).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        #endregion

        #region Inherited Methods

        /// <summary>
        /// Standard garbage collection.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            this.Dispose(true);

            // Suppress finalization.
            // * Not necessary in a sealed class. Included for when someone inevitably unseals the class.
            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Standard garbage collection and resource cleanup.
        /// </summary>
        /// <param name="disposing">
        /// Flag specifying that the object is or is not presently being disposed of.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                // dispose managed objects.
            }

            // Free unmanaged resources (unmanaged objects) and override a finalizer below.

            // Set large fields to null.
            this._httpClient.Dispose();
            this._disposed = true;
        }

        #endregion 

        #region Helpers

        /// <summary>
        /// The generate request URL helper function.
        /// </summary>
        /// <param name="searchType">
        /// The search type.
        /// </param>
        /// <param name="coordinate">
        /// The coordinate.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <returns>
        /// The appropriate <see cref="Uri">URL</see> to the OpenWeather API.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if an unexpected search type argument is passed to the function.
        /// </exception>
        private Uri GenerateRequestUrl(Models.AirPollution.SearchType searchType, NetTopologySuite.Geometries.Point? coordinate = null, DateTime startDate = default, DateTime endDate = default)
        {
            var scheme = "http";

            if (this._useHttps)
            {
                scheme = "https";
            }

            if (coordinate != null)
            {
                switch (searchType)
                {
                    case Models.AirPollution.SearchType.Forecast:
                        return new Uri(
                            $"{scheme}://api.openweathermap.org/data/2.5/air_pollution?appid={this._apiKey}&lat={coordinate.X}&lon={coordinate.Y}");
                    case Models.AirPollution.SearchType.History:
                        return new Uri(
                            $"{scheme}://api.openweathermap.org/data/2.5/air_pollution/history?appid={this._apiKey}&lat={coordinate.X}&lon={coordinate.Y}&start={Common.ConvertDateTimeToUnix(startDate)}&end={Common.ConvertDateTimeToUnix(endDate)}");
                }
            }

            throw new ArgumentException("The arguments specified were invalid.");
        }
        #endregion
    }
}
