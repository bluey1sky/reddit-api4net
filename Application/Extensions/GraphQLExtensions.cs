﻿using FireplaceApi.Application.Controllers;
using FireplaceApi.Application.Middlewares;
using FireplaceApi.Application.Resolvers;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FireplaceApi.Application.Extensions
{
    public static class GraphQLExtensions
    {
        public static IRequestExecutorBuilder UseGraphQLPipeline(
            this IRequestExecutorBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .UseDefaultPipeline()
                .UseSampleGraphQLRequestMiddleware();
        }

        public static IRequestExecutorBuilder AddGraphQLResolvers(
            this IRequestExecutorBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .AddQueryType<GraphQLController>()
                .AddTypeExtension<QueryCommunityResolvers>();
        }
    }
}