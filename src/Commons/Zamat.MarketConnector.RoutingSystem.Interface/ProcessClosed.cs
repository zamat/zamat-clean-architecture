using System;

namespace AUMS.MarketConnector.RoutingSystem.Interface;

/// <summary>
/// Send after foreign system close process
/// </summary>
public class ProcessClosed
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid ForeignSystemUserId { get; set; }

    /// <summary>
    /// Foreign system (for example BPA) process id.
    /// </summary>
    public string ForeignSystemProcessId { get; set; } = null!;

}
