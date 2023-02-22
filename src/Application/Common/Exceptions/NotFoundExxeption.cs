using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Exceptions;

    public class NotFoundExxeption : Exception
    {
        public NotFoundExxeption() : base() { }
        public NotFoundExxeption(string message) :base(message) { }
        public NotFoundExxeption(string message,Exception innerException) :base(message,innerException) { }

        public NotFoundExxeption(string name,object key) :base($"Entity\"{name}\"({key} was not found"){}
    
        
    }


public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }
    public ValidationException(): base("one or more validation failures have occured") =>Errors= new Dictionary<string, string[]>();
   
    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key,failureGroup => failureGroup.ToArray());
    }
}
