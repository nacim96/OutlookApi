using System;
using System.Collections.Generic;

//add references of OpenPop  
using OpenPop.Mime;
using OpenPop.Pop3;

namespace apiOutlook.Models
{
    [Serializable]
    public class MessageModel
    {
        public MessageModel()
        {

            this.Attachments = new List<Attachment>();

        }
        public string Id { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public DateTime msgDate { get; set; }
        public virtual List<Attachment> Attachments { get; set; }
    }

}