﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Assets.Domain.Entities;
using Assets.Domain.Repositories;
using Assets.Repositories.Context;
using Assets.Repositories.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assets.Repositories
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public AssetsRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Asset>> GetAllAsync(string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<AssetEntity> query = context.Assets;

                query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

                var entities = await query.ToListAsync();

                return _mapper.Map<List<Asset>>(entities);
            }
        }

        public async Task<IReadOnlyList<Asset>> GetAllAsync(string brokerId, string id, string name, bool? isDisabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, string cursor = null, int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<AssetEntity> query = context.Assets;

                query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

                if (!string.IsNullOrEmpty(id))
                    query = query.Where(x => EF.Functions.ILike(x.Id, $"%{id}%"));

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(x => EF.Functions.ILike(x.Name, $"%{name}%"));

                if (isDisabled.HasValue)
                    query = query.Where(x => x.IsDisabled == isDisabled);

                if (sortOrder == ListSortDirection.Ascending)
                {
                    if (cursor != null)
                        query = query.Where(x => x.Id.CompareTo(cursor) >= 0);

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (cursor != null)
                        query = query.Where(x => x.Id.CompareTo(cursor) < 0);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                var entities = await query.ToListAsync();

                return _mapper.Map<List<Asset>>(entities);
            }
        }

        public async Task<Asset> GetByIdAsync(string brokerId, string id)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(brokerId, id, context);

                return _mapper.Map<Asset>(existed);
            }
        }

        public async Task<Asset> InsertAsync(Asset asset)
        {
            asset.Created = DateTime.UtcNow;

            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(asset.BrokerId, asset.Id, context);

                if (existed != null)
                    throw new InvalidOperationException($"An asset with the same identifier '{asset.Id}' already exists.");

                var entity = _mapper.Map<AssetEntity>(asset);

                context.Assets.Add(entity);

                await context.SaveChangesAsync();

                return _mapper.Map<Asset>(entity);
            }
        }

        public async Task<Asset> UpdateAsync(Asset asset)
        {
            asset.Modified = DateTime.UtcNow;

            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(asset.BrokerId, asset.Id, context);

                if (existed == null)
                    throw new InvalidOperationException($"An asset with the identifier '{asset.Id}' not exists.");

                _mapper.Map(asset, existed);

                await context.SaveChangesAsync();

                return _mapper.Map<Asset>(existed);
            }
        }

        public async Task DeleteAsync(string brokerId, string id)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(brokerId, id, context);

                if (existed == null)
                    throw new InvalidOperationException($"An asset with the identifier '{id}' not exists.");

                context.Entry(existed).State = EntityState.Deleted;

                await context.SaveChangesAsync();
            }
        }

        private async Task<AssetEntity> GetAsync(string brokerId, string id, DataContext context)
        {
            IQueryable<AssetEntity> query = context.Assets;

            var existed = await query
                .Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper())
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
