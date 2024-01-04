// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace KafkaSample.Producer;

public sealed class ProducerHostedService : IHostedService, IDisposable
{
  private Timer? _timer;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    _timer = new Timer
    (
      callback: SendMessage,
      state   : null,
      dueTime : TimeSpan.Zero,
      period  : TimeSpan.FromSeconds(5)
    );
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _timer?.Change(Timeout.Infinite, 0);
    return Task.CompletedTask;
  }

  public void Dispose() => _timer?.Dispose();

  private void SendMessage(object? state)
  {
    // send message to kafka
  }
}
