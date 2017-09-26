using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using EnF;
using System.Web;
namespace Web.Repository
{
   public class MailReponsitory
    {
        public static int Mail(string EmailTo, string MailBody, string MailSubject)
        {
            //string sql = "select * from Config";
            //SqlDataAdapter adp = new SqlDataAdapter(sql, Cconnect.GetConnection());
            //DataTable dt = new DataTable();
            //adp.Fill(dt);
            DayGianTuongEntities db = new DayGianTuongEntities();
            var config = db.Configs.First();
            string eMail = config.EmailSent;
            string Pass = config.MailPass;
          
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(config.EmailSent, "Thông tin dịch vụ");
            mail.To.Add(EmailTo); //nhap dia chi mail gui den
            mail.To.Add(config.MailRetrive);
            mail.Subject = MailSubject;
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(eMail, Pass);
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.Send(mail);
                return 1;
            }
            catch(Exception ex)
            {
                throw ex;
                return 0;
            }
            
        }
        //public static int SendMail(string EmailTo, string MailBody, string MailSubject)
        //{
        //    SmtpClient smtpClient = new SmtpClient();

        //    smtpClient.Host = "relay-hosting.secureserver.net";//relay-hosting.secureserver.net - smtpout.secureserver.net 
        //    smtpClient.Credentials = new System.Net.NetworkCredential("info@web.com", "admin@123");

        //    MailMessage message = new MailMessage();

        //    MailAddress fromAddress = new MailAddress("info@web.com", MailSubject); /*noreply @blockchainbitcoin.us*/
        //    MailAddress toAddress = new MailAddress(HttpUtility.HtmlEncode(EmailTo));
        //    MailAddress bccAddress = new MailAddress("info@web.com", "info@web.com");
                                        
        //    message.From = fromAddress;
        //    message.To.Add(toAddress);
        //    message.Bcc.Add(bccAddress);

        //    message.Subject = "Form";
        //    message.IsBodyHtml = true;
        //    message.Body = MailBody;
        //    try
        //    {

        //        smtpClient.Send(message);
        //        return 1;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
    }
}
