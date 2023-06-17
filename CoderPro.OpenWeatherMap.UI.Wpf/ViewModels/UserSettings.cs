// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserSettings.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   The user settings view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf.ViewModels
{
    /// <summary>
    /// The user settings view model.
    /// </summary>
    public class UserSettings
    {
        #region Constructors

        public UserSettings()
        {
            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the theme is dark or light.
        /// </summary>
        public bool IsDarkTheme { get; set; }

        #endregion
    }
}
