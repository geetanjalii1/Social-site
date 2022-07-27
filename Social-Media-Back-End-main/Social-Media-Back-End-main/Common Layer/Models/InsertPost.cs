using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Models
{
    public class InsertPostRequest
    {
        public int UserID { get; set; }
        public FileType FileType { get; set; }
        public IFormFile File1 { get; set; }
        public string File2 { get; set; }
        public string Caption { get; set; }

    }

    public class InsertPostResponse
    {
        public  bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class DeletePostResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public enum FileType
    {
        Image = 1,
        Text = 2
    }

    public class ImageTypeRequest
    {
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
    }
}
