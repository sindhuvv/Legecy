using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.Web.Extensions;

namespace Tms.Web.Services.Api
{

	public class TmsClient : ITmsClient
	{
		private readonly IHttpClientFactory _client;

		public TmsClient(IHttpClientFactory client)
		{
			_client = client;
		}

		[Authorize(Policy = "TrainingViewers")]
		async Task<IReadOnlyList<Training>> ITmsClient.ListTrainings()
		{
			using (var httpclient = _client.CreateClient("tmsClient"))
			using (var response = await httpclient.GetAsync("training/list"))
			{
				if (response.StatusCode == HttpStatusCode.OK)
					return await response.Content.ReadAsJsonAsync<IReadOnlyList<Training>>();
				return null;
			}
		}
	}
	public interface ITmsClient
	{
		Task<IReadOnlyList<Training>> ListTrainings();
	}
}
