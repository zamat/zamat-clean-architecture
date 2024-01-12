using System;
using System.Collections.Generic;

namespace AUMS.MarketConnector.RoutingSystem.Interface;

public abstract class MessageIn
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid ForeignSystemUserId { get; set; }

    /// <summary>
    /// Foreign system (for example BPA) process id.
    /// </summary>
    public string? ForeignSystemProcessId { get; set; } = null!;

    /// <summary>
    /// Message id from foreign system
    /// </summary>
    /// <remarks>Used to detect message duplicates</remarks>
    public string ForeignSystemMessageId { get; set; } = null!;

    /// <summary>
    /// Type of process
    /// </summary>
    /// <remarks>
    /// Used for creating new processes or validating existing passed in <see cref="ForeignSystemProcessId"/>
    /// </remarks>
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
    /// Additional data
    /// </summary>
    public Dictionary<string, string>? Metadata { get; set; }
}
