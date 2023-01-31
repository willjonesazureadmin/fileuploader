using System;
using System.Collections.Generic;

namespace fileuploader.common.models
{
    public class CustomerModel 
    {
        public Guid Id { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }

        public DateTime BirthDate { get; set; }

    }
}