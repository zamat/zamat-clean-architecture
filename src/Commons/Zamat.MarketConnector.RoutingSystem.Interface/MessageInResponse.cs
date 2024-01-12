using System;

namespace AUMS.MarketConnector.RoutingSystem.Interface;

public class MessageInResponse
{
    /// <summary>
    /// Message Id assigned by AMC
    /// </summary>
    public Guid? AmcMessageId { get; set; }

    /// <summary>
    /// Process id assigned by AMC
    /// </summary>
    public Guid? AmcProcessId { get; set; }

    /// <summary>
    /// Indicates if message was accepted by AMC
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    public string? ErrorMessage { get; set; } = null;

    /// <summary>
    /// User identifier
    /// </summary>
    public Guid AmcUserId { get; set; }
}
