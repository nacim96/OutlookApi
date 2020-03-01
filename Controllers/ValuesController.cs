using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apiOutlook.ContextDB;
using apiOutlook.Models;
using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;

using OpenPop.Pop3;
using OpenPop.Mime;
namespace apiOutlook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        List<Message> allmessage = new List<Message>();

        static List<Message> newmessage;
        static List<string> seenUids = new List<string>();

        private readonly apiOutlookContext _context;
        private AllFunction all = new AllFunction();

        private MessageModel msg;
        Pop3Client client = new Pop3Client();

        public ValuesController(apiOutlookContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageModel>>> Get()
        {
            return await _context.MessagesapiOutlook.Include(pub => pub.Attachments).ToListAsync();
        }


        [HttpPost]
        public void PostEmail()
        {
            {
                if (all.Connect(client))
                {

                    newmessage = AllFunction.FetchUnseenMessages(client, seenUids);
                    int messageCount = newmessage.Count;

                    Console.WriteLine("unseen msg :" + messageCount);
                    // for(int i = 0; i<messageCount; i++)

                    for (int i = messageCount; i > 0; i--)
                    {
                        // if (newmessage[i].Headers.From.Address.Equals(keeJob) || newmessage[i].Headers.From.Address.Equals(useremail))
                        {

                            //allmessage.Add(newmessage[i]);
                            // Console.WriteLine(msg.Id);

                            msg = all.GetEmailContent(newmessage[i - 1]);
                            Console.WriteLine(msg.Id);
                            if (!MsgExistInBD(msg.Id))
                            {
                                _context.MessagesapiOutlook.Add(msg);
                                //Console.WriteLine(msg.FromName);
                                _context.SaveChanges();
                            }

                        }
                    }


                }

            }

        }
        private bool MsgExistInBD(string id)
        {
            return _context.MessagesapiOutlook.Any(e => e.Id == id);
        }


        // GET: api/values/5
        /*    [HttpGet("{id}")]
            public async Task<ActionResult<MessageModel>> Get(int id)
            {
                var author = await _context.MessagesapiOutlook.FindAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                return author;
            }
        */

        // PUT: api/values/5
        /*   [HttpPut("{id}")]
           public async Task<IActionResult> PutAuthor(int id, [Bind("Id,FromName,Subject")]MessageModel author)
           {
               if (id != author.Id)
               {
                   return BadRequest();
               }

               _context.Entry(author).State = EntityState.Modified;//plus de détail

               try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!EtudiantExists(id))
                   {
                       return NotFound();
                   }
                   else
                   {
                       throw;
                   }
               }

               return Ok(author);
           }*/
        // POST: api/values
        // To protect from overposting attacks
        // DELETE: api/values/5
        /* [HttpDelete("{id}")]
         public async Task<ActionResult<MessageModel>> DeleteAuthor(int id)
         {
             var author = await _context.MessagesapiOutlook.FindAsync(id);
             if (author == null)
             {
                 return NotFound();
             }

             _context.MessagesapiOutlook.Remove(author);
             await _context.SaveChangesAsync();

             return author;
         }
         private bool EtudiantExists(int id)
         {
             return _context.MessagesapiOutlook.Any(e => e.Id == id);
         }*/
    }
}