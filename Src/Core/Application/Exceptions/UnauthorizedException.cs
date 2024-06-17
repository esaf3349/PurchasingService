using Application.Exceptions.Common;

namespace Application.Exceptions;

public class UnauthorizedException(string message) : Exception(message), IApplicationException;