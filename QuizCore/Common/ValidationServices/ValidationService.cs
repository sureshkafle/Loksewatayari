
using FluentValidation.Results;
using QuizCore.Common.Exceptions;

namespace QuizCore.Common.ValidationServices
{
    public class ValidationService
    {

        public static void Validate(ValidationResult validatorResult)
        {
            if (!validatorResult.IsValid)
            {
                var failures = validatorResult.Errors.Where(f => f != null).ToList();
                throw new ValidationServiceException(failures);
            }

        }
    }
}