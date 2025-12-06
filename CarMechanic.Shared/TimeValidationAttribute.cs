using System.ComponentModel.DataAnnotations;

namespace CarMechanic.Shared;

public class TimeValidationAttribute: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {   
        var date = Convert.ToDateTime(value);
        if (date <= DateTime.Today && date >= new DateTime(1900, 1, 1))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Invalid date");
        }
    }
}