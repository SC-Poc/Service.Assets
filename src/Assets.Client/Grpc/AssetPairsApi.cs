﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Assets.Client.Api;
using Assets.Client.Models.AssetPairs;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Service.Assets.Contracts;

namespace Assets.Client.Grpc
{
    internal class AssetPairsApi : IAssetPairsApi
    {
        private readonly AssetPairs.AssetPairsClient _client;

        public AssetPairsApi(string address)
        {
            var channel = GrpcChannel.ForAddress(address);
            _client = new AssetPairs.AssetPairsClient(channel);
        }

        public async Task<IReadOnlyList<AssetPairModel>> GetAllAsync()
        {
            var response = await _client.GetAllAsync(new Empty());

            return response.AssetPairs
                .Select(asset => new AssetPairModel(asset))
                .ToList();
        }

        public async Task<IReadOnlyList<AssetPairModel>> GetAllByBrokerIds(IEnumerable<string> brokerIds)
        {
            var response = await _client.GetAllByBrokerIdsAsync(new GetAllAssetPairsByBrokerIdsRequest { BrokerIds = { brokerIds } });

            return response.AssetPairs
                .Select(asset => new AssetPairModel(asset))
                .ToList();
        }

        public async Task<IReadOnlyList<AssetPairModel>> GetAllByBrokerId(string brokerId)
        {
            var response = await _client.GetAllByBrokerIdAsync(new GetAllAssetPairsByBrokerIdRequest { BrokerId = brokerId });

            return response.AssetPairs
                .Select(assetPair => new AssetPairModel(assetPair))
                .ToList();
        }

        public async Task<AssetPairModel> GetBySymbolAsync(string brokerId, string symbol)
        {
            var response = await _client.GetBySymbolAsync(new GetAssetPairBySymbolRequest { BrokerId = brokerId, Symbol = symbol });

            return response.AssetPair != null
                ? new AssetPairModel(response.AssetPair)
                : null;
        }

        public async Task<AssetPairModel> AddAsync(AssetPairEditModel model)
        {
            var response = await _client.AddAsync(new AddAssetPairRequest
            {
                BrokerId = model.BrokerId,
                Symbol = model.Symbol,
                Accuracy = model.Accuracy,
                BaseAsset = model.BaseAsset,
                QuotingAsset = model.QuotingAsset,
                MinVolume = model.MinVolume.ToString(CultureInfo.InvariantCulture),
                MaxVolume = model.MaxVolume.ToString(CultureInfo.InvariantCulture),
                MaxOppositeVolume = model.MaxOppositeVolume.ToString(CultureInfo.InvariantCulture),
                MarketOrderPriceThreshold = model.MarketOrderPriceThreshold.ToString(CultureInfo.InvariantCulture),
                IsDisabled = model.IsDisabled
            });

            return new AssetPairModel(response.AssetPair);
        }

        public async Task UpdateAsync(AssetPairEditModel model)
        {
            await _client.UpdateAsync(new UpdateAssetPairRequest
            {
                BrokerId = model.BrokerId,
                Symbol = model.Symbol,
                Accuracy = model.Accuracy,
                BaseAsset = model.BaseAsset,
                QuotingAsset = model.QuotingAsset,
                MinVolume = model.MinVolume.ToString(CultureInfo.InvariantCulture),
                MaxVolume = model.MaxVolume.ToString(CultureInfo.InvariantCulture),
                MaxOppositeVolume = model.MaxOppositeVolume.ToString(CultureInfo.InvariantCulture),
                MarketOrderPriceThreshold = model.MarketOrderPriceThreshold.ToString(CultureInfo.InvariantCulture),
                IsDisabled = model.IsDisabled
            });
        }

        public async Task DeleteAsync(string brokerId, string symbol)
        {
            await _client.DeleteAsync(new DeleteAssetPairRequest { BrokerId = brokerId, Symbol = symbol });
        }
    }
}
