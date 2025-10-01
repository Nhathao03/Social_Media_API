using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Social_Media.Helpers
{
    public static class ApiResponseHelper
    {
        public static IActionResult Success<T>(T? data, string? message = null) =>
            new OkObjectResult(new ApiResponse<T>((int)HttpStatusCode.OK, message, data));
        public static IActionResult Created<T>(T? data, string? message = null) =>
            new ObjectResult(new ApiResponse<T>((int)HttpStatusCode.Created, message, data)) { StatusCode = 201 };
        public static IActionResult NotFound(string? message = null) =>
            new NotFoundObjectResult(new ApiResponse<object>((int)HttpStatusCode.NotFound, message));
        public static IActionResult BadRequest(string? message = null) =>
            new BadRequestObjectResult(new ApiResponse<object>((int)HttpStatusCode.BadRequest, message));
        public static IActionResult Unauthorized(string? message = null) =>
            new UnauthorizedObjectResult(new ApiResponse<object>((int)HttpStatusCode.Unauthorized, message));
        public static IActionResult Forbidden(string? message = null) =>
            new ObjectResult(new ApiResponse<string>((int)HttpStatusCode.Forbidden, message)) { StatusCode = 403 };
        public static IActionResult InternalServerError(string? message = null) =>
            new ObjectResult(new ApiResponse<string>((int)HttpStatusCode.InternalServerError, message)) { StatusCode = 500 };
        public static IActionResult NoContent(string message) =>
            new ObjectResult(new ApiResponse<string>((int)HttpStatusCode.NoContent, message));
    }
}
