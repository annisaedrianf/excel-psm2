using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using Chilkat;
//using Outlook = Microsoft.Office.Interop.Outlook;
//using System.Data.EntityClient;
using System.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;

namespace exels.Module.BusinessObjects
{
    public class ASEmailService
    {
        public ASEmailService()
        {
            //IConfigurationBuilder builder = new ConfigurationBuilder();
            //                        //.AddJsonFile("appsettings.json");
            //IConfigurationRoot configuration = builder.Build();
            //
            //Session.DefaultSession.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; 
            SetConfig();
        }
        private string mailLogFile;
        private string mailPassword;
        private int mailPort;
        private string mailServer;
        private string mailUser;
        private string mailUserFrom;
        private Boolean mailHTML;
        private Boolean mailSecure;
        private string mailStatus;
        public Boolean HTML 
        {
            get
            {
                return mailHTML;
            }
            set
            {
                mailHTML = value;
            }
        }

        public string EmailFileTemp { get; private set; }

        private void SetConfig()
        {

            //Configuration= new IConfiguration();

            //string appName = this.Configuration.GetSection("SR")["AppName"];


            //var configuration = new ConfigurationBuilder()
            //.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
            //.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
            //.Build();



            //mailUserFrom = configuration.GetSection("SR")["EmailUserFrom"]; ;
            //mailServer = configuration.GetSection("SR")["EmailServer"];
            //mailUser = configuration.GetSection("SR")["EmailUser"];
            //mailPassword = configuration.GetSection("SR")["EmailPassword"];
            //mailPort = System.Int32.Parse(configuration.GetSection("SR")["EmaiPort"]);
            //mailLogFile = configuration.GetSection("SR")["EmailLogFile"];
            //EmailFileTemp = configuration.GetSection("SR")["EmailFileTemp"];
            //if (configuration.GetSection("SR")["EmaiSL"].ToLower() == "true")
            //{
            //    mailSecure = true;
            //}
            //else
            //{
            //    mailSecure = false;

            //}
            ////mailUserFrom = ConfigurationManager.AppSettings["EmailUserFrom"];
            //if (configuration.GetSection("SR")["EmailHTML"] == "0")
            //{
            //    mailHTML = false;
            //}
            //else
            //{
            //    mailHTML = true;
            //};
            
            mailUserFrom = ConfigurationManager.AppSettings["EmailUserFrom"];
            mailServer = ConfigurationManager.AppSettings["EmailServer"];
            mailUser = ConfigurationManager.AppSettings["EmailUser"];
            mailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            mailPort = System.Int32.Parse(ConfigurationManager.AppSettings["EmaiPort"]);
            mailLogFile = ConfigurationManager.AppSettings["EmailLogFile"];
            EmailFileTemp = ConfigurationManager.AppSettings["EmailFileTemp"];
            if (ConfigurationManager.AppSettings["EmaiSL"].ToLower() == "true")
            {
               mailSecure = true;
            }
            else
            {
               mailSecure = false;

            }
            //mailUserFrom = ConfigurationManager.AppSettings["EmailUserFrom"];
            if (ConfigurationManager.AppSettings["EmailHTML"] == "0")
            {
                mailHTML = false;
            }
            else
            {
                mailHTML = true;
            };
            
        }
        // public async Task<bool> SendEmailAsync(string emailFrom, string emailTo, string emailCC, string subject, string mailbody , string emailAttachment)
        public string SendEmailAsync(string emailFrom, string emailTo, string emailCC, string subject, string mailbody, string emailAttachment)
        {
            // var configuration = new ConfigurationBuilder()
            //.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
            //.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
            //.Build();

            // mailUserFrom = configuration.GetSection("SR")["EmailUserFrom"];
            // mailServer = configuration.GetSection("SR")["EmailServer"];
            // mailUser = configuration.GetSection("SR")["EmailUser"];
            // mailPassword = configuration.GetSection("SR")["EmailPassword"];
            // mailPort = System.Int32.Parse(configuration.GetSection("SR")["EmaiPort"]);
            // mailLogFile = configuration.GetSection("SR")["EmailLogFile"];
            // EmailFileTemp = configuration.GetSection("SR")["EmailFileTemp"];

            var from = new MailAddress(emailFrom);
            var to = new MailAddress(emailTo);

            var useDefaultCredentials = true;
            var enableSsl = false;
            var replyto = ""; // set here your email; 
            var userName = string.Empty;
            var password = string.Empty;
            var port = 25;
            var host = mailServer; // "localhost";

            userName = mailUser;// "abc"; // setup here the username; 
            password = mailPassword;// "abc"; // setup here the password; 
            bool.TryParse("true", out useDefaultCredentials); //setup here if it uses defaault credentials 
            bool.TryParse("true", out enableSsl); //setup here if it uses ssl 
            int.TryParse(mailPort.ToString(), out port); //setup here the port 
            host = mailServer;// "www.google.com"; //setup here the host 

            using (var mail = new MailMessage(from, to))
            {
                mail.Subject = subject;
                mail.Body = mailbody;
                if (emailAttachment != "")
                {
                    Attachment attachment = new System.Net.Mail.Attachment(emailAttachment);
                    mail.Attachments.Add(attachment);
                }
                mail.IsBodyHtml = true;

                mail.ReplyToList.Add(new MailAddress(replyto, "My Email"));
                mail.ReplyToList.Add(from);
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
                                                   DeliveryNotificationOptions.OnFailure |
                                                   DeliveryNotificationOptions.OnSuccess;

                using (var client = new SmtpClient())
                {
                    client.Host = host;
                    client.EnableSsl = enableSsl;
                    client.Port = port;
                    client.UseDefaultCredentials = useDefaultCredentials;

                    if (!client.UseDefaultCredentials && !string.IsNullOrEmpty(userName) &&
                        !string.IsNullOrEmpty(password))
                    {
                        client.Credentials = new NetworkCredential(userName, password);
                    }

                    try
                    {
                        client.Send(mail);

                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }
                    //await client.SendMailAsync(mail);
                }
            }

            //return true;
        }
        //private SqlConnection cnnConnect;
        private SqlConnection OpenDBConn(string Database = "")
        {
            SqlConnection cnnConnect;
            cnnConnect = new SqlConnection(Database);
            try
            {
                cnnConnect.Open();
                return cnnConnect;
            }
            catch (Exception ex)
            {
                ASEmailService eService = new ASEmailService();
                eService.WriteToFile(" {0}  OpenDBConn " + ex.Message);

            }

            return cnnConnect;
        }
        public SqlDataReader ExeQuery(string sqlQuery, string database = "")
        {


            SqlCommand command;
            SqlDataReader dataReader;
            // command = new SqlCommand(sqlQuery, cnnConnect);
            command = new SqlCommand(sqlQuery, OpenDBConn(database));
            //dataReader = command.ExecuteReader();
            return command.ExecuteReader();
            //return dataReader;
        }
        public void WriteToFile(string text)
        {
            SetConfig();
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            try
            {
                string appPath = EmailFileTemp;
                CreateFoder(appPath);
                string path = string.Format("{0}\\{1}", appPath, mailLogFile + System.DateTime.Now.ToString("yyMMddhh") + ".log");

                using (System.IO.StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    writer.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CreateFoder(string subdir)
        {
            try
            {
                if (!Directory.Exists(subdir))
                {
                    Directory.CreateDirectory(subdir);
                }
            }
            catch (Exception ex)
            { }
        }
        private string HtmlCss
        {

            get
            {
                string sCss;
                sCss = " table { font - family: arial, sans - serif; " +
                 " border - collapse: collapse; " +
                 " width: 100 %;  } " +
                 " td, th {  border: 1px solid #dddddd; " +
                 " text - align: left; " +
                 " padding: 8px; } " +
                 " tr: nth - child(even) { " +
                 " background - color: #dddddd; } ";

                return sCss;
            }
        }

        private Chilkat.StringBuilder BodyEmail(string emailFrom,
                               string emailTo,
                               string emailCC,
                               string emailSubject,
                               string emailBodyhDR,
                               string emailBody
            )
        {

            string body = emailBody;
            body = body.Replace("[$from]", emailFrom);
            body = body.Replace("[$to]", emailTo);
            body = body.Replace("[$subject]", emailSubject);
            Chilkat.StringBuilder sbCss = new Chilkat.StringBuilder();
            bool bCrlf = true;
            sbCss.AppendLine("body {", bCrlf);
            sbCss.AppendLine("    background-color: powderblue;", bCrlf);
            sbCss.AppendLine("}", bCrlf);
            sbCss.AppendLine("h1 {", bCrlf);
            sbCss.AppendLine("    color: blue;", bCrlf);
            sbCss.AppendLine("}", bCrlf);
            sbCss.AppendLine("p {", bCrlf);
            sbCss.AppendLine("    color: red;", bCrlf);
            sbCss.AppendLine("}", bCrlf);
            sbCss.AppendLine("td, th {", bCrlf);
            sbCss.AppendLine("   border: 1px solid #dddd;", bCrlf);
            sbCss.AppendLine("   padding: 1px;", bCrlf);
            sbCss.AppendLine("}", bCrlf);


            //  It's possible to add a CSS file directly by calling AddRelatedFile.
            //  This example will add the CSS from a string.
            //string filenameInHtml = "styles.css";

            Chilkat.StringBuilder sbHtml = new Chilkat.StringBuilder();

            sbHtml.AppendLine("<html>", bCrlf);
            sbHtml.AppendLine("<head>", bCrlf);
            sbHtml.AppendLine("<meta http-equiv ='Content-Type' content ='text/html; charset=utf-8'></head>", bCrlf);
            sbHtml.AppendLine("<style type = 'text /css'>", bCrlf);
            sbHtml.AppendLine("table.gridtable {", bCrlf);
            sbHtml.AppendLine("font-family: verdana,arial,sans-serif;", bCrlf);
            sbHtml.AppendLine("font-size:12px;", bCrlf);
            sbHtml.AppendLine("color:#000000;", bCrlf);
            sbHtml.AppendLine("border-width: 1px;", bCrlf);
            sbHtml.AppendLine("border-color: #666666;", bCrlf);
            sbHtml.AppendLine("border-collapse: collapse;", bCrlf);
            sbHtml.AppendLine("}", bCrlf);
            sbHtml.AppendLine("table.gridtable th {", bCrlf);
            sbHtml.AppendLine("border-width: 1px;", bCrlf);
            sbHtml.AppendLine("font-size:11px;", bCrlf);
            sbHtml.AppendLine("border-style: solid;", bCrlf);
            sbHtml.AppendLine("border-color: #666666;", bCrlf);
            sbHtml.AppendLine("}", bCrlf);
            sbHtml.AppendLine("table.gridtable td {", bCrlf);
            sbHtml.AppendLine("border-width: 1px;", bCrlf);
            sbHtml.AppendLine("font-size:11px;", bCrlf);
            sbHtml.AppendLine("border-style: solid;", bCrlf);
            sbHtml.AppendLine("border-color: #666666;", bCrlf);
            sbHtml.AppendLine("}", bCrlf);
            sbHtml.AppendLine("</style>", bCrlf);
            sbHtml.AppendLine("<body>", bCrlf);
            sbHtml.AppendLine(emailBodyhDR, bCrlf);
            sbHtml.AppendLine(body, bCrlf);
            sbHtml.AppendLine("</body></html>", bCrlf);
            return sbHtml;
        }
        public void SendEmail(string emailFrom,
                               string emailTo,
                               string emailCC,
                               string emailSubject,
                               string emailBodyhDR,
                               string emailBody,
                               string emailAttachment
                               //string rhtml
                               )
        {
            //emailBodyhDR=emailBodyhDR.ToHtml();
            SetConfig();
            string err = "";
            if (emailFrom == null)
            {
                emailFrom = "";
            }
            if (emailTo == null)
            {
                emailTo = "";
            }
            if (emailCC == null)
            {
                emailCC = "";
            }
            if (emailSubject == "")
            {
                emailSubject = "";
            }
            if (emailBodyhDR == null)
            {
                emailBodyhDR = "";
            }
            if (emailBody == null)
            {
                emailBody = "";
            }
            try
            {
                //  The mailman object is used for sending and receiving email.
                MailMan mailman = new Chilkat.MailMan();

                //  Any string argument automatically begins the 30-day trial.
                //Oo7VJk.EM10124_l48OQcxxpFlL
                bool success = mailman.UnlockComponent("Oo7VJk.EM10124_l48OQcxxpFlL");
                if (success != true)
                {
                    //Console.WriteLine(mailman.LastErrorText);
                    WriteToFile(mailman.LastErrorText);
                    return;
                }

                //  Set the SMTP server.
                // mailman.SmtpHost = "smtp.att.yahoo.com";
                mailman.SmtpHost = mailServer;
                //  Set SMTP login and password (if necessary)
                //mailman.SmtpUsername = "myUsername@sbcglobal.net";
                mailman.SmtpUsername = mailUser;
                // mailman.SmtpPassword = "myPassword";
                mailman.SmtpPassword = mailPassword;


                mailman.SmtpSsl = mailSecure;
                mailman.StartTLS = true;
                //mailman.SmtpPort = 465;
                mailman.SmtpPort = mailPort;
                //  Create a new email object
                Chilkat.Email email = new Chilkat.Email();
                //email.AddTo("Chilkat Admin", "admin@chilkatsoft.com");
                if (emailTo != "")
                {

                    email.AddMultipleTo(emailTo.Replace(";", ","));
                }

                if (emailCC != "")
                {
                    //emailCC = "srieddy@engineer.com";
                    //email.AddCC("CC", emailCC);
                    email.AddMultipleCC(emailCC.Replace(";", ","));
                }

                // email.Body = "This is a test";
                string body = emailBody;
                email.From = emailFrom;
                email.FromAddress = emailFrom;
                email.FromName = emailFrom;
                email.Subject = emailSubject;

                body = body.Replace("[$from]", email.From.ToString());
                body = body.Replace("[$to]", email.GetToName(0).ToString());
                body = body.Replace("[$subject]", email.Subject);

                Chilkat.StringBuilder sbCss = new Chilkat.StringBuilder();
                bool bCrlf = true;
                sbCss.AppendLine("body {", bCrlf);
                sbCss.AppendLine("    background-color: powderblue;", bCrlf);
                sbCss.AppendLine("}", bCrlf);
                sbCss.AppendLine("h1 {", bCrlf);
                sbCss.AppendLine("    color: blue;", bCrlf);
                sbCss.AppendLine("}", bCrlf);
                sbCss.AppendLine("p {", bCrlf);
                sbCss.AppendLine("    color: red;", bCrlf);
                sbCss.AppendLine("}", bCrlf);
                sbCss.AppendLine("td, th {", bCrlf);
                sbCss.AppendLine("   border: 1px solid #dddd;", bCrlf);
                sbCss.AppendLine("   padding: 1px;", bCrlf);
                sbCss.AppendLine("}", bCrlf);


                //  It's possible to add a CSS file directly by calling AddRelatedFile.
                //  This example will add the CSS from a string.
                string filenameInHtml = "styles.css";

                Chilkat.StringBuilder sbHtml = new Chilkat.StringBuilder();

                sbHtml.AppendLine("<html>", bCrlf);
                sbHtml.AppendLine("<head>", bCrlf);
                sbHtml.AppendLine("<meta http-equiv ='Content-Type' content ='text/html; charset=utf-8'></head>", bCrlf);
                sbHtml.AppendLine("<style type = 'text /css'>", bCrlf);
                sbHtml.AppendLine("table.gridtable {", bCrlf);
                sbHtml.AppendLine("font-family: verdana,arial,sans-serif;", bCrlf);
                sbHtml.AppendLine("font-size:12px;", bCrlf);
                sbHtml.AppendLine("color:#000000;", bCrlf);
                sbHtml.AppendLine("border-width: 1px;", bCrlf);
                sbHtml.AppendLine("border-color: #666666;", bCrlf);
                sbHtml.AppendLine("border-collapse: collapse;", bCrlf);
                sbHtml.AppendLine("}", bCrlf);
                sbHtml.AppendLine("table.gridtable th {", bCrlf);
                sbHtml.AppendLine("border-width: 1px;", bCrlf);
                sbHtml.AppendLine("font-size:11px;", bCrlf);
                sbHtml.AppendLine("border-style: solid;", bCrlf);
                sbHtml.AppendLine("border-color: #666666;", bCrlf);
                sbHtml.AppendLine("}", bCrlf);
                sbHtml.AppendLine("table.gridtable td {", bCrlf);
                sbHtml.AppendLine("border-width: 1px;", bCrlf);
                sbHtml.AppendLine("font-size:11px;", bCrlf);
                sbHtml.AppendLine("border-style: solid;", bCrlf);
                sbHtml.AppendLine("border-color: #666666;", bCrlf);
                sbHtml.AppendLine("}", bCrlf);
                sbHtml.AppendLine("</style>", bCrlf);
                sbHtml.AppendLine("<body>", bCrlf);
                sbHtml.AppendLine(emailBodyhDR, bCrlf);
                sbHtml.AppendLine(body, bCrlf);
                sbHtml.AppendLine("</body></html>", bCrlf);

                email.From = emailFrom;
                email.FromAddress = emailFrom;
                email.FromName = emailFrom;
                if (mailHTML)
                {
                    email.SetHtmlBody(sbHtml.GetAsString());
                }
                else
                    email.Body = body;



                if (emailAttachment != "")
                {
                    email.AddFileAttachment(emailAttachment);

                }

                success = mailman.SendEmail(email);
                if (success != true)
                {
                    Console.WriteLine(mailman.LastErrorText);
                    this.WriteToFile(String.Format("The message was sent to {0} successfully!",
                           mailman.LastErrorText));
                    return;
                }
                else
                {
                    this.WriteToFile(String.Format("The message was sent to {0} successfully!",
                           ""));
                    mailStatus = "Completed";
                }

                success = mailman.CloseSmtpConnection();
                if (success != true)
                {
                    err = mailman.LastErrorText;
                    Console.WriteLine("Connection to SMTP server not closed cleanly.");
                }

                // Console.WriteLine("Mail Sent!");
            }
            catch
            {

                this.WriteToFile(err);
                mailStatus = err;
            }
        }

        public void SendEmail(
                                   string emailFrom,
                                   string emailTo,
                                   string emailCC,
                                   string emailSubject,
                                   string emailBodyhDR,
                                   string emailBody,
                                   string emailAttachment,
           object application
            //  Outlook.Application application
            )
        {
            SetConfig();
            Chilkat.StringBuilder sbHtml = new Chilkat.StringBuilder();

            //// Create a new MailItem and set the To, Subject, and Body properties.
            //Outlook.MailItem newMail = (Outlook.MailItem)application.CreateItem(Outlook.OlItemType.olMailItem);
            //newMail.To = emailTo;
            //newMail.Subject = emailSubject;

            //sbHtml = BodyEmail(emailFrom,
            //                    emailTo,
            //                    emailCC,
            //                    emailSubject,
            //                    emailBodyhDR,
            //                    emailBody);
            //if (mailHTML)
            //{
            //    newMail.HTMLBody = sbHtml.GetAsString();
            //}
            //else
            //{
            //    newMail.Body = sbHtml.ToString();
            //}
            ////newMail.HTMLBody

            //// Retrieve the account that has the specific SMTP address.
            //Outlook.Account account = GetAccountForEmailAddress(application, emailFrom);
            //// Use this account to send the email.
            //// account.DisplayName = "sri eddy suasono";
            //newMail.SendUsingAccount = account;
            //if (emailAttachment != "")
            //{
            //    //newMail.Attachments.Add( .AddFileAttachment(emailAttachment);

            //}
            //newMail.Send();
        }
        //public Outlook.Account GetAccountForEmailAddress(Outlook.Application application, string smtpAddress)
        public object GetAccountForEmailAddress(object application, string smtpAddress)
        {

            //// Loop over the Accounts collection of the current Outlook session.
            //Outlook.Accounts accounts = application.Session.Accounts;
            //foreach (Outlook.Account account in accounts)
            //{
            //    // When the email address matches, return the account.
            //    if (account.SmtpAddress == smtpAddress)
            //    {
            //        return account;
            //    }
            //}
            //throw new System.Exception(string.Format("No Account with SmtpAddress: {0} exists!", smtpAddress));
            return null;
        }
        //private void RemoveAttachmentsAndSaveToDisk(string path, Outlook.Folder folder, int size)
        private void RemoveAttachmentsAndSaveToDisk(string path, object folder, int size)
        {
            //Outlook.Items attachItems;
            //Outlook.Attachment attachment;
            //Outlook.Attachments attachments;
            //int byValueCount;
            //int removeCount;
            //bool saveMessage;
            //try
            //{
            //    // The restriction will find all items that
            //    // have attachments and MessageClass = IPM.Note.
            //    string filter = "@SQL=" + "\""
            //        + "urn:schemas:httpmail:hasattachment"
            //        + "\"" + " = True" + " AND " + "\""
            //        + "http://schemas.microsoft.com/mapi/proptag/0x001A001E"
            //        + "\"" + " = 'IPM.Note'";
            //    attachItems = folder.Items.Restrict(filter);
            //    foreach (Outlook.MailItem mail in attachItems)
            //    {
            //        saveMessage = false;
            //        byValueCount = 0;
            //        attachments = mail.Attachments;
            //        // Obtain the count of ByValue attachments.
            //        foreach (Outlook.Attachment attach in attachments)
            //        {
            //            if (attach.Size > size
            //                & attach.Type ==
            //                Outlook.OlAttachmentType.olByValue)
            //            {
            //                byValueCount = byValueCount + 1;
            //            }
            //        }
            //        if (byValueCount > 0)
            //        {
            //            // removeCount is number of attachments to remove.
            //            removeCount = attachments.Count - byValueCount;
            //            while (mail.Attachments.Count != removeCount)
            //            {
            //                // Use indexer to obtain 
            //                // first attachment in collection.
            //                attachment = mail.Attachments[1];
            //                // You can refine this code to save 
            //                // separate copies of attachments 
            //                // with the same name.
            //                attachment.SaveAsFile(path + @"\"
            //                    + attachment.FileName);
            //                attachment.Delete();
            //                if (saveMessage != true)
            //                {
            //                    saveMessage = true;
            //                }
            //            }
            //            if (saveMessage)
            //            {
            //                mail.Save();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ///Debug.WriteLine(ex.Message);
            //}
        }
    }
}
