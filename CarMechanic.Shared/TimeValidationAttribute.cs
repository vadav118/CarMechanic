using System.ComponentModel.DataAnnotations;

namespace CarMechanic.Shared;

public class TimeValidationAttribute: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (Convert.ToDateTime(value) <= DateTime.Today &&
            Convert.ToDateTime(value) >= Convert.ToDateTime("1900-01-01"))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Invalid date");
        }
    }
}