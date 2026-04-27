using AutoFixture.Xunit3;
using CoffeePot.API.DTOs.Consumer;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.Mappers;

public class ConsumerMapperTest
{
  [Theory]
  [AutoData]
  public void ConvertWriteConsumerDtoIntoConsumer_ShouldReturnConsumer(WriteConsumerDto writeConsumerDto)
  {
    var result = ConsumerMapper.ConvertWriteConsumerDtoIntoConsumer(writeConsumerDto);

    Assert.Equal(writeConsumerDto.Forename, result.Forename);
    Assert.Equal(writeConsumerDto.Surname, result.Surname);
  }

  [Theory]
  [AutoData]
  public void ConvertConsumerIntoConsumerDto_ShouldReturnConsumerDto(Consumer consumer)
  {
    var result = ConsumerMapper.ConvertConsumerIntoConsumerDto(consumer);

    Assert.Equal(consumer.Id, result.Id);
    Assert.Equal(consumer.Forename, result.Forename);
    Assert.Equal(consumer.Surname, result.Surname);
    Assert.Equal(consumer.Status, result.Status);
  }
}
