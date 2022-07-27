using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class SearchPeopleRequest
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string SearchKey { get; set; }
    }
    public class SearchPeopleResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<SearchPeople> data { get; set; }
    }

    public class SearchPeople
    {
        public int toUserID { get; set; }
        public string toUserName { get; set; }
    }
}
