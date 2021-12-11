using BottomTimeApi;
using BottomTimeApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BottomTimeApiTests.Controllers {
	public class DiveControllerIntegrationTests {
		[Fact]
		public async Task GetDivesIntegrationTestAsync() {
			WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
			});
			HttpClient client = application.CreateClient();

			HttpResponseMessage response = await client.GetAsync("api/dives");
			string responseContent = await response.Content.ReadAsStringAsync();
			IEnumerable<Dive> deserializedContent = JsonConvert.DeserializeObject<IEnumerable<Dive>>(responseContent);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(deserializedContent);
			Assert.NotEmpty(deserializedContent);
		}
	}
}