namespace AUMS.MarketConnector.RoutingSystem.Interface;

/// <summary>
/// Message from foreign system to send by AMC
/// </summary>
/// <typeparam name="TPayload"></typeparam>
/// <example>
///  new MessageIn&lt;TPayload&gt;
///  {
///     ProcessId = "BpaProcess123",
///     CommunicationId = "BpaUniqueMessage123",
///     ProcessTypeSymbol = "CK1000",
///     MessageTypeSymbol = "1.1_1",
///     UniqueMessageTypeSymbol = "1.1.1.1.",
///     TenantId = new Guid("2696500E-F046-48BC-BF6D-84CD0CE73156"),
///     Metadata = new Dictionary&lt;string, string&gt; { {"PodNo", "PPE1234"} },
///     Payload = new TPayload()
///  }
/// </example>
public class MessageIn<TPayload> : MessageIn where TPayload : class
{
    /// <summary>
    /// Message Content
    /// </summary>
    public TPayload Payload { get; set; } = null!;
}
