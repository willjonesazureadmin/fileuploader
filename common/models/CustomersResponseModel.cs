using System;
using System.Collections.Generic;

namespace fileuploader.common.models
{
    public class CustomersResponseModel : ResponseModel
    {
        public List<CustomerModel> customers {get; set;} = new List<CustomerModel>();

    }
}