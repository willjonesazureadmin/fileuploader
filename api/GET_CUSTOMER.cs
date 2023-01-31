using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using fileuploader.api.services;
using fileuploader.common.models;

namespace fileuploader.api
{
    public class GET_CUSTOMER
    {
        private readonly ICustomerService customerService;

        public GET_CUSTOMER(ICustomerService _customerService)
        {
            this.customerService = _customerService;
        }

        [FunctionName("GET_CUSTOMER")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{id}")] HttpRequest req, Guid id,
            ILogger log)
        {
            try
            {
                var c = customerService.GetCustomer(id);
                if (c.status == status.success)
                {
                    return new OkObjectResult(c);
                }
                else if (c.status == status.norecords)
                {
                    return new NotFoundObjectResult(c);
                }
                else
                {
                    return new BadRequestObjectResult(c);
                }
            }
            catch
            {
                return new BadRequestResult();
            }

        }
    }
}
