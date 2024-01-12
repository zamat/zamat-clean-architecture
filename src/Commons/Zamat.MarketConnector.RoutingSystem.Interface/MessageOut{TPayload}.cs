namespace AUMS.MarketConnector.RoutingSystem.Interface;

/// <summary>
/// Message from AMC To foreign system
/// </summary>
/// <typeparam name="TPayload"></typeparam>
/// <example>
///  new MessageOut&lt;TPayload&gt;
///  {
///     AmcMessageId = new Guid("8083F08B-85CE-41C2-BA63-6F310474BC8C"),
///     AmcProcessId = new Guid("595707EC-ECBD-427C-A26D-5350C9CA1CE1"),
///     ProcessId = "BpaProcess123",
///     ProcessTypeSymbol = "CK1000",
///     MessageTypeSymbol = "1.1_1",
///     UniqueMessageTypeSymbol = "1.1.1.1.",
///     TenantId = new Guid("2696500E-F046-48BC-BF6D-84CD0CE73156"),
///     Metadata = new Dictionary&lt;string, string&gt; { {"PodNo", "PPE1234"} },
///     Payload = new TPayload()
///  }
/// </example>
public class MessageOut<TPayload> : MessageOut where TPayload : class
{
    /// <summary>
    /// Message Content
    /// </summary>
    public TPayload Payload { get; set; } = null!;
}
