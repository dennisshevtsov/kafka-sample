// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Confluent.Kafka;
using KafkaSample.Producer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton((IServiceProvider provider) =>
{
  ProducerConfig config = builder.Configuration.GetSection("Kafka").Get<ProducerConfig>() ??
                          throw new Exception("No kafka producer config section: 'Kafka'.");
  IProducer<Null, string> producer = new ProducerBuilder<Null, string>(config).Build();

  ILogger<KafkaMessageProducer> logger = provider.GetRequiredService<ILogger<KafkaMessageProducer>>();

  KafkaMessageProducer messageProducer = new(producer, logger);

  return messageProducer;
});
builder.Services.AddHostedService<ProducerHostedService>();

WebApplication app = builder.Build();
app.MapGet("/", () => "Kafka sample producer working...");
app.Run();
