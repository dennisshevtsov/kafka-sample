// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Confluent.Kafka;
using KafkaSample.Consumer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService((IServiceProvider provider) =>
      {
        ConsumerConfig config = builder.Configuration.GetSection("Kafka").Get<ConsumerConfig>() ??
                                throw new Exception("No kafka consumer config section: 'Kafka'.");
        IConsumer<Null, string> consumer = new ConsumerBuilder<Null, string>(config).Build();
        string topic = builder.Configuration.GetValue<string>("LISTENING_TOPIC") ??
                       throw new Exception("No listening kafka topic: 'LISTENING_TOPIC'.");
        ILogger<ConsumerHostedService> logger = provider.GetRequiredService<ILogger<ConsumerHostedService>>();
        ConsumerHostedService backbroundService = new
        (
          consumer: consumer,
          topic   : topic,
          logger  : logger
        );

        return backbroundService;
      });

WebApplication app = builder.Build();
app.MapGet("/", () => "Kafka sample consumer working...");
app.Run();
