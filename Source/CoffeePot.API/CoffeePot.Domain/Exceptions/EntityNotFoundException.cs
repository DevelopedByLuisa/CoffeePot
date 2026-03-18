using System;

namespace CoffeePot.Domain.Exceptions;

public class EntityNotFoundException(string? message) : Exception(message);
