// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Confluent.Kafka;

namespace KafkaSample.Consumer;

public sealed class ConsumerHostedService(IConsumer<Null, string> consumer, string topic, ILogger<ConsumerHostedService> logger) : BackgroundService
{
  private readonly IConsumer<Null, string>      _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
  private readonly string                          _topic = topic    ?? throw new ArgumentNullException(nameof(topic));
  private readonly ILogger<ConsumerHostedService> _logger = logger   ?? throw new ArgumentNullException(nameof(logger));

  protected override Task ExecuteAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Consumer starting...");
    Task consumeLoop = Task.Run(() => Consume(cancellationToken));
    _logger.LogInformation("Consumer started.");
    return consumeLoop;
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Consumer stopping...");
    await base.StopAsync(cancellationToken);
    _logger.LogInformation("Consumer stopped.");
  }

  public override void Dispose()
  {
    _consumer?.Close();
    _consumer?.Dispose();

    base.Dispose();
  }

  private void Consume(CancellationToken cancellationToken)
  {
    _consumer.Subscribe(_topic);

    while (!cancellationToken.IsCancellationRequested)
    {
      try
      {
        _logger.LogInformation("Message consuming");
        ConsumeResult<Null, string> result = _consumer.Consume(cancellationToken);
        _logger.LogInformation("Message '{Message}' consumed.", result.Message.Value);
      }
      catch (OperationCanceledException)
      {
        break;
      }
      catch (ConsumeException ex)
      {
        _logger.LogError(ex, "Message consuming failed.");

        if (ex.Error.IsFatal)
        {
          break;
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Message consuming failed with unexpected error.");
      }
    }
  }
}
