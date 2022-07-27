using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class GetSendFriendRequestListResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetSendFriendRequestList> data { get; set; }
    }

    public class GetSendFriendRequestList
    {
        public int FriendRequestID { get; set; }
        public string FullName { get; set; }
    }
}
