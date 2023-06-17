// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeveloperSecrets.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the DeveloperSecrets view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf.ViewModels
{
    /// <summary>
    /// The developer secrets.
    /// </summary>
    public abstract class DeveloperSecrets
    {
        /// <summary>
        /// Gets the open weather map api key.
        /// </summary>
        public string? OpenWeatherMapApiKey { get; }
    }
}
