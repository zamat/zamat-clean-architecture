using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AUMS.AspNetCore.SmsAPI;

public class SmsAPIOptions
{
    public bool Enabled { get; set; } = false;
    public int TimeoutInSeconds { get; set; } = 10;
    [Required]
    [MaxLength(60)]
    public string SenderName { get; set; } = "Test";
    [Required]
    public string Token { get; set; } = "AccessKey";

    public bool Validate()
    {
        var context = new ValidationContext(this, null, null);
        var validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, validationResults, true);
    }
}
