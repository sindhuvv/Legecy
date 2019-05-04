using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Caching
{
	public class TmsCache : ITmsCache
	{
		private readonly IDistributedCache _distributedCache;
		private readonly ITmsConfiguration _tmsConfiguration;
		private readonly int _absoluteExpiration;

		public TmsCache(IDistributedCache distributedCache, ITmsConfiguration tmsConfiguration)
		{
			_distributedCache = distributedCache;
			_tmsConfiguration = tmsConfiguration;
			_absoluteExpiration = _tmsConfiguration.Settings.Caching.AbsoluteExpiration;
		}

		async Task<T> ITmsCache.GetAsync<T>(string key)
		{
			var item = await Task.FromResult<string>(_distributedCache.GetString(key));
			return item != null ? JsonConvert.DeserializeObject<T>(item) : null;
		}

		async Task ITmsCache.RemoveAsync(string key)
		{
			await _distributedCache.RemoveAsync(key);
		}

		async Task ITmsCache.SetAsync(string key, object item)
		{
			var options = new DistributedCacheEntryOptions
			{
				AbsoluteExpiration = DateTime.UtcNow.AddSeconds(_absoluteExpiration)
			};

			var value = JsonConvert.SerializeObject(item);
			await _distributedCache.SetStringAsync(key, value, options);
		}
	}

	public interface ITmsCache
	{
		Task SetAsync(string key, object item);

		Task<T> GetAsync<T>(string key) where T : class;

		Task RemoveAsync(string key);
	}
}
