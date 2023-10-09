using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace backend.Shared.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredGUIDAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                if (Guid.TryParse(input, out _))
                    return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}