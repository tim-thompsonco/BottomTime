using BottomTimeApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BottomTimeApi.Middleware {
	public class ExceptionMiddleware {
		private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context) {
			try {
				await _next(context);
			} catch (InvalidOperationException ex) {
				ApiExceptionMessage apiExceptionMessage = new(ex.Message);

				await UpdateContextWithErrorDetails(context, ex, apiExceptionMessage);
			} catch (Exception ex) {
				await UpdateContextWithErrorDetails(context, ex);
			}
		}

		private async Task UpdateContextWithErrorDetails(HttpContext context, Exception ex, ApiExceptionMessage message = null) {
			_logger.LogError(ex, ex.Message);

			context.Response.ContentType = "application/json";

			context.Response.StatusCode = message == null ? (int)HttpStatusCode.InternalServerError : (int)HttpStatusCode.BadRequest;
			string json = message == null ? JsonSerializer.Serialize(ex, _jsonOptions) : JsonSerializer.Serialize(message, _jsonOptions);

			await context.Response.WriteAsync(json);
		}
	}
}
