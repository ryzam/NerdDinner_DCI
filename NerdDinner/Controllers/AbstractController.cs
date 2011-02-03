using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisoDb;
using System.Configuration;

namespace NerdDinner.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected SisoDatabase db;

        public AbstractController()
        {
            var cnInfo = new SisoConnectionInfo(ConfigurationManager.AppSettings["nerddinner_connection"]);

            db = new SisoDatabase(cnInfo);
        }

        public ActionResult CreateDb()
        {
            db.InitializeExisting();

            Response.Write("Db created");

            return null;
        }

    }
}
