﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TaskEase.Core.Extensions;

public static class JwtBearerExtensions
{
    public static IConfigurationBuilder AddJwtBearer(this IConfigurationBuilder config, WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policyBuilder =>
                {
                    policyBuilder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policyBuilder.RequireAuthenticatedUser().Build();
                });
            })
            .AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

        return builder.Configuration;
    }
}