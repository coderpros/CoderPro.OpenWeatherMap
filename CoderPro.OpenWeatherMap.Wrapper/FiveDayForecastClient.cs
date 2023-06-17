// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiveDayForecastClient.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the FiveDayForecastClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper
{
    using CoderPro.OpenWeatherMap.Wrapper.Models.Common;

    /// <summary>
    /// The five day forecast client.
    /// </summary>
    public sealed class FiveDayForecastClient : IDisposable
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
        /// Initializes a new instance of the <see cref="FiveDayForecastClient"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The API key.
        /// </param>
        /// <param name="useHttps">
        /// Flag whether or not to use https.
        /// </param>
        public FiveDayForecastClient(string apiKey, bool useHttps = false)
        {
            this._apiKey = apiKey;
            this._httpClient = new HttpClient();
            this._useHttps = useHttps;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously gets the forecasts for every three hours for up to five days.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate being searched.
        /// </param>
        /// <param name="unit">
        /// The <see cref="Unit">unit of measure</see>.
        /// </param>
        /// <param name="mode">
        /// The <see cref="Mode">response format</see>.
        /// </param>
        /// <param name="limit">
        /// The maximum number of forecasts to be returned.
        /// </param>
        /// <param name="lang">
        /// The language (two letter ISO standard).
        /// </param>
        /// <returns>
        /// The <see cref="Models.FiveDayForecast.QueryResponse"/>.
        /// </returns>
        public async Task<Models.FiveDayForecast.QueryResponse> QueryAsync(
            NetTopologySuite.Geometries.Point coordinate,
            Unit unit = Unit.Standard,
            Mode mode = Mode.JSON,
            short limit = short.MaxValue,
            string lang = "en")
        {
            var jsonResponse = await this._httpClient.GetStringAsync(this.GenerateRequestUrl(coordinate, unit, mode, limit, lang)).ConfigureAwait(false);
            var query = new Models.FiveDayForecast.QueryResponse(jsonResponse);

            // return query.ValidRequest ? query : null;
            return query;
        }

        /// <summary>
        /// Gets the forecasts for every three hours for up to five days. Non-async version. Use for legacy code, use Async version wherever possible.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate being searched.
        /// </param>
        /// <param name="unit">
        /// The <see cref="Unit">unit of measure</see>.
        /// </param>
        /// <param name="mode">
        /// The <see cref="Mode">response format</see>.
        /// </param>
        /// <param name="limit">
        /// The maximum number of forecasts to be returned.
        /// </param>
        /// <param name="lang">
        /// The language (two letter ISO standard).
        /// </param>
        /// <returns>
        /// The <see cref="Models.FiveDayForecast.QueryResponse"/>.
        /// </returns>
        [Obsolete("Use async version whenever possible.")]
        public Models.FiveDayForecast.QueryResponse Query(
            NetTopologySuite.Geometries.Point coordinate,
            Unit unit = Unit.Standard,
            Mode mode = Mode.JSON,
            short limit = short.MaxValue,
            string lang = "en")
        {
            return Task.Run(async () => await this.QueryAsync(coordinate, unit, mode, limit, lang).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
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
        /// <param name="coordinate">
        /// The <see cref="NetTopologySuite.Geometries.Point">coordinate</see>.
        /// </param>
        /// <param name="unit">
        /// The <see cref="Unit">unit of measure</see>.
        /// </param>
        /// <param name="mode">
        /// The <see cref="Mode">mode</see>.
        /// </param>
        /// <param name="limit">
        /// The maximum number of records returned.
        /// </param>
        /// <param name="lang">
        /// The language (standard two character string).
        /// </param>
        /// <returns>
        /// The <see cref="Uri"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// XML Mode will be implemented in a future release.
        /// </exception>
        private Uri GenerateRequestUrl(
            NetTopologySuite.Geometries.Point coordinate,
            Unit unit = Unit.Standard,
            Mode mode = Mode.JSON,
            short limit = short.MaxValue,
            string lang = "en")
        {
            var scheme = "http";

            if (this._useHttps)
            {
                scheme = "https";
            }

            if (mode == Mode.XML)
            {
                throw new NotImplementedException("This feature is not yet implemented.");
            }

            return new Uri($"{scheme}://api.openweathermap.org/data/2.5/forecast?lat={coordinate.X}&lon={coordinate.Y}"
                           + $"&unit={unit}&mode={mode}&cnt={limit}&lang={lang}&appid={this._apiKey}");
        }

        #endregion
    }
}
