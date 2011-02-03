using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NerdDinner.Models.Domains;

namespace NerdDinner.Contexts.Registrations
{
    public interface IUserAccount : IRole
    {
        void CreateUserAccount(string userName, string passWord, string email);
    }

    public class RegisterAccountContext
    {
        protected IUserAccount IUserAccount { get; private set; }

        public RegistrationStatus RegistrationStatus { get; private set; }

        public RegisterAccountContext Bind(IUserAccount userAccount)
        {
            this.IUserAccount = userAccount;
            return this;
        }

        public void Register(string userName, string passWord, string email)
        {
            try
            {
                this.IUserAccount.PlayRole<RegisterAccountRole>()
                    .Register(userName, passWord, email);
                RegistrationStatus = RegistrationStatus.Success;
            }
            catch
            {
                RegistrationStatus = RegistrationStatus.Fail;
            }
        }
    }


    public class RegisterAccountRole : PlayedBy<IUserAccount>
    {
        public void Register(string userName, string passWord, string email)
        {
            Self.CreateUserAccount(userName, passWord, email);
        }
    }

    
}