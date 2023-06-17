// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindConverter.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the WindConverter type.
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
    /// The wind converter.
    /// </summary>
    public class WindConverter : IMultiValueConverter
    {
        #region Interface Methods

        /// <summary>
        /// The convert method replaces wind directions _ with a space (" ") .
        /// </summary>
        /// <param name="value">
        /// The original value.
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
        /// The converted <see cref="object"/>.
        /// </returns>
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value[0]?.ToString()?.Replace("_", " ")} ({value[1]}°)";
        }

        /// <summary>
        /// The convert back method returns the original value before conversion.
        /// </summary>
        /// <param name="value">
        /// The value.
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
        /// The <see cref="object[]" /> containing the original value(s) .
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This method has not been implemented.
        /// </exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}