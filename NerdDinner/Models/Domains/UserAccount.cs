using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NerdDinner.Contexts.Registrations;

namespace NerdDinner.Models.Domains
{
    public class UserAccount : DomainEntity , IRolePlayer , IUserAccount
    {
        public UserAccount()
        {
            ContactDetail = new ContactDetail();
        }
        public string Username { get; set; }

        public string Password { get; set; }

        public ContactDetail ContactDetail { get; set; }

        public void CreateUserAccount(string userName, string passWord, string email)
        {
            this.Username = userName;
            this.Password = passWord;
            ContactDetail.Email = email;
        }
    }

    public class ContactDetail : IRolePlayer
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }
    }

    public class Address : IRolePlayer
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string PostCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }

    public enum RegistrationStatus
    {
        Success, Fail
    }
}