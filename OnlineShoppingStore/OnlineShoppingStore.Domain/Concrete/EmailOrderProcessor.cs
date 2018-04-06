//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OnlineShoppingStore.Domain.Concrete
//{
//    class EmailOrderProcessor
//    {
//    }
//}
using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace OnlineShoppingStore.Domain.Concrete
{
    //public class EmailSettings
    //{
    //    public string MailtoAddress = "orders@example.com";
    //    public string MailFromAddress = "sportsstore@example.com";
    //    public bool UseSsl = true;
    //    public string Username = "MySmtpUsername";
    //    public string Password = "MySmtpPassword";
    //    public string ServerName = "smtp.example.com";
    //    public int ServerPort = 587;
    //    public bool WriteAsFile = false;
    //    public string FileLocation = @"~/App_Data/sports_store_emails";
    //}

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient
            {
                EnableSsl = emailSettings.UseSsl,
                Host = emailSettings.ServerName,
                Port = emailSettings.ServerPort,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
            })
            {
                //if (emailSettings.WriteAsFile)
                //{
                //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                //    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                //    smtpClient.EnableSsl = false;
                //}

                var body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.lines)
                {
                    var subtotal = line.Product.price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity, line.Product.name, subtotal);
                }

                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.name)
                    .AppendLine(shippingInfo.line1)
                    .AppendLine(shippingInfo.line2 ?? "")
                    .AppendLine(shippingInfo.line3 ?? "")
                    .AppendLine(shippingInfo.city)
                    .AppendLine(shippingInfo.state ?? "")
                    .AppendLine(shippingInfo.Country)
                    
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingInfo.Giftwrap ? "yes" : "no");

                var mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailtoAddress, "New order submitted!", body.ToString());

                //if (emailSettings.WriteAsFile)
                //    mailMessage.BodyEncoding = Encoding.ASCII;

                smtpClient.Send(mailMessage);
            }
        }
    }
}