using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class GetPostResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetPost> data { get; set; }
    }

    public class GetPost
    {
        public int PostID { get; set; }
        public string InserttionDate { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string PostType { get; set; }
        public string Value { get; set; }
        public string PublicID { get; set; }
        public int Like {get; set;}
        public string LikeDate { get; set; }
        public bool IsDelete { get; set; }
        public ImageTypeRequest data { get; set; }
    }

}
