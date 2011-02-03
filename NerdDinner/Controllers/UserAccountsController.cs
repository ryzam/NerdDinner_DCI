using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using NerdDinner.Contexts.Registrations;
using NerdDinner.Models.Domains;
using System.Web.Security;
using NerdDinner.Contexts.Queries;

namespace NerdDinner.Controllers
{
    public class UserAccountsController : AbstractController
    {
        //
        // GET: /UserAccount/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {

            return View();
        }

        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {
            if (!ValidateLogOn(userName, password))
            {
                ViewData["rememberMe"] = rememberMe;
                return View();
            }

            using (var s = db.CreateUnitOfWork())
            {
                //// Make sure we have the username with the right capitalization
                //// since we do case sensitive checks for OpenID Claimed Identifiers later.
                //userName = this.MembershipService.GetCanonicalUsername(userName);

                UserAccountQueryContext qryCtx = new UserAccountQueryContext(s);
                var userAccount = qryCtx.GetUserAccountBy(userName, password);

                if (userAccount == null)
                {
                    ModelState.AddModelError("User Account", "User Account doesn't exist.");
                }
                else
                {

                    FormsAuthenticationTicket authTicket = new
                                    FormsAuthenticationTicket(1, //version
                                    userName, // user name
                                    DateTime.Now,             //creation
                                    DateTime.Now.AddMinutes(30), //Expiration
                                    rememberMe, //Persistent
                                    userName); //since Classic logins don't have a "Friendly Name"

                    string encTicket = FormsAuthentication.Encrypt(authTicket);

                    this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                }
                s.Commit();
            }
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = "8"; //MembershipService.MinPasswordLength;

            return View();
        }

        [HttpPost]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {
            using (var s = db.CreateUnitOfWork())
            {
                //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

                if (ValidateRegistration(userName, email, password, confirmPassword))
                {
                    // Attempt to register the user
                    var userAccount = new UserAccount();

                    RegisterAccountContext ctx = new RegisterAccountContext();
                    ctx.Bind(userAccount)
                        .Register(userName, password, email);
                    s.Insert(userAccount);
                    s.Commit();

                    if (ctx.RegistrationStatus == RegistrationStatus.Success)
                    {

                        FormsAuthentication.SetAuthCookie(userName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("_FORM", ErrorCodeToString(ctx.RegistrationStatus));
                    }
                    
                }
            }
            // If we got this far, something failed, redisplay form
            return View();
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < 8)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         8));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(RegistrationStatus createStatus)
        {
            
            switch (createStatus)
            {
                case RegistrationStatus.Fail:
                    return "Registartion fail.Please contact administrator...";
                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            //if (!MembershipService.ValidateUser(userName, password))
            //{
            //    ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            //}

            return ModelState.IsValid;
        }
    }
}
