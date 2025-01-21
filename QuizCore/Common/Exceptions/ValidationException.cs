using FluentValidation.Results;

namespace QuizCore.Common.Exceptions{
    public class ValidationServiceException : Exception
    {
        public ValidationServiceException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
            ErrorsList = new List<string>();
        }

        public ValidationServiceException(IEnumerable<ValidationFailure> failures)
            : this()

        {

             ErrorsList.AddRange(failures.Select(x=>x.ErrorMessage).Distinct());   
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);
            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();
                // var ErrorMessage = propertyFailures.ElementAt(1);
                // ErrorsList.Add(ErrorMessage);
                Errors.Add(propertyName, propertyFailures);
               
            }
          
        }

        public IDictionary<string, string[]> Errors { get; }
        public List<string> ErrorsList { get; }

    }
}