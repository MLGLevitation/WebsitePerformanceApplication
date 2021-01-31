using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utility
{
    public class ProgressBarHub : Hub
    {
        public static void SendMessage(string msg, int count)
        {
            string message;
            message = msg != "" ? "Completed: " + msg : "";
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressBarHub>();
            hubContext.Clients.All.sendMessage(string.Format(message), count);
        }
    }
}
