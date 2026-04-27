using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Consumer;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.API.Services;

public class ConsumerService(IGenericRepository<Consumer> consumerRepository)
{
  public async Task<ConsumerDto> CreateConsumerAsync(WriteConsumerDto writeConsumerDto, CancellationToken cancellationToken)
  {
    var consumer = ConsumerMapper.ConvertWriteConsumerDtoIntoConsumer(writeConsumerDto);
    var processedConsumer = await consumerRepository.CreateAsync(consumer, cancellationToken);
    return ConsumerMapper.ConvertConsumerIntoConsumerDto(processedConsumer);
  }

  public async Task<ConsumerDto> UpdateConsumerAsync(int id, WriteConsumerDto writeConsumerDto, CancellationToken cancellationToken)
  {
    var loadedConsumer = await consumerRepository.GetByIdAsync(id, cancellationToken);
    loadedConsumer.Update(writeConsumerDto.Forename, writeConsumerDto.Surname);

    loadedConsumer = await consumerRepository.UpdateAsync(loadedConsumer, cancellationToken);
    return ConsumerMapper.ConvertConsumerIntoConsumerDto(loadedConsumer);
  }

  public async Task<ConsumerDto> DeleteConsumerAsync(int id, CancellationToken cancellationToken)
  {
    var loadedConsumer = await consumerRepository.GetByIdAsync(id, cancellationToken);
    loadedConsumer.Delete();

    loadedConsumer = await consumerRepository.UpdateAsync(loadedConsumer, cancellationToken);
    return ConsumerMapper.ConvertConsumerIntoConsumerDto(loadedConsumer);
  }

  public async Task<IEnumerable<ConsumerDto>> GetConsumersAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedConsumer = await consumerRepository.GetAsync(cancellationToken);

    if (includeDeleted)
    {
      return loadedConsumer.Select(ConsumerMapper.ConvertConsumerIntoConsumerDto);
    }

    return loadedConsumer
      .Where(consumer => consumer.Status != Status.Deleted)
      .Select(ConsumerMapper.ConvertConsumerIntoConsumerDto);
  }

  public async Task<ConsumerDto> GetConsumerByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedConsumer = await consumerRepository.GetByIdAsync(id, cancellationToken);
    return ConsumerMapper.ConvertConsumerIntoConsumerDto(loadedConsumer);
  }
}
