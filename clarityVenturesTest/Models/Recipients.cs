using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace clarityVenturesTest.Models;

public class Recipients
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Email sender is required.")]
    public string EmailSender { get; set; }

    [Required(ErrorMessage = "Email recipient is required.")]
    public string EmailRecipient { get; set; }

    [Required(ErrorMessage = "Subject is required.")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Body is required.")]
    public string Body { get; set; }

    [DefaultValue(false)]
    public bool? SentStatus { get; set; }

    [DefaultValue(0)]
    public int RetryAttempts { get; set; }

    public DateTime Date { get; set; }

    public Recipients()
    {
    }
    public Recipients(string emailSender, string emailRecipient, string subject, string body) {
        EmailSender = emailSender;
        EmailRecipient = emailRecipient;
        Subject = subject;
        Body = body;
    }
}
