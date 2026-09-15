using System.ComponentModel.DataAnnotations;

namespace AuthorsWebAPI.Validations
{
    public class FirstLetterCapitalAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) 
            {
                // we avoid to validate things that are already validated like null value
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper()) 
            {
                return new ValidationResult("The first letter should be capital");
            }

            return ValidationResult.Success;
        }
    }
}
