using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NerdDinner.Models.Domains;

namespace NerdDinner.Contexts.Dinners
{
    public interface IHostDinner : IRole
    {
        void CreateHostDinner(string name, string hostedBy);
        void AddRVSP(DinnerRVSP rvsp);
    }

    public class CreateHostDinnerContext
    {
        protected IHostDinner IHostDinner { get; set; }

        public CreateHostDinnerContext Bind(IHostDinner hostDinner)
        {
            this.IHostDinner = hostDinner;
            return this;
        }

        public void CreateHostDinner(string name,string hostedBy,DinnerRVSP rvsp)
        {
            this.IHostDinner.PlayRole<CreateHostDinnerRole>()
                .CreateHostDinner(name,hostedBy, rvsp);
        }
    }

    public class CreateHostDinnerRole : PlayedBy<IHostDinner>
    {
        public void CreateHostDinner(string name, string hostedBy, DinnerRVSP rvsp)
        {
            Self.CreateHostDinner(name,hostedBy);
            Self.AddRVSP(rvsp);
        }
    }
}