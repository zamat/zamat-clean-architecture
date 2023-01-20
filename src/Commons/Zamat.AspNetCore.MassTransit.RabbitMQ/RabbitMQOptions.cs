namespace Zamat.AspNetCore.MassTransit.RabbitMQ;

public class RabbitMQOptions
{
    public string Host { get; set; } = default!;
    public string? Prefix { get; set; }
}
