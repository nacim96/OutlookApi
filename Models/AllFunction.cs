using System;
using System.Collections.Generic;
using System.IO;

//add references of OpenPop  
using OpenPop.Mime;
using OpenPop.Pop3;

namespace apiOutlook.Models
{
    public class AllFunction
    {
        private static string keeJob = "contact@keejobmail.com";
        private static string useremail = "**********@**********";
        public MessageModel GetEmailContent(Message objMessage)
        {
            MessageModel message = new MessageModel();

            //MessagePart plainTextPart = null, HTMLTextPart = null;  

            // Message objMessage = objPOP3Client.GetMessage(messageNumber);

            message.Id = objMessage.Headers.MessageId == null ? "" : objMessage.Headers.MessageId.Trim();

            message.FromAddress = objMessage.Headers.From.Address.Trim();
            message.FromName = objMessage.Headers.From.DisplayName.Trim();
            message.msgDate = objMessage.Headers.DateSent;

            message.Subject = objMessage.Headers.Subject.Trim();

            /*plainTextPart = objMessage.FindFirstPlainTextVersion();  
            message.Body = (plainTextPart == null ? "" : plainTextPart.GetBodyAsText().Trim());  

            HTMLTextPart = objMessage.FindFirstHtmlVersion();  
            message.Html = (HTMLTextPart == null ? "" : HTMLTextPart.GetBodyAsText().Trim());  
          */
            List<MessagePart> attachment = objMessage.FindAllAttachments();
            foreach (var ado in attachment)

            {
                Console.WriteLine(ado.ContentType.MediaType);
                if (ado.ContentType.MediaType.Equals("application/pdf"))
                {
                 // Console.WriteLine(ado.GetBodyAsText());
                    message.Attachments.Add(new Attachment(ado.FileName.Trim()/*,ado.Body*/));
                    downloadFile(ado);
                }
            }

            return message;

        }


        public void downloadFile(MessagePart ado)
        {
            string attachmentdirectory = @"c:\temp\mail\attachments";
            Directory.CreateDirectory(attachmentdirectory);
            //overwrites MessagePart.Body with attachment
            File.WriteAllBytes(ado.FileName.Trim(), ado.Body);
            // Console.WriteLine(ado.GetBodyAsText());
            // Save the file 
            ado.Save(new System.IO.FileInfo(System.IO.Path.Combine(attachmentdirectory, ado.FileName.Trim())));
        }

        public bool Connect(Pop3Client client)
        {
            client.Connect("******************", 995, true);
            client.Authenticate("****************", "**************", AuthenticationMethod.UsernameAndPassword);

            if (client.Connected)
                return true;
            else return false;

        }

        public static List<Message> FetchUnseenMessages(Pop3Client client, List<string> seenUids)
        {
            {
                Console.WriteLine("seen : "+seenUids.Count);
                // Fetch all the current uids seen
                List<string> uids = client.GetMessageUids();
                // Create a list we can return with all new messages
                List<Message> newMessages = new List<Message>();
                // All the new messages not seen by the POP3 client
                for (int i = 0; i < uids.Count; i++)
                {        //Console.WriteLine(uids[i]);
                         //  Console.WriteLine(seenUids.Contains(seenUids[i]));
                    string currentUidOnServer = uids[i];
                    if (!seenUids.Contains(currentUidOnServer))
                    {
                        Message unseenMessage = client.GetMessage(i + 1);
                        if (unseenMessage.Headers.From.Address.Equals(keeJob) || unseenMessage.Headers.From.Address.Equals(useremail))
                        {
                            // Add the message to the new messages
                            if (unseenMessage.FindAllAttachments().Count > 0)
                            {
                                newMessages.Add(unseenMessage);
                                // Add the uid to the seen uids, as it has now been seen
                                seenUids.Add(currentUidOnServer);
                                //  Console.WriteLine(seenUids.Count);

                            }



                            // Return our new found messages
                        }
                    }
                }
                return newMessages;
            }
        }
    }


}