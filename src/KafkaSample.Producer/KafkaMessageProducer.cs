// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Confluent.Kafka;

namespace KafkaSample.Producer;

public sealed class KafkaMessageProducer(IProducer<Null, string> producer, ILogger<KafkaMessageProducer> logger)
{
  private readonly IProducer<Null, string>     _producer = producer ?? throw new ArgumentNullException(nameof(producer));
  private readonly ILogger<KafkaMessageProducer> _logger = logger   ?? throw new ArgumentNullException(nameof(logger))  ;

  public void Hello()
  {
    _logger.LogInformation("Kafka message sending...");
    _producer.Produce
    (
      topic          : "kafka-sample-topic",
      message        : new Message<Null, string>
      {
        Value = "Hello world!",
      },
      deliveryHandler: (DeliveryReport<Null, string> report) =>
      {
        if (report is null)
        {
          _logger.LogWarning("Kafka delivery report null.");
          return;
        }

        if (report.Status == PersistenceStatus.NotPersisted)
        {
          _logger.LogWarning("Sending kafka message failed.");
          return;
        }

        if (report.Status == PersistenceStatus.PossiblyPersisted)
        {
          _logger.LogWarning("Sending kafka message possibly failed.");
          return;
        }

        if (report.Status == PersistenceStatus.Persisted)
        {
          _logger.LogInformation("Kafka message sent.");
          return;
        }
      }
    );
  }
}
