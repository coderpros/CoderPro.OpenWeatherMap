// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Components.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the Components type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable StyleCop.SA1650
// ReSharper disable InconsistentNaming
#pragma warning disable CS8604
namespace CoderPro.OpenWeatherMap.Wrapper.Models.AirPollution
{
    #region Usings

    using System.Globalization;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The components.
    /// </summary>
    public class Components
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Components"/> class.
        /// </summary>
        /// <param name="componentData">
        /// The component data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if there is no component data present in the JSON response
        /// </exception>
        public Components(JToken componentData)
        {
            if (componentData is null)
            {
                throw new ArgumentNullException(nameof(componentData));
            }

            this.CO = double.Parse(componentData.SelectToken("co")?.ToString(), CultureInfo.InvariantCulture);
            this.NO = double.Parse(componentData.SelectToken("no")?.ToString(), CultureInfo.InvariantCulture);
            this.NO2 = double.Parse(componentData.SelectToken("no2")?.ToString(), CultureInfo.InvariantCulture);
            this.O3 = double.Parse(componentData.SelectToken("o3")?.ToString(), CultureInfo.InvariantCulture);
            this.SO2 = double.Parse(componentData.SelectToken("so2")?.ToString(), CultureInfo.InvariantCulture);
            this.PM25 = double.Parse(componentData.SelectToken("pm2_5")?.ToString(), CultureInfo.InvariantCulture);
            this.PM10 = double.Parse(componentData.SelectToken("pm10")?.ToString(), CultureInfo.InvariantCulture);
            this.NH3 = double.Parse(componentData.SelectToken("nh3")?.ToString(), CultureInfo.InvariantCulture);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the carbon monoxide level. Measured in μg/m3.
        /// </summary>
        public double CO { get; }

        /// <summary>
        /// Gets the nitrogen monoxide level. Measured in μg/m3.
        /// </summary>
        public double NO { get; }

        /// <summary>
        /// Gets the nitrogen dioxide level. Measured in μg/m3.
        /// </summary>
        public double NO2 { get; }

        /// <summary>
        /// Gets the ozone level. Measured in μg/m3.
        /// </summary>
        public double O3 { get; }

        /// <summary>
        /// Gets the sulfur dioxide level. Measured in μg/m3.
        /// </summary>
        public double SO2 { get; }

        /// <summary>
        /// Gets the fine particulate matter. Measured in μg/m3.
        /// </summary>
        public double PM25 { get; }

        /// <summary>
        /// Gets the coarse particulate matter. Measured in μg/m3.
        /// </summary>
        public double PM10 { get; }

        /// <summary>
        /// Gets the ammonia level. Measured in μg/m3.
        /// </summary>
        public double NH3 { get; }

        #endregion
    }
}
