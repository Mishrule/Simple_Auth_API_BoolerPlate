using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Application.Reponses
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public List<string> Errors { get; set; }
    }

    public class BaseCountResponse<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> SuccessMessages { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string MessageCount { get; set; }
    }

    public class BaseResponseList<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Results { get; set; }
        public List<string> Errors { get; set; }
    }

    public class PaginatedModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; }
    }

    public class PaginatedModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
    public class BaseEntityResponse
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public string EntityStatus { get; set; }
    }
}
