using System.Net.Mail;

namespace NHLStackOverflow.Mailers
{ 
    public interface IUserMailer
    {

        MailMessage MailConfirm(string link, string email);
		
		
	}
}