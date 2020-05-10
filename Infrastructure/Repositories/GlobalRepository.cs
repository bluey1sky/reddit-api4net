﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GamingCommunityApi.Infrastructure.Converters;
using GamingCommunityApi.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GamingCommunityApi.Core.Models;
using GamingCommunityApi.Core.Extensions;
using GamingCommunityApi.Core.Enums;
using GamingCommunityApi.Core.Exceptions;
using GamingCommunityApi.Core.ValueObjects;
using GamingCommunityApi.Core.Interfaces.IRepositories;

namespace GamingCommunityApi.Infrastructure.Repositories
{
    public class GlobalRepository : IGlobalRepository
    {
        private readonly ILogger<GlobalRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly GamingCommunityApiContext _gamingCommunityApiContext;
        private readonly DbSet<GlobalEntity> _globalEntities;
        private readonly GlobalConverter _globalConverter;

        public GlobalRepository(ILogger<GlobalRepository> logger, 
            IConfiguration configuration, GamingCommunityApiContext gamingCommunityApiContext,
            GlobalConverter globalConverter)
        {
            _logger = logger;
            _configuration = configuration;
            _gamingCommunityApiContext = gamingCommunityApiContext;
            _globalEntities = gamingCommunityApiContext.GlobalEntities;
            _globalConverter = globalConverter;
        }

        public async Task<List<Global>> ListGlobalsAsync()
        {
            var globalEntites = await _globalEntities
                .AsNoTracking()
                .Include(
                )
                .ToListAsync();

            return globalEntites.Select(e => _globalConverter.ConvertToModel(e)).ToList();
        }

        public async Task<Global> GetGlobalByIdAsync(GlobalId globalId)
        {
            var globalEntity = await _globalEntities
                .AsNoTracking()
                .Where(e => e.Id == globalId.To<int>())
                .Include(
                )
                .SingleOrDefaultAsync();

            return _globalConverter.ConvertToModel(globalEntity);
        }

        public async Task<Global> CreateGlobalAsync(GlobalId globalId, GlobalValues globalValues)
        {
            var globalEntity = new GlobalEntity(globalId.To<int>(), globalValues);
            _globalEntities.Add(globalEntity);
            await _gamingCommunityApiContext.SaveChangesAsync();
            _gamingCommunityApiContext.DetachAllEntries();
            return _globalConverter.ConvertToModel(globalEntity);
        }

        public async Task<Global> UpdateGlobalAsync(Global global)
        {
            var globalEntity = _globalConverter.ConvertToEntity(global);
            _globalEntities.Update(globalEntity);
            try
            {
                await _gamingCommunityApiContext.SaveChangesAsync();
                _gamingCommunityApiContext.DetachAllEntries();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var serverMessage = $"Can't update the globalEntity DbUpdateConcurrencyException. {globalEntity.ToJson()}";
                throw new ApiException(ErrorName.INTERNAL_SERVER, serverMessage, systemException: ex);
            }
            return _globalConverter.ConvertToModel(globalEntity);
        }

        public async Task DeleteGlobalAsync(GlobalId globalId)
        {
            var globalEntity = await _globalEntities
                .Where(e => e.Id == globalId.To<int>())
                .SingleOrDefaultAsync();

            _globalEntities.Remove(globalEntity);
            await _gamingCommunityApiContext.SaveChangesAsync();
            _gamingCommunityApiContext.DetachAllEntries();
        }

        public async Task<bool> DoesGlobalIdExistAsync(GlobalId globalId)
        {
            return await _globalEntities
                .AsNoTracking()
                .Where(e => e.Id == globalId.To<int>())
                .AnyAsync();
        }
    }

    public static class GlobalRepositoryExtensions
    {
        public static IQueryable<GlobalEntity> Include(
                    [NotNull] this IQueryable<GlobalEntity> globalEntitiesQuery)
        {
            return globalEntitiesQuery;
        }
    }
}
