using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class GetAllFriendListResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetAllFriendList> data { get; set; }
    }

    public class GetAllFriendList
    {
        public int FriendRequestID { get; set; }
        public string FromFullName { get; set; }
        public string ToFullName { get; set; }
    }
}
