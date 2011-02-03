using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisoDb;

namespace NerdDinner.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected SisoDatabase db;

        public AbstractController()
        {
            var cnInfo = new SisoConnectionInfo(@"sisodb:provider=Sql2008||plain:server=.\SQLEXPRESS;Database=nerddiner;user id=nerddinner;password=qwe123;");

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
