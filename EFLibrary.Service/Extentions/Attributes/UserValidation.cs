using System.ComponentModel.DataAnnotations;

namespace EFLibrary.Service.Extentions.Attributes
{
    public class UserValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string)
            {
                if (value.ToString()?.Trim().Length == 0
                   )
                {
                    return new ValidationResult("User's information must not be empty");
                }
            }

            return ValidationResult.Success;
        }
    }
}