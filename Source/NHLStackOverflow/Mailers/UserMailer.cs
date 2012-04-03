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
            // create the variables which we give allong
            var mailMessage = new MailMessage { Subject = "Account registratie bevestigen - Question Jam", Body = link };
			
            // add the email adress to the message
			mailMessage.To.Add(email);
            ViewBag.WebLink = link; // add the link to it
            // create the actual email
			PopulateBody(mailMessage, viewName: "MailConfirm");
            // and return this
			return mailMessage;
		}

        public virtual MailMessage MailPassForgotten(string link, string email)
        {
            // create the variables which we give allong
            var mailMessage = new MailMessage { Subject = "Wachtwoord vergeten - Question Jam", Body = link };

            // add the email adress to the message
            mailMessage.To.Add(email);
            // give along the link
            ViewBag.Link = link;
            // create the actual email
            PopulateBody(mailMessage, viewName: "MailPassForgotten");

            return mailMessage;
        }
	}
}