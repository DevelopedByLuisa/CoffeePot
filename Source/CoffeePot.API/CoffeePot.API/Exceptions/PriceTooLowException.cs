using System;

namespace CoffeePot.API.Exceptions;

public class PriceTooLowException(string? message) : Exception(message);
