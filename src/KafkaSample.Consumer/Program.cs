// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using KafkaSample.Consumer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<ConsumerHostedService>();

WebApplication app = builder.Build();
app.MapGet("/", () => "Kafka sample consumer working...");
app.Run();
