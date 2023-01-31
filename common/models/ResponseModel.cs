using System;
using System.Collections.Generic;

namespace fileuploader.common.models
{
    public class ResponseModel
    {
        public status status { get; set; }
        public string? Message { get; set; }

    }

    public enum status
    {
        nostatus,
        success,
        failure,

        norecords
    }



}