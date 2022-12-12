using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AMP.Processors.Exceptions.Extensions;

public static class ExceptionExtensions
{
    public static int GetStatusCode(this Exception exception)
    {
        return exception.GetType().Name switch
        {
            nameof(NullRegistrationException) => (int)HttpStatusCode.NotFound,
            nameof(UserAlreadyExistsException) => (int)HttpStatusCode.Conflict,
            nameof(UserVerificationFailedException) => (int)HttpStatusCode.NotFound,
            nameof(InvalidIdException) => (int)HttpStatusCode.NotFound,
            nameof(SmsException) => (int)HttpStatusCode.BadRequest,
            nameof(NullReferenceException) => (int)HttpStatusCode.BadRequest,
            nameof(ValidationException) => (int)HttpStatusCode.PreconditionFailed,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
}