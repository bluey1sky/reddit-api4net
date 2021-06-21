﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using GamingCommunityApi.Core.Enums;
using GamingCommunityApi.Core.Exceptions;
using GamingCommunityApi.Core.Extensions;
using GamingCommunityApi.Core.Models;
using GamingCommunityApi.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Net;
using GamingCommunityApi.Core.ValueObjects;
using GamingCommunityApi.Core.Interfaces.IRepositories;
using GamingCommunityApi.Core.Interfaces.IGateways;
using Microsoft.AspNetCore.WebUtilities;

namespace GamingCommunityApi.Core.Operators
{
    public class GlobalOperator
    {
        private readonly ILogger<GlobalOperator> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGlobalRepository _globalRepository;
        private readonly IGoogleGateway _googleGateway;
        public static GlobalValues GlobalValues { get; set; }

        public GlobalOperator(ILogger<GlobalOperator> logger, IConfiguration configuration,
            IServiceProvider serviceProvider, IGlobalRepository globalRepository,
            IGoogleGateway googleGateway)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _globalRepository = globalRepository;
            _googleGateway = googleGateway;
        }

        public async Task<List<Global>> ListGlobalsAsync()
        {
            var globals = await _globalRepository.ListGlobalsAsync();
            return globals;
        }

        public async Task<Global> GetGlobalByIdAsync(GlobalId id)
        {
            var global = await _globalRepository.GetGlobalByIdAsync(id);

            if (global == null)
                return global;

            return global;
        }

        public async Task<Global> CreateGlobalAsync(GlobalId id, GlobalValues globalValues)
        {
            var global = await _globalRepository.CreateGlobalAsync(id, globalValues);
            return global;
        }

        public async Task DeleteGlobalAsync(GlobalId id)
        {
            await _globalRepository.DeleteGlobalAsync(id);
        }

        public async Task<bool> DoesGlobalIdExistAsync(GlobalId id)
        {
            var globalIdExists = await _globalRepository.DoesGlobalIdExistAsync(id);
            return globalIdExists;
        }
    }
}