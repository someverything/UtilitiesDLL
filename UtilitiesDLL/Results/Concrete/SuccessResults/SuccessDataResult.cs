﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UtilitiesDLL.Results.Concrete.SuccessResults
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message, HttpStatusCode statusCode) : base(data, true, message, statusCode)
        {
        }
        public SuccessDataResult(T data, HttpStatusCode statusCode) : base(data, true, statusCode)
        {
        }
        public SuccessDataResult(string message, HttpStatusCode statusCode) : base(default, true, message, statusCode)
        {
        }
        public SuccessDataResult(HttpStatusCode statusCode) : base(default, true, statusCode)
        {
        }
        [JsonConstructor]
        public SuccessDataResult(T data, bool success, string message, HttpStatusCode statusCode) : base(statusCode: statusCode, message: message, data: data, success: success)
        {
        }
    }
}
