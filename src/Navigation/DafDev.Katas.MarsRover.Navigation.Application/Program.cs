using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateApplicationBuilder(args).Build();
await host.RunAsync();