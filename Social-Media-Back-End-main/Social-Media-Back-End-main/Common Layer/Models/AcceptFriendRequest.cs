using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class AcceptFriendRequestRequest
    {
        public string FriendRequestID { get; set; }
    }

    public class AcceptFriendRequestResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class CancleFriendRequestResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
