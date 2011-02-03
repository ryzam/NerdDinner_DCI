using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisoDb;
using NerdDinner.Models.Domains;

namespace NerdDinner.Contexts.Queries
{
    public class HostDinnersQueryContext
    {
        protected IUnitOfWork s;

        public HostDinnersQueryContext(IUnitOfWork s)
        {
            this.s = s;
        }

        public HostDinner GetHostDinnerBy(Guid Id)
        {
            return s.GetAll<HostDinner>().Where(c => c.Id == Id).FirstOrDefault();
        }


        public IEnumerable<HostDinner> FindUpcomingDinners()
        {
            return s.GetAll<HostDinner>()
                .Where(c => c.DinnerDetail.EventDate >= DateTime.Now)
                .OrderBy(c => c.DinnerDetail.EventDate);
        }
    }
}