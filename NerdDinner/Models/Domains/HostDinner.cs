using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NerdDinner.Contexts.Dinners;

namespace NerdDinner.Models.Domains
{
    public class HostDinner : DomainEntity, IRolePlayer , IHostDinner
    {
        public HostDinner()
        {
            DinnerDetail = new DinnerDetail();
            DinnerMapLocation = new DinnerMapLocation();
            ContactDetail = new ContactDetail();
        }

        public bool IsUserRegistered(string userName)
        {
            return RVSPs.Any(r =>  r.AttendeeName == userName);
        }

        public bool IsHostedBy(string userName)
        {
            return String.Equals(DinnerDetail.HostedBy, userName, StringComparison.Ordinal);
        }

        public DinnerDetail DinnerDetail { get; set; }

        public DinnerMapLocation DinnerMapLocation { get; set; }

        public ContactDetail ContactDetail { get; set; }

        public IList<DinnerRVSP> RVSPs { get; set; }

        public void AddRVSP(DinnerRVSP rvsp)
        {
            if (RVSPs == null)
                RVSPs = new List<DinnerRVSP>();
            RVSPs.Add(rvsp);
        }

        public void CreateHostDinner(string name, string hostedBy)
        {
            this.ContactDetail.Name = name;
            this.DinnerDetail.HostedBy = hostedBy;

        }
    }

    public class DinnerDetail : IRolePlayer
    {
        public DinnerDetail()
        {
            Address = new Address();
        }

        public string Title { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public string HostedBy { get; set; }

        public Address Address { get; set; }

       
    }

    public class DinnerMapLocation : IRolePlayer
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class DinnerRVSP : DomainEntity, IRolePlayer
    {
        public DinnerRVSP()
        {

        }

        public string AttendeeName { get; set; }
    }
}