using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ConcertSite.Filters;
using ConcertSite.Models;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace ConcertSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SeeGuid(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            StatementOnBuy model = Db.StatementsOnBuy.SingleOrDefault(u => u.id == id);
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            User model = Db.Users.SingleOrDefault(u => u.id == id);
            Db.Users.Remove(model);
            Db.SaveChanges();
            return RedirectToAction("ViewUsers", "Account");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteConcert(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            Concert model = Db.Concerts.SingleOrDefault(u => u.id == id);
            Db.Concerts.Remove(model);
            Db.SaveChanges();
            return RedirectToAction("Tours", "Home");
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult UserStatements()
        {
            DataBaseContext Db = new DataBaseContext();
            List<StatementOnBuy> model = Db.StatementsOnBuy.Where(u => u.User.login == User.Identity.Name).ToList();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditConcert(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            Concert model = Db.Concerts.SingleOrDefault(u => u.id == id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditConcert(Concert model)
        {
            DataBaseContext Db = new DataBaseContext();
            Concert model1 = Db.Concerts.SingleOrDefault(u => u.id == model.id);
            model1.city = model.city;
            model1.country = model.country;
            model1.place = model.place;
            model1.singer = model.singer;
            model1.time = model.time;
            Db.SaveChanges();
            Db.Dispose();
            return RedirectToAction("Tours", "Home");
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditStatement(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            StatementOnBuy model = Db.StatementsOnBuy.SingleOrDefault(u => u.id == id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditStatement(StatementOnBuy model)
        {
            DataBaseContext Db = new DataBaseContext();
            StatementOnBuy model1 = Db.StatementsOnBuy.Include("Bilet").SingleOrDefault(u => u.id == model.id);
            model1.isPaid = model.isPaid;
            //var guid = new Guid();
            model1.Bilet.guidBilet = Guid.NewGuid();
            Db.SaveChanges();

            if (model1.isPaid == true)
            {
                var fromAdress = new MailAddress("Bilet.unical.number@gmail.com", "From concert-site");
                const string fromPassword = "1234vhFNLKFJ";
                const string subject = "unical code for bilet";
                string body = "Hello, thank you for your purchase. Your unical identity for ticket: " + model1.Bilet.guidBilet;
                var toAdress = new MailAddress(model1.User.email);
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAdress.Address, fromPassword)
                };
                try
                {
                    using (var message = new MailMessage()
                    {
                        From = fromAdress,
                        Subject = subject,
                        Body = body
                    })
                    {
                        message.To.Add(toAdress);
                        smtp.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("Company", "Account");
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditBilet(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            Bilet model = Db.Bilets.SingleOrDefault(u => u.id == id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditBilet(Bilet model)
        {
            DataBaseContext Db = new DataBaseContext();
            Bilet model1 = Db.Bilets.SingleOrDefault(u => u.id == model.id);
            model1.ConcertId = model.ConcertId;
            model1.isFree = model.isFree;
            model1.place = model.place;
            model1.price = model.price;
            model1.row = model.row;
            model1.sektor = model.sektor;
            Db.SaveChanges();
            return RedirectToAction("Tours", "Home");
        }

        [Authorize(Roles = "user, admin")]
        public ActionResult Company(string s = "")
        {
            DataBaseContext Db = new DataBaseContext();
            List<StatementOnBuy> model = Db.StatementsOnBuy.ToList();
            if (s == "")
            {
                model = Db.StatementsOnBuy.ToList();
            }
            else
            {
                model = Db.StatementsOnBuy.Where(u => u.Bilet.Concert.singer.Contains(s) || u.Bilet.Concert.city.Contains(s) || u.User.city.Contains(s)).ToList();
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditUser(int id)
        {
            DataBaseContext Db = new DataBaseContext();
            User model = Db.Users.SingleOrDefault(u => u.id == id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(User model)
        {
            DataBaseContext Db = new DataBaseContext();
            User model1 = Db.Users.SingleOrDefault(u => u.id == model.id);

            model1.name = model.name;
            model1.login = model.login;
            model1.pass = model.pass;
            model1.country = model.country;
            model1.city = model.city;
            model1.phone = model.phone;
            model1.email = model.email;
            model1.RoleId = model.RoleId;
            Db.SaveChanges();
            return RedirectToAction("ViewUsers", "Account");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ViewUsers(string s = "")
        {
            DataBaseContext Db = new DataBaseContext();
            List<User> model = Db.Users.ToList();
            if (s == "")
            {
                model = Db.Users.ToList();
            }
            else
            {
                model = Db.Users.Where(u => u.login.Contains(s) || u.name.Contains(s)).ToList();
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddConcert()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddConcert(Concert model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string _city = model.city;
                    string _country = model.country;
                    string _place = model.place;
                    string _singer = model.singer;
                    DateTime _time = model.time;

                    Concert concert = new Concert()
                    {
                        city = _city,
                        country = _country,
                        place = _place,
                        singer = _singer,
                        time = _time
                    };

                    DataBaseContext Db = new DataBaseContext();
                    Db.Concerts.Add(concert);
                    Db.SaveChanges();
                    return RedirectToAction("Tours", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddBilet()
        {
            Bilet model = new Bilet();
            model.id = 1;
            DataBaseContext Db = new DataBaseContext();
            List<ListBoxItems> listConcerts = (from v in Db.Concerts select new ListBoxItems { Id = v.id, Name = v.singer + " " + v.city }).ToList();
            ViewBag.ListConcerts = new SelectList(listConcerts, "Id", "Name", model.id);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddBilet(Bilet model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int _price = model.price;
                    int _sektor = model.sektor;
                    int _row = model.row;
                    int _place = model.place;
                    int _ConcertId = model.ConcertId;

                    Bilet bilet = new Bilet()
                    {
                        price = _price,
                        sektor = _sektor,
                        row = _row,
                        place = _place,
                        ConcertId = _ConcertId,
                        isFree = true
                    };
                    DataBaseContext Db = new DataBaseContext();
                    Db.Bilets.Add(bilet);
                    Db.SaveChanges();
                    return RedirectToAction("Tours", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            DataBaseContext Db1 = new DataBaseContext();
            List<ListBoxItems> listConcerts = (from v in Db1.Concerts select new ListBoxItems { Id = v.id, Name = v.singer + " " + v.city }).ToList();
            ViewBag.ListConcerts = new SelectList(listConcerts, "Id", "Name", model.id);
            return View(model);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Messages(string name = "")
        {
            ModelViewMessage model = new ModelViewMessage();
            DataBaseContext Db = new DataBaseContext();
            ViewBag.Message = "";
            model.UserListMessage = new List<string>();
            string currUser = User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                model.message = new Message();
                model.message.loginTo = name;
                model.myMessage = Db.Messages.Where(u => u.loginFrom == name && u.loginTo == currUser || u.loginTo == name && u.loginFrom == currUser).ToList();
                model.myMessage.Reverse();
            }
            else
            {
                List<Message> listMessage = Db.Messages.Where(u => u.loginFrom == currUser || u.loginTo == currUser).ToList();
                listMessage.Reverse();
                List<string> listUsers = new List<string>();
                foreach (var item in listMessage)
                {
                    if (!listUsers.Contains(item.loginFrom))
                    {
                        listUsers.Add(item.loginFrom);
                    }
                    if (!listUsers.Contains(item.loginTo))
                    {
                        listUsers.Add(item.loginTo);
                    }
                }
                listUsers.Remove(currUser);
                model.UserListMessage = listUsers;
            }
            return View(model);
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public ActionResult Messages(Message model)
        {
            ViewBag.Message = "";
            string currUser = User.Identity.Name;
            if (ModelState.IsValid)
            {
                model.loginFrom = currUser;
                model.time = DateTime.Now;
                DataBaseContext Db = new DataBaseContext();
                Db.Messages.Add(model);
                Db.SaveChanges();
                ViewBag.Message = "Message was sended";
            }
            ModelViewMessage model1 = new ModelViewMessage();
            DataBaseContext Db1 = new DataBaseContext();
            List<Message> listMessage = Db1.Messages.Where(u => u.loginFrom == currUser || u.loginTo == currUser).ToList();
            listMessage.Reverse();
            List<string> listUsers = new List<string>();
            foreach (var item in listMessage)
            {
                if (!listUsers.Contains(item.loginFrom))
                {
                    listUsers.Add(item.loginFrom);
                }
                if (!listUsers.Contains(item.loginTo))
                {
                    listUsers.Add(item.loginTo);
                }
            }
            listUsers.Remove(currUser);
            model1.UserListMessage = listUsers;
            return View(model1);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Cabinet()
        {
            DataBaseContext Db = new DataBaseContext();
            User model = Db.Users.SingleOrDefault(u => u.login == User.Identity.Name);
            return View(model);
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public ActionResult Cabinet(User model)
        {
            DataBaseContext Db = new DataBaseContext();
            User model1 = Db.Users.SingleOrDefault(u => u.login == User.Identity.Name);

            model1.name = model.name;
            model1.pass = model.pass;
            model1.country = model.country;
            model1.city = model.city;
            model1.phone = model.phone;
            model1.email = model.email;

            Db.SaveChanges();
            return View(model);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Buy(int id)
        {
            ViewBag.Message = "Buy";

            DataBaseContext Db = new DataBaseContext();
            Bilet model = Db.Bilets.SingleOrDefault(u => u.id == id);
            if (model != null)
            {
                User user = Db.Users.SingleOrDefault(u => u.login == User.Identity.Name);
                StatementOnBuy newState = new StatementOnBuy();
                newState.isPaid = false;
                newState.User = user;
                newState.Bilet = model;
                model.isFree = false;
                Db.StatementsOnBuy.Add(newState);
                Db.SaveChanges();
            }

            return RedirectToAction("Pay", "Account");
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Pay()
        {
            return View();
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string _name = model.name;
                    string _login = model.login;
                    string _pass = model.pass;
                    string _country = model.country;
                    string _city = model.city;
                    int _phone = Convert.ToInt32(model.phone);
                    string _email = model.email;
                    User user = new User()
                    {
                        name = _name,
                        login = _login,
                        pass = _pass,
                        country = _country,
                        city = _city,
                        phone = _phone,
                        email = _email,
                        RoleId = 2
                    };

                    DataBaseContext Db = new DataBaseContext();
                    User user1 = Db.Users.SingleOrDefault(u => u.login == model.login);
                    if (user1 != null)
                    {
                        ViewBag.Login = "Login is not empty.";
                    }

                    else if (user.pass == user.login)
                    {
                        ViewBag.Login = "Not valid password.";
                    }

                    else if (user.pass == null || user.login == null)
                    {
                        ViewBag.Login = "Please, reenter login and password.";
                    }

                    else
                    {
                        ViewBag.Login = "";
                        Db.Users.Add(user);
                        Db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(model.login, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }



        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
