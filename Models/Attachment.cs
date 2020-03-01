using System;  
using System.Collections.Generic;  
  
//add references of OpenPop  
using OpenPop.Mime;  
using OpenPop.Pop3; 

namespace apiOutlook.Models
{[Serializable]
public class Attachment  
{   public Attachment(string FileName/*,byte[] Content*/){
    this.FileName =FileName;
   // this.Content =Content;




}
    public int Id{ get; set; }  
        public string FileName{ get; set; }
  // public string ContentType { get; set; }
   // public byte[] Content { get; set; }
  public  MessageModel MessageModel { get; set; }
} 

  }