// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalName.cs" company="coderPro.net">
//   Copyright 2023 coderPro.net. All rights reserved.
// </copyright>
// <summary>
//   Defines the LocalName type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoderPro.OpenWeatherMap.Wrapper.Models.GeoCoding
{
    #region Usings

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// The local name.
    /// </summary>
    public class LocalName
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalName"/> class.
        /// </summary>
        /// <param name="localNameData">
        /// The local name data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if localNameData is null.
        /// </exception>
        public LocalName(JProperty localNameData)
        {
            if (localNameData is null)
            {
                throw new ArgumentNullException(nameof(localNameData));
            }

            this.LanguageCode = localNameData.Name;
            this.Name = localNameData.Value.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the language code.
        /// </summary>
        public string LanguageCode { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        #endregion
    }
}