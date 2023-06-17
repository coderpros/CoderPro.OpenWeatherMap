// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the application settings view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable StyleCop.SA1402 
namespace CoderPro.OpenWeatherMap.UI.Wpf.ViewModels
{
    /// <summary>
    /// The application settings.
    /// </summary>
    public class ApplicationSettings
    {
        #region Constructors

        public ApplicationSettings()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the weather.
        /// </summary>
        public Weather Weather { get; set; }

        #endregion
    }

    /// <summary>
    /// The weather view model.
    /// </summary>
    public class Weather
    {
        #region Properties

        /// <summary>
        /// Gets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets the province.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets the default unit of measure.
        /// </summary>
        public string DefaultUOM { get; set; }

        /// <summary>
        /// Gets the Spatial Reference Id (default for the app).
        /// </summary>
        public int SRID { get; set; }

        #endregion 
    }
}
