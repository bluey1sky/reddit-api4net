﻿using GamingCommunityApi.Core.Operators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace GamingCommunityApi.Api.Tools.Swagger
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;
        readonly IWebHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        /// <param name="env"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IWebHostEnvironment env)
        {
            _provider = provider;
            _env = env;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _env));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, IWebHostEnvironment env)
        {
            var description_html = "<div>";
            
            description_html += @"
                <div style=""display: block; margin: 0px 0px 10px 0px;"">
                    <h5><strong>Gamers</strong>! Gaming Community is the place where you can communicate with each other about your beloved games.</h5>
                </div>";

            description_html += $@"
                <div style=""display: block; margin: 0px 0px 60px 0px;"">
                    <div style=""margin-bottom:10px"">Sample urls:</div>
                    <div style=""margin-left: 30px;""><a href=""{GlobalOperator.GlobalValues.Api.BaseUrlPath}users/{{your-id}}\"">{GlobalOperator.GlobalValues.Api.BaseUrlPath}users/{{your-id}}</a></div>
                    <div style=""margin-left: 30px;""><a href=""{GlobalOperator.GlobalValues.Api.BaseUrlPath}v0.1/users/{{your-id}}\"">{GlobalOperator.GlobalValues.Api.BaseUrlPath}v0.1/users/{{your-id}}</a></div>
                </div>";

            description_html += $@"
                <div style=""display: block; margin: 0px 0px 25px 0px;"">
                    <a class=""btn"" href=""{GlobalOperator.GlobalValues.Api.BaseUrlPath}v0.1/users/open-google-log-in-page"" 
                                style=""display: inline; text-transform: none; padding: 15px 8px 15px 8px; font-size: 10px; text-decoration: none"" target=""_blank"">
                        <h2 style=""display: inline;"">
                            <img width=""20px"" style=""margin: 0px 5px -5px 0px"" alt=""Google sign-in""
                                    src=""https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Google_%22G%22_Logo.svg/512px-Google_%22G%22_Logo.svg.png"" />
                        Login with Google
                        </h2>
                    </a>
                </div>";

            description_html += "</div>";

            var info = new OpenApiInfo()
            {
                Title = "Gaming Community Api",
                Version = description.ApiVersion.ToString(),
                Description = description_html,
                Contact = new OpenApiContact
                {
                    Name = "Soroush Kavousi",
                    Email = "soroushkavousi.me@gmail.com",
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://example.com/license")
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }
            
            return info;
        }
    }
}
