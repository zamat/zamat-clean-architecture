using System;

namespace AUMS.Common.ActionLinks.Consts;

/// <summary>
/// Class that defines identifiers for formats in AUMS 
/// </summary>
public class ActionLinkItemGuid
{
    /// <summary>
    /// Contracts 
    /// </summary>
    public static Guid ActionLinkItemAgreementGuid
        => Guid.Parse("c9fc5f8f-e0b9-40ad-8ac9-469465d91b7c");

    /// <summary>
    /// Deregulation tree
    /// </summary>
    public static Guid ActionLinkItemDeregulationTreeGuid
        => Guid.Parse("fec6a5e1-a2ab-420b-b074-fe35d7915ab3");

    /// <summary>
    /// Point of consumption
    /// </summary>
    public static Guid ActionLinkItemPointOfDeliveryGuid
        => Guid.Parse("2aab7abd-ceb2-4a1a-b8f5-50c8d9c86984");
}
