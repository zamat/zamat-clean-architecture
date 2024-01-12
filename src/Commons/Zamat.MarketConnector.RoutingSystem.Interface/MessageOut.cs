using System;
using System.Collections.Generic;

namespace AUMS.MarketConnector.RoutingSystem.Interface;

public class MessageOut
{
    /// <summary>
    /// Message Id assigned by AMC
    /// </summary>
    public Guid AmcMessageId { get; set; }

    /// <summary>
    /// Process id assigned by AMC
    /// </summary>
    public Guid AmcProcessId { get; set; }

    /// <summary>
    /// Foreign system (for example BPA) process id.
    /// </summary>
    /// <remarks>If known by AMC</remarks>
    public string? ForeignSystemProcessId { get; set; }

    /// <summary>
    /// Type of process
    /// </summary>
    public string ProcessTypeSymbol { get; set; } = null!;

    /// <summary>
    /// Message type
    /// </summary>
    /// <remarks>
    /// <para><see cref="MessageTypeSymbol"/> and <see cref="UniqueMessageTypeSymbol"/> should be equal if
    /// foreign system uses only one field to identify messages types</para>
    /// </remarks>
    public string MessageTypeSymbol { get; set; } = null!;

    /// <summary>
    /// Message unique type
    /// </summary>
    /// <remarks>
    /// <para><see cref="MessageTypeSymbol"/> and <see cref="UniqueMessageTypeSymbol"/> should be equal if
    /// foreign system uses only one field to identify messages types</para>
    /// </remarks>
    public string UniqueMessageTypeSymbol { get; set; } = null!;

    /// <summary>
    /// Tenant identifier
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// User identifier
    /// </summary>
    public Guid AmcUserId { get; set; }

    /// <summary>
    /// Additional data
    /// </summary>
    public Dictionary<string, string>? Metadata { get; set; }
}
