using System.ComponentModel.DataAnnotations;

namespace CarMechanic.Shared;

public class TimeValidationAttribute: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var date = (int)value;
        if (date <= DateTime.Today.Year && date >= new DateTime(1900, 1, 1).Year)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Invalid date");
        }
    }
}