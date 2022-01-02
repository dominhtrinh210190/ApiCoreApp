using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ApiResponse
    {
        public static readonly ApiResponse GeneralError = new ApiResponse { Result = new ResponseResult { Code = "1", Message = "Something went wrong." } };

        public ApiResponse() { }

        public ApiResponse(dynamic success) { this.Data = success; }

        public ApiResponse(int errorCode, string message)
        {
            Result = new ResponseResult { Code = errorCode.ToString(), Message = message };
        }

        [JsonProperty("data")]
        public dynamic Data { get; set; }
        
        [JsonProperty("result")]
        public ResponseResult Result { get; set; } = new ResponseResult { Code = "000000", Message = "successful", Description = string.Empty };

    } 
    public class ResponseResult
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; } 
    }
}
