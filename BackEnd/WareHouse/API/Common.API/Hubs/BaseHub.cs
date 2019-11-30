using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Common.API.Hubs
{
    /// <summary>
    /// Base hub.
    /// </summary>
    public class BaseHub : Hub
    {
        /// <summary>
        /// Get current user id.
        /// </summary>
        /// <returns>string value.</returns>
        protected string CurrentUserId()
        {
            if (this.Context.User != null && this.Context.User.Identity.IsAuthenticated)
            {
                var userId = this.Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userId;
            }

            return string.Empty;
        }
    }
}
