using Application.Exceptions.Common;

namespace Application.Exceptions;

public class NotFoundException(string message) : Exception(message), IApplicationException;