using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class RejectFriendRequestRequest
    {
        public int FriendRequestID { get; set; }
    }

    public class RejectFriendRequestResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
