using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.Utilities
{
    public class ServiceValidationException : Exception
    {
        public ServiceValidationException(ValidationContext validationContext, IEnumerable<ValidationResult> validationResults) : this("Validation Error", validationContext, validationResults)
        {

        }
        public ServiceValidationException(string message, ValidationContext validationContext, IEnumerable<ValidationResult> validationResults) : base(message)
        {
            ValidationContext = validationContext;
            ValidationResults = validationResults;
        }

        public ValidationContext ValidationContext { get; private set; }
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }
    }
}
