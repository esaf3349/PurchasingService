using Application.Exceptions.Common;

namespace Application.Exceptions;

public class AlreadyExistsException(string message) : Exception(message), IApplicationException;