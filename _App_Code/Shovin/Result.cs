using System;
using System.Runtime.Serialization;

namespace Shovin
{
    [DataContract(Name = "Result{0}")]
    public class Result<T>
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public String Message { get; set; }
        [DataMember]
        public T Data { get; set; }

        public Result(T data)
        {
            Data = data;
            ErrorCode = 0;
            Message = "";
        }

        public Result(int errorCode, String message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public Result(int errorCode, String message, T data)
        {
            Data = data;
            ErrorCode = errorCode;
            Message = message;
        }

        public Result()
        {
            ErrorCode = -1;
            Message = "UNKNOWN ERROR";
        }
    }
}