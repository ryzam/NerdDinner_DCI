using System.Web.Mvc;
using NerdDinner.Helpers;
using NerdDinner.Models.Domains;

namespace NerdDinner.Models.ViewModels {
    public class HostDinnerFormViewModel {
        
        public HostDinner HostDinner { get; private set; }
        public SelectList Countries { get; private set; }

        public HostDinnerFormViewModel(HostDinner hostDinner)
        {
            HostDinner = hostDinner;
            Countries = new SelectList(PhoneValidator.Countries, HostDinner.DinnerDetail.Address.Country);
        }
    }
}
