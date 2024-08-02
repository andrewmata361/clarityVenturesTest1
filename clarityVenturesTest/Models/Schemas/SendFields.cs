using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace clarityVenturesTest.Models.Schemas;

public class SendFields
{
    [Required(ErrorMessage = "Email sender is required.")]
    public required string EmailSender { get; set; }

    [Required(ErrorMessage = "Email recipient is required.")]
    public required string EmailRecipient { get; set; }

    [Required(ErrorMessage = "Subject is required.")]
    public required string Subject { get; set; }

    [Required(ErrorMessage = "Body is required.")]
    public required string Body { get; set; }
}
