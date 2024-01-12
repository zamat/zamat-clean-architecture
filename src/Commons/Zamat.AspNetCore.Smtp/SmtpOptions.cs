using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AUMS.AspNetCore.Smtp;

public class SmtpOptions
{
    public bool Enabled { get; set; } = false;
    public int TimeoutInSeconds { get; set; } = 10;
    [Required]
    [MaxLength(100)]
    public string Host { get; set; } = "localhost";
    [Required]
    public int Port { get; set; } = 587;
    [Required]
    public string From { get; set; } = default!;
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool EnableSsl { get; set; } = false;
    public bool UseDefaultCredentials { get; set; } = false;

    public bool Validate()
    {
        var context = new ValidationContext(this, null, null);
        var validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(this, context, validationResults, true);
    }
}
