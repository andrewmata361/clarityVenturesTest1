using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clarityVenturesTest.Models;
using clarityVenturesTest.Models.Schemas;
using Microsoft.Extensions.Hosting;

namespace clarityVenturesTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RecipientsController : ControllerBase
    {
        private readonly RecipientsContext _context;
        private string? Host { get; set; }
        private int Port { get; set; }
        private string? UserName { get; set; }
        private string? Password { get; set; }
        private bool? UseSsl { get; set; }
        public RecipientsController(RecipientsContext context)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            Host = configuration.GetValue<string>("MailSettings:Host");
            Port = configuration.GetValue<int>("MailSettings:Port");
            UserName = configuration.GetValue<string>("MailSettings:UserName");
            Password = configuration.GetValue<string>("MailSettings:Password");
            UseSsl = configuration.GetValue<bool>("MailSettings:UseSSL");
            _context = context;
        }

        // GET: api/Recipients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipients>>> GetRecipients()
        {
            return await _context.Recipients.ToListAsync();
        }

        // GET: api/Recipients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipients>> GetRecipients(long id)
        {
            var recipients = await _context.Recipients.FindAsync(id);

            if (recipients == null)
            {
                return NotFound();
            }

            return recipients;
        }

        // POST: api/Recipients/Send
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Send")]
        public async Task<ActionResult<Recipients>> SendMail(SendFields sendData)
        {
            ClarityTestMailer.TestMailer cmailer = new(
                Host,
                Port,
                UserName,
                Password,
                UseSsl
            );
            Recipients recipients = new(
                sendData.EmailSender,
                sendData.EmailRecipient,
                sendData.Subject,
                sendData.Body
            );
            cmailer.Connect();

            recipients.Date = DateTime.Now;
            recipients.SentStatus = await cmailer.Send(sendData.EmailSender, sendData.EmailRecipient, sendData.Subject, sendData.Body);
            _context.Recipients.Add(recipients);
            await _context.SaveChangesAsync();


            // keep trying until sent
            DateTime startTime = DateTime.Now;
            while (!(recipients.SentStatus ?? false) && (DateTime.Now - startTime).TotalSeconds < 60)
            {
                recipients.SentStatus = await cmailer.Send(sendData.EmailSender, sendData.EmailRecipient, sendData.Subject, sendData.Body);
                if (recipients.SentStatus == true)
                {
                    _context.Recipients.Update(recipients);
                    await _context.SaveChangesAsync();
                }
                await Task.Delay(1000);
            }

            return CreatedAtAction(nameof(GetRecipients), new { id = recipients.Id }, recipients);
        }

        private bool RecipientsExists(long id)
        {
            return _context.Recipients.Any(e => e.Id == id);
        }
    }
}
