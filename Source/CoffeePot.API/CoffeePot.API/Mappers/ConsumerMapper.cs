using CoffeePot.API.DTOs.Consumer;
using CoffeePot.Domain.Entities;

namespace CoffeePot.API.Mappers;

public static class ConsumerMapper
{
  /// <summary>
  ///   Converts a WriteConsumerDto into a Consumer. 
  /// </summary>
  /// <param name="writeConsumerDto">The WriteConsumerDto.</param>
  public static Consumer ConvertWriteConsumerDtoIntoConsumer(WriteConsumerDto writeConsumerDto)
  {
    return new Consumer { Forename = writeConsumerDto.Forename, Surname = writeConsumerDto.Surname };
  }

  /// <summary>
  ///   Converts a Consumer into a ConsumerDto.
  /// </summary>
  /// <param name="consumer">The Consumer.</param>
  public static ConsumerDto ConvertConsumerIntoConsumerDto(Consumer consumer)
  {
    return new ConsumerDto(consumer.Id, consumer.Forename, consumer.Surname, consumer.Status);
  }
}
