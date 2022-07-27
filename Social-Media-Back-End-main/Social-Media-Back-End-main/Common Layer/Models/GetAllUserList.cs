using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class GetAllUserListResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetAllUserList> data { get; set; }
    }

    public class GetAllUserList
    {
        public int ToUserID { get; set; }
        public string ToUserName { get; set; }
    }
}
