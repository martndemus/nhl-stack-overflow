using System.Net.Mail;
using Mvc.Mailer;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer     
	{
		public UserMailer():
			base()
		{
			MasterName="_Layout";
		}

		
		public virtual MailMessage MailConfirm(string link, string email)
		{
            var mailMessage = new MailMessage { Subject = "Account registratie bevestigen - Question Jamw", Body = link };
			
			mailMessage.To.Add(email);
			//ViewBag.Data = someObject;
            ViewBag.WebLink = link;
			PopulateBody(mailMessage, viewName: "MailConfirm");

			return mailMessage;
		}

        public virtual MailMessage MailPassForgotten(string link, string email)
        {
            var mailMessage = new MailMessage { Subject = "Wachtwoord vergeten - Question Jam", Body = link };
            mailMessage.To.Add(email);
            ViewBag.Link = link;
            PopulateBody(mailMessage, viewName: "MailPassForgotten");

            return mailMessage;
        }
	}
}