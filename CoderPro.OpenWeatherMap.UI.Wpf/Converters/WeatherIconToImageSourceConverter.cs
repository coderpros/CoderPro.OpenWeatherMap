// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeatherIconToImageSourceConverter.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the WeatherIconToImageSourceConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.UI.Wpf.Converters
{
    #region Usings

    using System;
    using System.Globalization;
    using System.Windows.Data;

    #endregion

    /// <summary>
    /// The weather icon to image source converter.
    /// </summary>
    public class WeatherIconToImageSourceConverter : IValueConverter
    {
        #region Interface Methods

        /// <summary>
        /// The convert method.
        /// </summary>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"https://openweathermap.org/img/w/{value}.png";
        }

        /// <summary>
        /// The convert back method.
        /// </summary>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <param name="targetTypes">
        /// The target types.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This method has not been implemented.
        /// </exception>
        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
