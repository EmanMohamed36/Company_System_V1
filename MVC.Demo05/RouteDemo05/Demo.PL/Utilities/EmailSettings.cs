using System.Net;
using System.Net.Mail;

namespace Demo.PL.Utilities
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
			try
			{
				var client = new SmtpClient("smtp.gmail.com", 587);
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("ei3377898@gmail.com", "qewn lnyr cgks fpmg");
				client.Send("ei3377898@gmail.com", email.To, email.Subject, email.Body);
				return true;
			}
			catch (Exception)
			{

				return false;
			}
        }
    }
}
