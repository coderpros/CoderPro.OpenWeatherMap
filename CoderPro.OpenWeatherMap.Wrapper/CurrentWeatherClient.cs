// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentWeatherClient.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the CurrentWeatherClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CoderPro.OpenWeatherMap.Wrapper
{
    #region Usings

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather;

    #endregion

    /// <summary>
    /// The current weather client.
    /// </summary>
    public sealed class CurrentWeatherClient : IDisposable
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
        /// Initializes a new instance of the <see cref="CurrentWeatherClient"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The API key.
        /// </param>
        /// <param name="useHttps">
        /// Flag whether or not to use https.
        /// </param>
        public CurrentWeatherClient(string apiKey, bool useHttps = false)
        {
            this._apiKey = apiKey;
            this._httpClient = new HttpClient();
            this._useHttps = useHttps;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously gets the current weather. 
        /// </summary>
        /// <param name="queryString">
        /// The {city, province, country}, or { city, state } (US Only) or {PostalCode} 
        /// </param>
        /// <param name="searchType">
        /// The <see cref="SearchType">search type</see>.
        /// </param>
        /// <returns>
        /// Returns <see cref="QueryResponse" /> or null if the query is invalid.
        /// </returns>
        public async Task<QueryResponse?> QueryAsync(string queryString, SearchType searchType)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(searchType, queryString)).ConfigureAwait(false);
            var query = new QueryResponse(jsonResponse);

            return query.ValidRequest ? query : null;
        }

        /// <summary>
        /// Get the current weather. Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="queryString">
        /// The {city, province, country}, or { city, state } (US Only) or {PostalCode} 
        /// </param>
        /// <param name="searchType">
        /// The search Type.
        /// </param>
        /// <returns>
        /// Returns <see cref="QueryResponse"/> or null if the query is invalid.
        /// </returns>
        [Obsolete("Use Async version wherever possible.")]
        public QueryResponse? Query(string queryString, SearchType searchType)
        {
            return Task.Run(async () => await this.QueryAsync(queryString, searchType).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously gets the current weather by coordinate.
        /// </summary>
        /// <param name="searchType">
        /// The search type.
        /// </param>
        /// <param name="coordinate">
        /// The <see cref="NetTopologySuite.Geometries.Point">coordinate</see> to be searched.
        /// </param>
        /// <returns>
        /// Returns <see cref="QueryResponse"/> or null if the query is invalid.
        /// </returns>
        public async Task<QueryResponse?> QueryByCoordinatesAsync(SearchType searchType, NetTopologySuite.Geometries.Point coordinate)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(searchType, coordinate: coordinate)).ConfigureAwait(false);
            var query = new QueryResponse(jsonResponse);

            return query.ValidRequest ? query : null;
        }

        /// <summary>
        /// Gets the current weather by coordinate. Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="searchType">
        /// The search type. See <see cref="SearchType"/>
        /// </param>
        /// <param name="coordinate">
        /// The coordinate to be searched.
        /// </param>
        /// <returns>
        /// Returns <see cref="QueryResponse" /> or null if the query is invalid.
        /// </returns>
        [Obsolete("Use Async version wherever possible.")]
        public QueryResponse? QueryByCoordinates(SearchType searchType, NetTopologySuite.Geometries.Point coordinate)
        {
            return Task.Run(async () => await this.QueryByCoordinatesAsync(searchType, coordinate).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
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
        /// <param name="queryString">
        /// The query string to search.
        /// </param>
        /// <param name="coordinate">
        /// The coordinate to search.
        /// </param>
        /// <returns>
        /// The <see cref="Uri"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if an unexpected search type argument is passed to the function.
        /// </exception>
        private Uri GenerateRequestUrl(SearchType searchType, string? queryString = null, NetTopologySuite.Geometries.Point? coordinate = null)
        {
            var scheme = "http";

            if (this._useHttps)
            {
                scheme = "https";
            }

            switch (searchType)
            {
                case SearchType.Coordinate:
                    return new Uri(
                        $"{scheme}://api.openweathermap.org/data/2.5/weather?appid={this._apiKey}&lat={coordinate.X}&lon={coordinate.Y}");
                case SearchType.LocationName:
                    return new Uri(
                        $"{scheme}://api.openweathermap.org/data/2.5/weather?appid={this._apiKey}&q={queryString}");
                default:
                    throw new Exception("Invalid search type specified.");
            }
        }

        #endregion
    }
}