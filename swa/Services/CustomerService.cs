using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using System.Text;
using fileuploader.common.helpers;
using System.Collections.Specialized;
using fileuploader.common.models;

namespace fileuploader.swa.Services
{

    public interface ICustomerService
    {
        Task<CustomersResponseModel> GetCustomers(string FirstName, string Surname, string BirthDate);
    }
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;

        public CustomerService(HttpClient httpClient)
        {
            this._http = httpClient;
        }

        public async Task<CustomersResponseModel> GetCustomers(string FirstName, string Surname, string BirthDate)
        {
            var Response = new CustomersResponseModel();

            try
            {
                var dataRequest = await _http.GetAsync(String.Format("/api/customers?{0}", QueryStringBuilder.Build(null, FirstName, "FirstName", Surname, "Surname", BirthDate, "BirthDate")));
                if (!dataRequest.IsSuccessStatusCode)
                {
                    Response.Message = dataRequest.ReasonPhrase;
                    Response.status = status.failure;
                }
                else
                {
                    try
                    {
                        Response = await dataRequest.Content.ReadFromJsonAsync<CustomersResponseModel>();
                        if (Response.customers.Count() > 0)
                        {
                            Response.status = status.success;
                        }
                        else
                        {
                            Response.Message = "No valid data returned";
                            Response.status = status.norecords;
                        }
                    }
                    catch
                    {
                            Response.Message = "No valid data returned";
                            Response.status = status.failure;
                    }

                }

                return Response;


            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                throw new ApplicationException($"Reason: {exception.Message}"); //or add a custom logic here
            }

            catch (Exception exception)
            {
                throw new ApplicationException($"Reason: {exception.Message}"); //or add a custom logic here
            }
        }


    }
}