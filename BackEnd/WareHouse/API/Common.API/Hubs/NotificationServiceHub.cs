using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Common.API.Hubs
{
    /// <summary>
    /// Notification service.
    /// </summary>
    public class NotificationServiceHub : BaseHub
    {
        private readonly ConnectionMapping _connectionMapping;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="connectionMapping">Connection mapping.</param>
        public NotificationServiceHub(ConnectionMapping connectionMapping)
        {
            _connectionMapping = connectionMapping;
        }

        /// <summary>
        /// Connected.
        /// </summary>
        /// <returns>Void.</returns>
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                _connectionMapping.Add(CurrentUserId(), Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// disconected method.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <returns>Void.</returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                _connectionMapping.Remove(CurrentUserId(), Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
