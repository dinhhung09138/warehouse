using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.API.Hubs;
using Core.Common.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Common.API
{
    /// <summary>
    /// Base controller.
    /// </summary>
    [EnableCors("WarehouseApplicationPolicy")]
    [ApiController]
    public class BaseController : Controller
    {
        /// <summary>
        /// Connection mapping.
        /// </summary>
        private readonly ConnectionMapping _connectionMapping;

        /// <summary>
        /// Logger service.
        /// </summary>
        private readonly ILoggerService _logger;

        /// <summary>
        /// Hub context.
        /// </summary>
        private readonly IHubContext<NotificationServiceHub> _hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="provider">Service provider.</param>
        public BaseController(IServiceProvider provider)
        {
            this._logger = provider.GetService<ILoggerService>();
            this._hubContext = provider.GetService<IHubContext<NotificationServiceHub>>();
            this._connectionMapping = provider.GetService<ConnectionMapping>();
        }

        /// <summary>
        /// Gets hub context.
        /// </summary>
        protected IHubContext<NotificationServiceHub> HubContext => this._hubContext;

        /// <summary>
        /// Get current user id.
        /// </summary>
        /// <returns>user id.</returns>
        protected string CurrentUserId()
        {
            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userId;
            }

            return string.Empty;
        }

        /// <summary>
        /// Send notify to all user except the current user.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="item">Object data.</param>
        /// <returns>Void.</returns>
        protected async Task NotifyExceptCurrentUser(string eventName, object item)
        {
            try
            {
                var connectionIds = this._connectionMapping.GetConnections(this.CurrentUserId());
                await this._hubContext.Clients.AllExcept(connectionIds).SendAsync(eventName, item).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Send notify to all user.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="item">Object data.</param>
        /// <returns>Void.</returns>
        protected async Task NotifyToAll(string eventName, object item)
        {
            try
            {
                await this._hubContext.Clients.All.SendAsync(eventName, item).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Send notify to single user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="eventName">Event name.</param>
        /// <param name="item">Object data.</param>
        /// <returns>Void.</returns>
        protected async Task NotifyToSingleUser(string userId, string eventName, object item)
        {
            try
            {
                var connectionIds = this._connectionMapping.GetConnections(userId);
                await this._hubContext.Clients.Clients(connectionIds).SendAsync(eventName, item).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
