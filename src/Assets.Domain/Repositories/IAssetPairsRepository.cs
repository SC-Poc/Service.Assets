﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Assets.Domain.Entities;

namespace Assets.Domain.Repositories
{
    public interface IAssetPairsRepository
    {
        Task<IReadOnlyList<AssetPair>> GetAllAsync();

        Task<IReadOnlyList<AssetPair>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<AssetPair>> GetAllAsync(
            string brokerId, string assetPairId, string name, string baseAssetId, string quoteAssetId, bool? isDisabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, string cursor = null, int limit = 50);

        Task<AssetPair> GetByIdAsync(string assetPairId);

        Task<AssetPair> InsertAsync(AssetPair assetPair);

        Task<AssetPair> UpdateAsync(AssetPair assetPair);

        Task DeleteAsync(string assetPairId);
    }
}
