﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Results.Abstract;

namespace UtilitiesDLL.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }

        public HttpStatusCode StatusCode { get; }
        public Result(bool success, string message, HttpStatusCode statusCode) : this(success, statusCode)
        {
            Message = message;
        }
        public Result(bool success, HttpStatusCode statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }
    }
}
