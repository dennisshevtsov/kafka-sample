// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using KafkaSample.Producer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<ProducerHostedService>();

WebApplication app = builder.Build();
app.MapGet("/", () => "Kafka sample producer working...");
app.Run();
