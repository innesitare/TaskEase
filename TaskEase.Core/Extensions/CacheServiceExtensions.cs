﻿using TaskEase.Core.Services.Abstractions;

namespace TaskEase.Core.Extensions;

public static class CacheServiceExtensions
{
    public static async Task RemoveCachesAsync<T>(this ICacheService<T> cacheService,
        CancellationToken cancellationToken, params string[] cacheKeys)
        where T : class
    {
        foreach (string cacheKey in cacheKeys)
        {
            await cacheService.RemoveAsync(cacheKey, cancellationToken);
        }
    }
}