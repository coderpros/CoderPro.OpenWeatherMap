// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoCodingClient.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the GeoCodingClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper
{
    #region Usings

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using CoderPro.OpenWeatherMap.Wrapper.Models.GeoCoding;

    #endregion

    /// <summary>
    /// The geo coding client.
    /// </summary>
    public sealed class GeoCodingClient : IDisposable
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

        #region Consturctors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCodingClient"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The API key.
        /// </param>
        /// <param name="useHttps">
        /// Flag whether or not to use https.
        /// </param>
        public GeoCodingClient(string apiKey, bool useHttps = false)
        {
            this._apiKey = apiKey;
            this._httpClient = new HttpClient();
            this._useHttps = useHttps;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously gets the location.
        /// </summary>
        /// <param name="queryString">
        /// The location being searched for.
        /// </param>
        /// <param name="limit">
        /// The maximum number of locations to be returned.
        /// </param>
        /// <returns>
        /// Returns <see cref="LocationQueryResponse"/> or null if the query is invalid.
        /// </returns>
        public async Task<LocationQueryResponse> QueryCoordinatesAsync(string queryString, int? limit = null)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(Models.GeoCoding.Type.CoordinatesByLocationName, queryString, limit)).ConfigureAwait(false);
            var query = new LocationQueryResponse(jsonResponse);

            return query;
        }

        /// <summary>
        /// Gets the location. Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="queryString">
        /// The location being searched for.
        /// </param>
        /// <param name="limit">
        /// The maximum number of locations to be returned.
        /// </param>
        /// <returns>
        /// Returns <see cref="LocationQueryResponse"/> or null if the query is invalid.
        /// </returns>
        [Obsolete("Use Async version wherever possible.")]
        public LocationQueryResponse QueryCoordinates(string queryString, int? limit = null)
        {
            return Task.Run(async () => await this.QueryCoordinatesAsync(queryString, limit).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronous reverse geo lookup.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate being searched for.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// Returns <see cref="LocationQueryResponse"/> or null if the query is invalid.
        /// </returns>
        public async Task<LocationQueryResponse> QueryReverseAsync(NetTopologySuite.Geometries.Point coordinate, int? limit = null)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(type: Models.GeoCoding.Type.Reverse, coordinate: coordinate, limit: limit)).ConfigureAwait(false);
            var query = new LocationQueryResponse(jsonResponse);

            return query;
        }

        /// <summary>
        /// Reverse geo lookup Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate being searched for.
        /// </param>
        /// <param name="limit">
        /// The maximum number of locations that can be returned.
        /// </param>
        /// <returns>
        /// Returns <see cref="LocationQueryResponse"/> or null if the query is invalid.
        /// </returns>
        [Obsolete("Use Async version wherever possible.")]
        public LocationQueryResponse QueryReverse(NetTopologySuite.Geometries.Point coordinate, int? limit = null)
        {
            return Task.Run(async () => await this.QueryReverseAsync(coordinate, limit).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronous geo lookup by postcode.
        /// </summary>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The <see cref="PostCodeQueryResponse"/> or null if the query is invalid.
        /// </returns>
        public async Task<PostCodeQueryResponse> QueryCoordinatesByPostCodeAsync(string queryString)
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(Models.GeoCoding.Type.CoordinatesByPostCode, queryString)).ConfigureAwait(false);
            var query = new PostCodeQueryResponse(jsonResponse);

            return query;
        }

        /// <summary>
        /// Geo lookup by postcode Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="queryString">
        /// The query string
        /// </param>
        /// <returns>
        /// The <see cref="PostCodeQueryResponse"/> or null if the query is invalid.
        /// </returns>
        [Obsolete("Use Async version wherever possible.")]
        public PostCodeQueryResponse QueryCoordinatesByPostCode(string queryString)
        {
            return Task.Run(async () => await this.QueryCoordinatesByPostCodeAsync(queryString).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
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
        /// <param name="type">
        /// The type of GeoCoding Request <see cref="System.Type"/>.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <param name="limit">
        /// The maximum number of records to be returned.
        /// </param>
        /// <param name="coordinate">
        /// The coordinate to be searched.
        /// </param>
        /// <returns>
        /// The appropriate <see cref="Uri">URL</see> to the OpenWeather API.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if an unexpected search type argument is passed to the function.
        /// </exception>
        private Uri GenerateRequestUrl(Models.GeoCoding.Type type, string? queryString = null, int? limit = null, NetTopologySuite.Geometries.Point? coordinate = null)
        {
            var scheme = "http";

            if (this._useHttps)
            {
                scheme = "https";
            }

            switch (type)
            {
                case Models.GeoCoding.Type.CoordinatesByLocationName:
                    return new Uri(
                        $"{scheme}://api.openweathermap.org/geo/1.0/direct?q={queryString}&limit={limit}&appid={this._apiKey}");
                case Models.GeoCoding.Type.CoordinatesByPostCode:
                    return new Uri(
                        $"{scheme}://api.openweathermap.org/geo/1.0/zip?zip={queryString}&appid={this._apiKey}");
                case Models.GeoCoding.Type.Reverse:
                    return new Uri(
                        $"{scheme}://api.openweathermap.org/geo/1.0/reverse?lat={coordinate.X}&lon={coordinate.Y}&limit={limit}&appid={this._apiKey}");
            }

            throw new ArgumentException("The arguments specified were invalid.");
        }

        #endregion
    }
}