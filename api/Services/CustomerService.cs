using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fileuploader.common.models;

namespace fileuploader.api.services
{
    public interface ICustomerService
    {

        public CustomersResponseModel GetCustomer(string FirstName, string Surname, string BirthDate);
        public CustomerResponseModel GetCustomer(Guid Id);


    }


    public class CustomerService : ICustomerService
    {
        public List<CustomerModel> Customers = new List<CustomerModel>();

        public CustomerService()
        {
            SeedData();
        }

        private void SeedData()
        {
            this.Customers.Add(new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "William",
                Surname = "Jones",
                BirthDate = DateTime.Now.Date
            });
            this.Customers.Add(new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                Surname = "Geldoff",
                BirthDate = DateTime.Parse("11/26/1985").Date
            });
            this.Customers.Add(new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                Surname = "McCarthy",
                BirthDate = DateTime.Parse("12/18/73").Date
            });
        }

        public CustomerResponseModel GetCustomer(Guid Id)
        {
            try
            {
                var q  = Customers.Where(p => p.Id == Id).FirstOrDefault();
                if (q != null)
                {
                    return new CustomerResponseModel()
                    {
                        status = status.success,
                        Message = null,
                        customer = q
                    };

                }
                else
                {
                    return new CustomerResponseModel()
                    {
                        status = status.norecords,
                        Message = "No Records Found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new CustomerResponseModel()
                {
                    status = status.failure,
                    Message = ex.Message,
                };
            }
        }
        public CustomersResponseModel GetCustomer(string FirstName, string Surname, string BirthDate)
        {
            try
            {
                
                IQueryable<CustomerModel> q = Customers.AsQueryable();
                q = q.Where(p => (FirstName == null || p.FirstName.ToLower().Contains(FirstName.ToLower())) && (Surname == null || p.Surname.ToLower().Contains(Surname.ToLower())) && p.Surname.ToLower().Contains(Surname.ToLower()));
                if (q.Any())
                {
                    return new CustomersResponseModel()
                    {
                        status = status.success,
                        Message = null,
                        customers = q.ToList()
                    };

                }
                else
                {
                    return new CustomersResponseModel()
                    {
                        status = status.norecords,
                        Message = "No Records Found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new CustomersResponseModel()
                {
                    status = status.failure,
                    Message = ex.Message,
                };
            }
        }
    }
}