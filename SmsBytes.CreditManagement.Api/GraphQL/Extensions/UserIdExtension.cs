using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SmsBytes.CreditManagement.Api.GraphQL.Extensions
{
    public static class UserIdExtension
    {
        public static string? GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
