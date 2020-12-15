using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace MobileStore.Pages
{
    public partial class MailPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                int port = 587;
                bool enableSSL = true;

                string emailFrom = "bot-bot-81@bk.ru";
                string password = "bot12345!";
                string emailTo = "i_v.d.kochulaev@mpt.ru";
                string smtpAddress = "smtp.mail.ru"; //smtp протокол
                string name = "Отправил " + tbName.Text.ToString() + ", почта " + tbMail.Text;
                string message = tbMessage.Text; 

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = "Обратная связь";
                mail.Body = "\r\n" + name + "\r\n"+"\r\n" + message; //тело сообщения
                mail.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
                tbMail.Text = "";
                tbMessage.Text = "";
                tbName.Text = "";
            }
            catch
            {
               
            }
        }
    }
}