// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wind.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Wind type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CoderPro.OpenWeatherMap.Wrapper.Models.CurrentWeather
{
    #region Usings

    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The wind.
    /// </summary>
    public class Wind
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Wind"/> class.
        /// </summary>
        /// <param name="windData">
        /// The wind data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when windData is null.
        /// </exception>
        public Wind(JToken windData)
        {
            if (windData is null)
            {
                throw new ArgumentNullException(nameof(windData));
            }

            this.SpeedMetersPerSecond = double.Parse(windData.SelectToken("speed")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.SpeedFeetPerSecond = this.SpeedMetersPerSecond * 3.28084;
            this.SpeedKilometersPerHour = this.SpeedMetersPerSecond * 3.6;
            this.SpeedMilesPerHour = this.SpeedFeetPerSecond * 0.681818;
            this.Degree = double.Parse(windData.SelectToken("deg")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            this.Direction = this.AssignDirection(this.Degree);

            if (windData.SelectToken("gust") != null)
            {
                this.Gust = double.Parse(windData.SelectToken("gust")?.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
            }
        }

        #endregion

        /// <summary>
        /// The direction enum.
        /// </summary>
        [SuppressMessage("ReSharper", "StyleCop.SA1602", Justification = "Values are self explanatory.")]
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Values are consistent with values returned from the API.")]
        public enum DirectionEnum
        {
            North,
            North_North_East,
            North_East,
            East_North_East,
            East,
            East_South_East,
            South_East,
            South_South_East,
            South,
            South_South_West,
            South_West,
            West_South_West,
            West,
            West_North_West,
            North_West,
            North_North_West,
            Unknown
        }

        #region Properties

        /// <summary>
        ///  Gets the speed in meters per second.
        /// </summary>
        public double SpeedMetersPerSecond { get; }

        /// <summary>
        /// Gets the speed in feet per second
        /// </summary>
        public double SpeedFeetPerSecond { get; }

        /// <summary>
        /// Gets the Speed in kilometers per hour.
        /// </summary>
        public double SpeedKilometersPerHour { get; }

        /// <summary>
        /// Gets the speed in miles per hour.
        /// </summary>
        public double SpeedMilesPerHour { get; }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        public DirectionEnum Direction { get; }

        /// <summary>
        /// Gets the degree.
        /// </summary>
        public double Degree { get; }

        /// <summary>
        /// Gets the gust speed. 
        /// </summary>
        public double Gust { get; }

        #endregion
        
        #region Helpers

        /// <summary>
        /// The direction enum to string converts the enum variable to its corresponding English text.
        /// </summary>
        /// <param name="dir">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DirectionEnumToString(DirectionEnum dir)
        {
            switch (dir)
            {
                case DirectionEnum.East:
                    return "East";
                case DirectionEnum.East_North_East:
                    return "East North-East";
                case DirectionEnum.East_South_East:
                    return "East South-East";
                case DirectionEnum.North:
                    return "North";
                case DirectionEnum.North_East:
                    return "North East";
                case DirectionEnum.North_North_East:
                    return "North North-East";
                case DirectionEnum.North_North_West:
                    return "North North-West";
                case DirectionEnum.North_West:
                    return "North West";
                case DirectionEnum.South:
                    return "South";
                case DirectionEnum.South_East:
                    return "South East";
                case DirectionEnum.South_South_East:
                    return "South South-East";
                case DirectionEnum.South_South_West:
                    return "South South-West";
                case DirectionEnum.South_West:
                    return "South West";
                case DirectionEnum.West:
                    return "West";
                case DirectionEnum.West_North_West:
                    return "West North-West";
                case DirectionEnum.West_South_West:
                    return "West South-West";
                case DirectionEnum.Unknown:
                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// The falls between function determines if the specified val falls between the min and max.
        /// </summary>
        /// <param name="val">
        /// The value.
        /// </param>
        /// <param name="min">
        /// The minimum.
        /// </param>
        /// <param name="max">
        /// The maximum.
        /// </param>
        /// <returns>
        /// <see cref="bool"/>.
        /// </returns>
        private static bool FallsBetween(double val, double min, double max)
        {
            return min <= val && val <= max;
        }

        /// <summary>
        /// The assign direction function assigns a direction to a degree.
        /// </summary>
        /// <param name="degree">
        /// The degree.
        /// </param>
        /// <returns>
        /// The <see cref="DirectionEnum"/>.
        /// </returns>
        [SuppressMessage("ReSharper", "StyleCop.SA1503", Justification = "Because it looks cleaner")]
        private DirectionEnum AssignDirection(double degree)
        {
            if (FallsBetween(degree, 348.75, 360))
                return DirectionEnum.North;
            if (FallsBetween(degree, 0, 11.25))
                return DirectionEnum.North;
            if (FallsBetween(degree, 11.25, 33.75))
                return DirectionEnum.North_North_East;
            if (FallsBetween(degree, 33.75, 56.25))
                return DirectionEnum.North_East;
            if (FallsBetween(degree, 56.25, 78.75))
                return DirectionEnum.East_North_East;
            if (FallsBetween(degree, 78.75, 101.25))
                return DirectionEnum.East;
            if (FallsBetween(degree, 101.25, 123.75))
                return DirectionEnum.East_South_East;
            if (FallsBetween(degree, 123.75, 146.25))
                return DirectionEnum.South_East;
            if (FallsBetween(degree, 168.75, 191.25))
                return DirectionEnum.South;
            if (FallsBetween(degree, 191.25, 213.75))
                return DirectionEnum.South_South_West;
            if (FallsBetween(degree, 213.75, 236.25))
                return DirectionEnum.South_West;
            if (FallsBetween(degree, 236.25, 258.75))
                return DirectionEnum.West_South_West;
            if (FallsBetween(degree, 258.75, 281.25))
                return DirectionEnum.West;
            if (FallsBetween(degree, 281.25, 303.75))
                return DirectionEnum.West_North_West;
            if (FallsBetween(degree, 303.75, 326.25))
                return DirectionEnum.North_West;
            if (FallsBetween(degree, 326.25, 348.75))
                return DirectionEnum.North_North_West;

            return DirectionEnum.Unknown;
        }

        #endregion
    }
}
