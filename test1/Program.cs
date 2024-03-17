using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


           /* string fromMail = "tharindakaushalya778@gmai.com";
            string fromPassword = "";

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromMail);
            msg.Subject = "Test Sublect";
            msg.To.Add(new MailAddress("tharindakaushalya778@gmai.com"));
            msg.Body = "<html><body> Test Body </body></html>";
            msg.IsBodyHtml = true;


            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, 
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
           
            };


            smtpClient.Send(msg);*/

        }
    }
}
