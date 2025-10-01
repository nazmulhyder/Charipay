using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Common.Models
{
    public class ApiResponse <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> FailedResponse(string message, List<string> errors = null) {
            return new ApiResponse<T>
            {
                Errors = errors,
                Success = false,
                Message = message
            };
        
        }
    }

   
}
