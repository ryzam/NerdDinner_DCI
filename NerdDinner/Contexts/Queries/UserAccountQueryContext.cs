using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisoDb;
using NerdDinner.Models.Domains;

namespace NerdDinner.Contexts.Queries
{
    public class UserAccountQueryContext
    {
        protected IUnitOfWork s;

        public UserAccountQueryContext(IUnitOfWork s)
        {
            this.s = s;
        }

        public UserAccount GetUserAccountBy(string userName, string password)
        {
            return s.Query<UserAccount>(c => c.Username == userName && c.Password == password).FirstOrDefault();
        }
    }
}