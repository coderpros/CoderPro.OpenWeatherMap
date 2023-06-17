// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Common type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper
{
    /// <summary>
    /// The common class contains common routines used by multiple classes in the project.
    /// </summary>
    internal abstract class Common
    {
        #region Methods
        /// <summary>
        /// Converts a value from UNIX standard to datetime.
        /// </summary>
        /// <param name="unixTime">
        /// The unix time.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        internal static DateTime ConvertUnixToDateTime(long unixTime)
        {
            var x = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            
            return x.DateTime;
        }

        /// <summary>
        /// Converts a value from DateTime to UNIX standard datetime.
        /// </summary>
        /// <param name="dt">
        /// The DateTime.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        internal static long ConvertDateTimeToUnix(DateTime dt)
        {
            return ((DateTimeOffset)dt).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Converts a value from kelvin to celsius.
        /// </summary>
        /// <param name="celsius">
        /// The kelvin value.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        internal static double ConvertToFahrenheit(double celsius)
        {
            return Math.Round(9.0 / 5.0 * celsius + 32, 3);
        }

        /// <summary>
        /// Converts a value from kelvin to fahrenheit.
        /// </summary>
        /// <param name="kelvin">
        /// The kelvin value.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        internal static double ConvertToCelsius(double kelvin)
        {
            return Math.Round(kelvin - 273.15, 3);
        }
        #endregion
    }
}
