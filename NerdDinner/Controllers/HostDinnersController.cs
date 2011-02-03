using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models.Domains;
using NerdDinner.Models.ViewModels;
using NerdDinner.Contexts.Dinners;
using NerdDinner.Helpers;
using NerdDinner.Contexts.Queries;

namespace NerdDinner.Controllers
{
    public class HostDinnersController : AbstractController
    {
        //
        // GET: /HostDinners/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {

            HostDinner dinner = new HostDinner();
            dinner.DinnerDetail.EventDate = DateTime.Now.AddDays(7);
       

            return View(new HostDinnerFormViewModel(dinner));
        }

        [HttpPost, Authorize]
        public ActionResult Create(HostDinner hostDinner)
        {

            if (ModelState.IsValid)
            {
                using (var s = db.CreateUnitOfWork())
                { 
                    NerdIdentity nerd = (NerdIdentity)User.Identity;
                    
                    DinnerRVSP rsvp = new DinnerRVSP();
                    rsvp.AttendeeName = nerd.FriendlyName;
                   
                    CreateHostDinnerContext ctx = new CreateHostDinnerContext();
                    ctx.Bind(hostDinner)
                        .CreateHostDinner(nerd.Name,nerd.FriendlyName,rsvp);

                    s.Insert(hostDinner);
                    s.Commit();
                 
                    return RedirectToAction("Details", new { id = hostDinner.Id });
                }
            }

            return View(new HostDinnerFormViewModel(hostDinner));
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new FileNotFoundResult { Message = "No Dinner found due to invalid dinner id" };
            }

            using (var s = db.CreateUnitOfWork())
            {
                HostDinnersQueryContext qryCtx = new HostDinnersQueryContext(s);

                HostDinner hostDinner = qryCtx.GetHostDinnerBy(id.Value);

                if (hostDinner == null)
                {
                    return new FileNotFoundResult { Message = "No Dinner found for that id" };
                }

                return View(hostDinner);
            }
        }

        public ActionResult WebSlicePopular()
        {
            ViewData["Title"] = "Popular Nerd Dinners";
            using (var s = db.CreateUnitOfWork())
            {
                HostDinnersQueryContext qryCtx = new HostDinnersQueryContext(s);
                var model = from dinner in qryCtx.FindUpcomingDinners()
                            orderby dinner.RVSPs.Count descending
                            select dinner;

                return View("WebSlice", model.ToList().Take(5));
            }
        }

    }
}
