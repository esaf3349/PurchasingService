using Application.Exceptions.Common;

namespace Application.Exceptions;

public class BadRequestException(string message) : Exception(message), IApplicationException;