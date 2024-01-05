// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace KafkaSample.Consumer;

public sealed class ConsumerHostedService(ILogger<ConsumerHostedService> logger) : BackgroundService
{
  private readonly ILogger<ConsumerHostedService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("Consumer starting...");
    // Consume and process kafka message.
    _logger.LogInformation("Consumer started.");
    return Task.CompletedTask;
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Consumer stopping...");
    await base.StopAsync(cancellationToken);
    _logger.LogInformation("Consumer stopped.");
  }
}
