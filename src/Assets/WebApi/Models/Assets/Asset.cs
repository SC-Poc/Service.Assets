﻿using System;

namespace Assets.WebApi.Models.Assets
{
    /// <summary>
    /// Represents an asset.
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// The human-readable name.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// The additional information.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The number of decimal places.
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// Indicates that the asset is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// The creation date and time.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The last update date and time.
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
