﻿using System;

namespace Models
{
    public class Response : IResponse
    {
        public bool Error
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public Response()
        {

        }

        public Response(Exception ex)
        {
            Error = true;

            Message = ex.Message;
        }
    }
}
