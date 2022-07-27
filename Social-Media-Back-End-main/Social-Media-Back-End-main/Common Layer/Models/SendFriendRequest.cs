using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class SendFriendRequestRequest
    {
        [Required]
        public int FromUserID { get; set; }

        [Required]
        public int ToUserID { get; set; }
    }

    public class SendFriendRequestResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
