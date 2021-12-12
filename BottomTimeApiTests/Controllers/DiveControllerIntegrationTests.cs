using BottomTimeApi;
using BottomTimeApi.Models;
using BottomTimeApiTests.Data.MockData;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BottomTimeApiTests.Controllers {
	public class DiveControllerIntegrationTests {
		[Fact]
		public async Task GetDivesOkIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			using HttpResponseMessage response = await client.GetAsync("api/dives");
			string responseContent = await response.Content.ReadAsStringAsync();
			IEnumerable<Dive> deserializedContent = JsonConvert.DeserializeObject<IEnumerable<Dive>>(responseContent);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(deserializedContent);
			Assert.NotEmpty(deserializedContent);
		}

		[Fact]
		public async Task PostDiveCreatedIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");

			using HttpResponseMessage response = await client.PostAsync("api/dives", diveContent);
			string responseContent = await response.Content.ReadAsStringAsync();
			Dive deserializedContent = JsonConvert.DeserializeObject<Dive>(responseContent);

			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
			Assert.Equal(divePost.Number, deserializedContent.Number);
			Assert.Equal(divePost.BottomTime, deserializedContent.BottomTime);
			Assert.Equal(divePost.AvgDepth, deserializedContent.AvgDepth);
		}

		[Fact]
		public async Task GetDiveByIdOkIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");
			using HttpResponseMessage postResponse = await client.PostAsync("api/dives", diveContent);
			string postResponseContent = await postResponse.Content.ReadAsStringAsync();
			Dive deserializedPostResponseContent = JsonConvert.DeserializeObject<Dive>(postResponseContent);

			using HttpResponseMessage getResponse = await client.GetAsync($"api/dives/{deserializedPostResponseContent.Id}");
			string getResponseContent = await getResponse.Content.ReadAsStringAsync();
			Dive deserializedGetResponseContent = JsonConvert.DeserializeObject<Dive>(getResponseContent);

			Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
			Assert.Equal(deserializedPostResponseContent.Number, deserializedGetResponseContent.Number);
			Assert.Equal(deserializedPostResponseContent.BottomTime, deserializedGetResponseContent.BottomTime);
			Assert.Equal(deserializedPostResponseContent.AvgDepth, deserializedGetResponseContent.AvgDepth);
		}

		[Fact]
		public async Task GetDiveByIdNotFoundIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int nonexistentId = 1; // The lowest ID number in ACC DB is 7, so this ID should never exist

			using HttpResponseMessage response = await client.GetAsync($"api/dives/{nonexistentId}");

			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task UpdateDiveNoContentIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");
			using HttpResponseMessage postResponse = await client.PostAsync("api/dives", diveContent);
			string postResponseContent = await postResponse.Content.ReadAsStringAsync();
			Dive deserializedPostResponseContent = JsonConvert.DeserializeObject<Dive>(postResponseContent);
			deserializedPostResponseContent.Location += " updated";
			using StringContent updateDiveContent = new(JsonConvert.SerializeObject(deserializedPostResponseContent), Encoding.UTF8, "application/json");

			using HttpResponseMessage updateResponse = await client.PutAsync($"api/dives", updateDiveContent);
			using HttpResponseMessage getResponse = await client.GetAsync($"api/dives/{deserializedPostResponseContent.Id}");
			string getResponseContent = await getResponse.Content.ReadAsStringAsync();
			Dive deserializedGetResponseContent = JsonConvert.DeserializeObject<Dive>(getResponseContent);

			Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
			Assert.Equal(deserializedPostResponseContent.Location, deserializedGetResponseContent.Location);
		}

		[Fact]
		public async Task UpdateDiveBadRequestIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePut = new MockDivePost {
				Number = 10001 // This should throw a validation exception since the max number allowed is 10000
			};
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePut), Encoding.UTF8, "application/json");
			
			using HttpResponseMessage putResponse = await client.PutAsync("api/dives", diveContent);

			Assert.Equal(HttpStatusCode.BadRequest, putResponse.StatusCode);
		}

		[Fact]
		public async Task DeleteDiveByIdNoContentIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");
			using HttpResponseMessage postResponse = await client.PostAsync("api/dives", diveContent);
			string postResponseContent = await postResponse.Content.ReadAsStringAsync();
			Dive deserializedPostResponseContent = JsonConvert.DeserializeObject<Dive>(postResponseContent);

			using HttpResponseMessage deleteResponse = await client.DeleteAsync($"api/dives/{deserializedPostResponseContent.Id}");
			string deleteResponseContent = await deleteResponse.Content.ReadAsStringAsync();
			Dive deserializedDeleteResponseContent = JsonConvert.DeserializeObject<Dive>(deleteResponseContent);

			Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
		}

		[Fact]
		public async Task DeleteDiveByIdNotFoundIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int nonexistentId = 1; // The lowest ID number in ACC DB is 7, so this ID should never exist

			using HttpResponseMessage response = await client.DeleteAsync($"api/dives/{nonexistentId}");

			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}