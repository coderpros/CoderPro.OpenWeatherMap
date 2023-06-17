// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CapitalizationConverter.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   The capitalization converter.
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
    /// The capitalization converter.
    /// </summary>
    public class CapitalizationConverter : IValueConverter
    {
        #region Interface Methods

        /// <summary>
        /// The convert method takes a string and capitalizes the letter of each word.
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
            var sa = value.ToString()?.Split(" ");
            var sb = new System.Text.StringBuilder();

            if (sa != null)
            {
                foreach (var s in sa)
                {
                    sb.Append($"{s.Substring(0, 1).ToUpperInvariant()}{s.Substring(1)} ");
                }
            }

            return sb.ToString().TrimEnd();
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
