﻿using FireplaceApi.Core.Models;
using FireplaceApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;

namespace FireplaceApi.Infrastructure.Converters
{
    public class PostConverter
    {
        private readonly ILogger<PostConverter> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserConverter _authorConverter;
        private readonly CommunityConverter _communityConverter;

        public PostConverter(ILogger<PostConverter> logger,
            IServiceProvider serviceProvider, UserConverter authorConverter,
            CommunityConverter communityConverter)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _authorConverter = authorConverter;
            _communityConverter = communityConverter;
        }

        // Entity

        public PostEntity ConvertToEntity(Post post)
        {
            if (post == null)
                return null;

            UserEntity authorEntity = null;
            if (post.Author != null)
                authorEntity = _authorConverter
                    .ConvertToEntity(post.Author.PureCopy());

            CommunityEntity communityEntity = null;
            if (post.Community != null)
                communityEntity = _communityConverter
                    .ConvertToEntity(post.Community.PureCopy());

            var postEntity = new PostEntity(post.AuthorId,
                post.AuthorUsername, post.CommunityId,
                post.CommunityName, post.Content,
                post.CreationDate, post.ModifiedDate, 
                post.Id, post.Vote, authorEntity, communityEntity);

            return postEntity;
        }

        public Post ConvertToModel(PostEntity postEntity)
        {
            if (postEntity == null)
                return null;

            User author = null;
            if (postEntity.AuthorEntity != null)
                author = _authorConverter.ConvertToModel(postEntity.AuthorEntity.PureCopy());

            Community community = null;
            if (postEntity.CommunityEntity != null)
                community = _communityConverter
                    .ConvertToModel(postEntity.CommunityEntity.PureCopy());

            var post = new Post(postEntity.Id.Value,
                postEntity.AuthorEntityId, postEntity.AuthorEntityUsername,
                postEntity.CommunityEntityId, postEntity.CommunityEntityName,
                postEntity.Vote, postEntity.Content, postEntity.CreationDate, 
                postEntity.ModifiedDate, author, community);

            return post;
        }
    }
}