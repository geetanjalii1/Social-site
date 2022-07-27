using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class GetAllFriendRequestListResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetAllFriendRequestList> data { get; set; }
    }
    public class GetAllFriendRequestList
    {
        public int FriendRequestID { get; set; }
        public string FullName { get; set; }
    }
}
