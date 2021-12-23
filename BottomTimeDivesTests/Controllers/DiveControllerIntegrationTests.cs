using BottomTimeDives;
using BottomTimeDives.Errors;
using BottomTimeDives.Models;
using BottomTimeDivesTests.Data.MockData;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BottomTimeDivesTests.Controllers {
	public class DiveControllerIntegrationTests {
		[Fact]
		public async Task GetDivesOkIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int pageNumber = 1;
			const int divesPerPage = 5;

			using HttpResponseMessage response = await client.GetAsync($"dives?pageNumber={pageNumber}&divesPerPage={divesPerPage}");
			string responseContent = await response.Content.ReadAsStringAsync();
			List<Dive> deserializedContent = JsonConvert.DeserializeObject<List<Dive>>(responseContent);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(deserializedContent);
			Assert.Equal(divesPerPage, deserializedContent.Count);
		}

		[Fact]
		public async Task GetDivesBadRequestPageNumberIsZeroIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int pageNumber = 0;
			const int divesPerPage = 5;
			const string expectedResponseMessage = "The page number cannot be less than 1.";

			using HttpResponseMessage response = await client.GetAsync($"dives?pageNumber={pageNumber}&divesPerPage={divesPerPage}");
			string responseContent = await response.Content.ReadAsStringAsync();
			ApiExceptionMessage deserializedResponseContent = JsonConvert.DeserializeObject<ApiExceptionMessage>(responseContent);

			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			Assert.Equal(expectedResponseMessage, deserializedResponseContent.Message);
		}

		[Fact]
		public async Task GetDivesBadRequestDivesPerPageBelowMinAmountIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int pageNumber = 1;
			const int divesPerPage = 0;
			const string expectedResponseMessage = "The dives per page cannot be less than 1.";

			using HttpResponseMessage response = await client.GetAsync($"dives?pageNumber={pageNumber}&divesPerPage={divesPerPage}");
			string responseContent = await response.Content.ReadAsStringAsync();
			ApiExceptionMessage deserializedResponseContent = JsonConvert.DeserializeObject<ApiExceptionMessage>(responseContent);

			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			Assert.Equal(expectedResponseMessage, deserializedResponseContent.Message);
		}

		[Fact]
		public async Task GetDivesOkDivesPerPageAtMaxAmountIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int pageNumber = 1;
			const int divesPerPage = 100;

			using HttpResponseMessage response = await client.GetAsync($"dives?pageNumber={pageNumber}&divesPerPage={divesPerPage}");
			string responseContent = await response.Content.ReadAsStringAsync();
			List<Dive> deserializedContent = JsonConvert.DeserializeObject<List<Dive>>(responseContent);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(deserializedContent);
			Assert.Equal(divesPerPage, deserializedContent.Count);
		}

		[Fact]
		public async Task GetDivesBadRequestDivesPerPageAboveMaxAmountIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			const int pageNumber = 1;
			const int divesPerPage = 101;
			const string expectedResponseMessage = "The dives per page cannot be greater than 100.";

			using HttpResponseMessage response = await client.GetAsync($"dives?pageNumber={pageNumber}&divesPerPage={divesPerPage}");
			string responseContent = await response.Content.ReadAsStringAsync();
			ApiExceptionMessage deserializedResponseContent = JsonConvert.DeserializeObject<ApiExceptionMessage>(responseContent);

			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			Assert.Equal(expectedResponseMessage, deserializedResponseContent.Message);
		}

		[Fact]
		public async Task PostDiveCreatedIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");

			using HttpResponseMessage response = await client.PostAsync("dives", diveContent);
			string responseContent = await response.Content.ReadAsStringAsync();
			Dive deserializedContent = JsonConvert.DeserializeObject<Dive>(responseContent);

			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
			Assert.Equal(divePost.Number, deserializedContent.Number);
			Assert.Equal(divePost.BottomTime, deserializedContent.BottomTime);
			Assert.Equal(divePost.AvgDepth, deserializedContent.AvgDepth);

			// Cleanup dive that was created to avoid bloating the size of ACC DB
			await client.DeleteAsync($"dives/{deserializedContent.Id}");
		}

		[Fact]
		public async Task PostDiveBadRequestIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost {
				Location = string.Empty // This is a required field for DivePost
			};
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");

			using HttpResponseMessage response = await client.PostAsync("dives", diveContent);

			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task GetDiveByIdOkIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");
			using HttpResponseMessage postResponse = await client.PostAsync("dives", diveContent);
			string postResponseContent = await postResponse.Content.ReadAsStringAsync();
			Dive deserializedPostResponseContent = JsonConvert.DeserializeObject<Dive>(postResponseContent);

			using HttpResponseMessage getResponse = await client.GetAsync($"dives/{deserializedPostResponseContent.Id}");
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

			using HttpResponseMessage response = await client.GetAsync($"dives/{nonexistentId}");

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
			using HttpResponseMessage postResponse = await client.PostAsync("dives", diveContent);
			string postResponseContent = await postResponse.Content.ReadAsStringAsync();
			Dive deserializedPostResponseContent = JsonConvert.DeserializeObject<Dive>(postResponseContent);
			deserializedPostResponseContent.Location += " updated";
			using StringContent updateDiveContent = new(JsonConvert.SerializeObject(deserializedPostResponseContent), Encoding.UTF8, "application/json");

			using HttpResponseMessage updateResponse = await client.PutAsync($"dives", updateDiveContent);
			using HttpResponseMessage getResponse = await client.GetAsync($"dives/{deserializedPostResponseContent.Id}");
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
			const string expectedResponseMessage = "Dive number is too high. The maximum dive number is 10,000.";

			using HttpResponseMessage putResponse = await client.PutAsync("dives", diveContent);
			string putResponseContent = await putResponse.Content.ReadAsStringAsync();
			ApiExceptionMessage deserializedPutResponseContent = JsonConvert.DeserializeObject<ApiExceptionMessage>(putResponseContent);

			Assert.Equal(HttpStatusCode.BadRequest, putResponse.StatusCode);
			Assert.Equal(expectedResponseMessage, deserializedPutResponseContent.Message);
		}

		[Fact]
		public async Task DeleteDiveByIdNoContentIntegrationTestAsync() {
			using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
				});
			using HttpClient client = application.CreateClient();
			DivePost divePost = new MockDivePost();
			using StringContent diveContent = new(JsonConvert.SerializeObject(divePost), Encoding.UTF8, "application/json");
			using HttpResponseMessage postResponse = await client.PostAsync("dives", diveContent);
			string postResponseContent = await postResponse.Content.ReadAsStringAsync();
			Dive deserializedPostResponseContent = JsonConvert.DeserializeObject<Dive>(postResponseContent);

			using HttpResponseMessage deleteResponse = await client.DeleteAsync($"dives/{deserializedPostResponseContent.Id}");
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

			using HttpResponseMessage response = await client.DeleteAsync($"dives/{nonexistentId}");

			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}