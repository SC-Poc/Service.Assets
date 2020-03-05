namespace Assets.WebApi.Models.AssetPairs
{
    /// <summary>
    /// Represents an asset pair create/update information.
    /// </summary>
    public class AssetPairEditModel
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The human-readable name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The base asset identifier.
        /// </summary>
        public string BaseAssetId { get; set; }

        /// <summary>
        /// The quoting asset identifier.
        /// </summary>
        public string QuotingAssetId { get; set; }

        /// <summary>
        /// The base asset accuracy.
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// The minimal allowed order volume.
        /// </summary>
        public decimal MinVolume { get; set; }

        /// <summary>
        /// The maximum allowed volume of <see cref="BaseAssetId"/>.
        /// </summary>
        public decimal MaxVolume { get; set; }

        /// <summary>
        /// The maximum allowed volume of <see cref="QuotingAssetId"/>.
        /// </summary>
        public decimal MaxOppositeVolume { get; set; }

        /// <summary>
        /// The market order price threshold.
        /// </summary>
        public decimal MarketOrderPriceThreshold { get; set; }
    }
}
