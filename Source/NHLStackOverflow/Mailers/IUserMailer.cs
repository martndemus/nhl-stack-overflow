using System.Net.Mail;

namespace NHLStackOverflow.Mailers
{ 
    public interface IUserMailer
    {
        // what we give along when we create a mail
        MailMessage MailConfirm(string link, string email);
        MailMessage MailPassForgotten(string link, string email);
	}
}