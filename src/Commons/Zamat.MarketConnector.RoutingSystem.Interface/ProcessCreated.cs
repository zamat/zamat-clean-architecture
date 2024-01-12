using System;

namespace AUMS.MarketConnector.RoutingSystem.Interface;

/// <summary>
/// Foreign system response after creating new process, as result of receiving MessageOut from AMC
/// </summary>
public class ProcessCreated
{
    /// <summary>
    /// AMC Process id 
    /// </summary>
    public Guid AmcProcessId { get; set; }

    /// <summary>
    /// Foreign system (for example BPA) process id.
    /// </summary>
    public string ForeignSystemProcessId { get; set; } = null!;

    /// <summary>
    /// User identifier
    /// </summary>
    public Guid ForeignSystemUserId { get; set; }

}
