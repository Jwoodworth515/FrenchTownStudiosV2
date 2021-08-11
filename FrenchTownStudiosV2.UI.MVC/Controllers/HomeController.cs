using FrenchTownStudiosV2.UI.MVC.Models;
using System.Web.Mvc;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace FrenchTownStudiosV2.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost] //Http verbs - It helps define the intent of the http request
        [ValidateAntiForgeryToken] // This validates the anti-forgery token created on the form.
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                // ModelState.IsValid is making sure we have all of the required info and that it is in the correct state.
                //If the modelstate is NOT valid, we will send the user back to the contact form to try again.
                return View(cvm);
            }

            //Build the email message that will be sent
            string message = $"(FrenchTownStudios) You have received an email from {cvm.Name} with a subject of {cvm.Subject}.  Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

            //MailMessage object - This is what we will use to send the email
            //Added Using Statement, commented out below
            //Go to SmarterASP and login.  CLick on Emails
            //                             from: This must be on your hosting account, to: any email you want to send a message to.
            //MailMessage mm = new MailMessage("email@yourdomain.com", "email@gmail.com", cvm.Subject, message);
            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, message);

            //MailMessage Properties
            //Allow HTML formatting in the email
            mm.IsBodyHtml = true;

            //To set the priority of the message
            mm.Priority = MailPriority.High;

            //Respond to email address
            mm.ReplyToList.Add(cvm.Email);

            //SMTP Client - This is defining the information from the host that allows an email to be sent from your application using the email servers on your SmarterASP account

            SmtpClient Client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
            Client.Port = 8889;
            //Client credentials
            Client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());

            //Try to send the email
            try
            {
                Client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be processed at this time.  Please try again later.  Error Message:<br/> {ex.StackTrace}";
                return View(cvm);
            }

            //If everything goes okay (message was sent)  then we will return the user to a Contact Confirmation View
            return View("EmailConfirmation", cvm);

        }
    }
}
