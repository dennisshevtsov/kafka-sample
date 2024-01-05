// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace KafkaSample.Producer;

public sealed class ProducerHostedService(ILogger<ProducerHostedService> logger) : IHostedService, IDisposable
{
  private readonly ILogger<ProducerHostedService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

  private Timer? _timer;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Producer starting...");
    _timer = new Timer
    (
      callback: SendMessage,
      state   : null,
      dueTime : TimeSpan.Zero,
      period  : TimeSpan.FromSeconds(5)
    );
    _logger.LogInformation("Producer started.");

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Producer stopping...");
    _timer?.Change(Timeout.Infinite, 0);
    _logger.LogInformation("Producer stopped.");

    return Task.CompletedTask;
  }

  public void Dispose() => _timer?.Dispose();

  private void SendMessage(object? state)
  {
    _logger.LogInformation("Producer sending message to kafka...");
    // send message to kafka
    _logger.LogInformation("Producer sent message to kafka.");
  }
}
