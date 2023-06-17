// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Temperature.cs" company="">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Temperature type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    using Common = CoderPro.OpenWeatherMap.Wrapper.Common;

    /// <summary>
    /// The temperature.
    /// </summary>
    public class Temperature
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Temperature"/> class.
        /// </summary>
        /// <param name="temp">
        /// The current temperature.
        /// </param>
        /// <param name="min">
        /// The minimum temperature.
        /// </param>
        /// <param name="max">
        /// The maximum temperature.
        /// </param>
        /// <param name="like">
        /// The feels like temperature.
        /// </param>
        public Temperature(double temp, double min, double max, double like)
        {
            this.KelvinCurrent = temp;
            this.KelvinMaximum = max;
            this.KelvinMinimum = min;
            this.KelvinFeelsLike = like;

            this.CelsiusCurrent = Common.ConvertToCelsius(this.KelvinCurrent);
            this.CelsiusMaximum = Common.ConvertToCelsius(this.KelvinMaximum);
            this.CelsiusMinimum = Common.ConvertToCelsius(this.KelvinMinimum);
            this.CelsiusFeelsLike = Common.ConvertToCelsius(this.KelvinFeelsLike);

            this.FahrenheitCurrent = Common.ConvertToFahrenheit(this.CelsiusCurrent);
            this.FahrenheitMaximum = Common.ConvertToFahrenheit(this.CelsiusMaximum);
            this.FahrenheitMinimum = Common.ConvertToFahrenheit(this.CelsiusMinimum);
            this.FahrenheitFeelsLike = Common.ConvertToFahrenheit(this.CelsiusFeelsLike);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets current temperature in celsius.
        /// </summary>
        public double CelsiusCurrent { get; }

        /// <summary>
        /// Gets the current temperature in fahrenheit.
        /// </summary>
        public double FahrenheitCurrent { get; }

        /// <summary>
        /// Gets the current temperature in kelvin.
        /// </summary>
        public double KelvinCurrent { get; }

        /// <summary>
        /// Gets the current minimum temperature in celsius for the metro area.
        /// </summary>
        public double CelsiusMinimum { get; }

        /// <summary>
        /// Gets the current maximum temperature in celsius for the metro area.
        /// </summary>
        public double CelsiusMaximum { get; }

        /// <summary>
        /// Gets the current "feels like" temperature in celsius.
        /// </summary>
        public double CelsiusFeelsLike { get; }

        /// <summary>
        /// Gets the minimum temperature in fahrenheit for the metro area
        /// </summary>
        public double FahrenheitMinimum { get; }

        /// <summary>
        /// Gets the maximum temperature in fahrenheit for the metro area.
        /// </summary>
        public double FahrenheitMaximum { get; }

        /// <summary>
        /// Gets "feels like" temperature in Fahrenheit.
        /// </summary>
        public double FahrenheitFeelsLike { get; }

        /// <summary>
        /// Gets the minimum temperature in kelvin for the metro area.
        /// </summary>
        public double KelvinMinimum { get; }

        /// <summary>
        /// Gets the maximum temperature in kelvin for the metro area.
        /// </summary>
        public double KelvinMaximum { get; }

        /// <summary>
        /// Gets the "feels like" temperature in kelvin.
        /// </summary>
        public double KelvinFeelsLike { get; }

        #endregion
    }
}
