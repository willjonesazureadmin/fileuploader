using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using fileuploader.api.services;
using fileuploader.common.models;

namespace fileuploader.api
{
    public class GET_CUSTOMERS
    {
        private readonly ICustomerService customerService;

        public GET_CUSTOMERS(ICustomerService _customerService)
        {
            this.customerService = _customerService;
        }

        [FunctionName("GET_CUSTOMERS")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers")] HttpRequest req, ILogger log)
        {
            try
            {
                string surname = req.Query["surname"];
                string firstName = req.Query["firstName"];
                string birthDate = req.Query["birthDate"];
                var c = customerService.GetCustomer(firstName, surname, birthDate);
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
