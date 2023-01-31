using System;
using System.Collections.Generic;
using fileuploader.common.models;

namespace fileuploader.swa.Services
{

    public class StateService
    {
        public CustomerModel? Customer { get; set; }

        public event Action? OnChange;

        public string fullName
        {
            get
            {
                if (this.Customer != null) return String.Format(this.Customer.Surname + ", " + this.Customer.FirstName);
                else return "";
            }
        }

        public StateService()
        {
        }

        public void SetCustomer(CustomerModel Customer)
        {
            this.Customer = new CustomerModel();

            this.Customer = Customer;
            NotifyStateChanged();
        }

        public void RemoveCustomer()
        {
            this.Customer = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}